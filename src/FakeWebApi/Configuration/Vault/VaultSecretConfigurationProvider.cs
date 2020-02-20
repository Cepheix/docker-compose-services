using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using VaultSharp;
using VaultSharp.V1.AuthMethods.Token;

namespace FakeWebApi.Configuration.Vault
{
        public class VaultSecretConfigurationProvider : ConfigurationProvider
    {
        private readonly BasicVaultService _vaultService;
        private readonly VaultConnectionConfiguration _connectionConfiguration;

        public VaultSecretConfigurationProvider(VaultConnectionConfiguration connectionConfiguration)
        {
            var vaultClient = new VaultClient(new VaultClientSettings(connectionConfiguration.VaultAddress,
                new TokenAuthMethodInfo(connectionConfiguration.AuthenticationToken)));

            _vaultService = new BasicVaultService(vaultClient);

            _connectionConfiguration = connectionConfiguration;
        }

        public override void Load() => LoadAsync().ConfigureAwait(false).GetAwaiter().GetResult();

        private async Task LoadAsync()
        {
            var common = _connectionConfiguration.Common;
            await AddSecretToConfiguration(common.ElasticSearchPath, common.EnginePath);
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