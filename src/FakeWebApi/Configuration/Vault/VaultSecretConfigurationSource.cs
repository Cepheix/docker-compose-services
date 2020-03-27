using Microsoft.Extensions.Configuration;

namespace FakeWebApi.Configuration.Vault
{
    public class VaultSecretConfigurationSource : IConfigurationSource
    {
        private readonly VaultConnectionConfiguration _connectionConfiguration;
        private readonly VaultConfigurationProviderOptions _configurationProviderOptions;

        public VaultSecretConfigurationSource(VaultConnectionConfiguration connectionConfiguration, VaultConfigurationProviderOptions configurationProviderOptions)
        {
            _connectionConfiguration = connectionConfiguration;
            _configurationProviderOptions = configurationProviderOptions;
        }

        public IConfigurationProvider Build(IConfigurationBuilder builder)
        {
            return new VaultSecretConfigurationProvider(_connectionConfiguration, _configurationProviderOptions);
        }
    }
}