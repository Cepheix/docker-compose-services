using Microsoft.Extensions.Configuration;

namespace FakeWebApi.Configuration.Vault
{
    public class VaultSecretConfigurationSource : IConfigurationSource
    {
        private readonly VaultConnectionConfiguration _connectionConfiguration;

        public VaultSecretConfigurationSource(VaultConnectionConfiguration connectionConfiguration)
        {
            _connectionConfiguration = connectionConfiguration;
        }

        public IConfigurationProvider Build(IConfigurationBuilder builder)
        {
            return new VaultSecretConfigurationProvider(_connectionConfiguration);
        }
    }
}