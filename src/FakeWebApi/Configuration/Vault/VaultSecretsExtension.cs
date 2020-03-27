using Microsoft.Extensions.Configuration;

namespace FakeWebApi.Configuration.Vault
{
    public static class VaultSecretsExtension
    {
        public static IConfigurationBuilder AddVaultSecrets(this IConfigurationBuilder builder, bool shouldReload, int reloadInterval)
        {
            IConfigurationRoot configuration = builder.Build();
            var vaultConfiguration = configuration.GetSection("Vault").Get<VaultConnectionConfiguration>();

            var options = new VaultConfigurationProviderOptions() {
                Reload = shouldReload,
                ReloadInterval = reloadInterval
            };

            builder.Add(new VaultSecretConfigurationSource(vaultConfiguration, options));

            return builder;
        }
    }
}