using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using VaultSharp;
using VaultSharp.V1.Commons;

namespace FakeWebApi.Configuration.Vault
{
    public class BasicVaultService
    {
        private readonly IVaultClient _vaultClient;

        public BasicVaultService(IVaultClient vaultClient)
        {
            _vaultClient = vaultClient;
        }

        public async Task<Dictionary<string, object>> FindSecretAsync(string secretPath, string enginePath)
        {
            Secret<SecretData> secret = await _vaultClient.V1.Secrets.KeyValue.V2.ReadSecretAsync(secretPath, mountPoint: enginePath);

            if (secret == null)
            {
                throw new NullReferenceException($"Secret {enginePath} / {secretPath} does not exists in the vault");
            }

            return secret.Data.Data;
        }
    }
}