using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using VaultSharp;
using VaultSharp.V1.AuthMethods.Token;
using Timer = System.Timers.Timer;

namespace FakeWebApi.Configuration.Vault
{
        public class VaultSecretConfigurationProvider : ConfigurationProvider
    {
        private readonly BasicVaultService _vaultService;
        private readonly VaultConnectionConfiguration _connectionConfiguration;

        private readonly Timer _reloadTimer = new Timer();

        public VaultSecretConfigurationProvider(VaultConnectionConfiguration connectionConfiguration, VaultConfigurationProviderOptions options)
        {
            var vaultClient = new VaultClient(new VaultClientSettings(connectionConfiguration.VaultAddress,
                new TokenAuthMethodInfo(connectionConfiguration.AuthenticationToken)));

            _vaultService = new BasicVaultService(vaultClient);

            _connectionConfiguration = connectionConfiguration;

            if (options.Reload) {
                _reloadTimer.AutoReset = false;
                _reloadTimer.Interval = options.ReloadInterval;
                _reloadTimer.Elapsed += (s, e) => { Load(); };
            }
        }

        private async Task LoadAsync()
        {
            var common = _connectionConfiguration.Common;
            await AddSecretToConfiguration(common.ElasticSearchPath, common.EnginePath);
            await AddSecretToConfiguration(common.MonitoringPath, common.EnginePath);
        }

        public override void Load()
        {
            try
            {
                Data.Clear();
                LoadAsync().ConfigureAwait(false).GetAwaiter().GetResult();
                OnReload();
            }
            finally
            {
                _reloadTimer.Start();
            }
        }

        private async Task AddSecretToConfiguration(string secretPath, string enginePath)
        {
            var secret = await _vaultService.FindSecretAsync(secretPath, enginePath);

            foreach (KeyValuePair<string, object> keyValuePair in secret)
            {
                string name = $"{secretPath}:{keyValuePair.Key}";
                Data.Add(name, keyValuePair.Value.ToString());
            }
        }
    }
}