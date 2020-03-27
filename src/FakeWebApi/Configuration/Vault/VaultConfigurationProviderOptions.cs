namespace FakeWebApi.Configuration.Vault {

    public class VaultConfigurationProviderOptions
    {
        public bool Reload { get; set; } = true;
        public int ReloadInterval { get; set; } = 90000;
    }

}