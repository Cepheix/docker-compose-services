using Microsoft.Extensions.Configuration;

namespace FakeWebApi.Configuration.Vault
{
    public static class VaultSecretsExtension
    {
        public static IConfigurationBuilder AddVaultSecrets(this IConfigurationBuilder builder)
        {
            IConfigurationRoot configuration = builder.Build();
            var vaultConfiguration = configuration.GetSection("Vault").Get<VaultConnectionConfiguration>();

            builder.Add(new VaultSecretConfigurationSource(vaultConfiguration));

            return builder;
        }
    }
}