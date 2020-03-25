namespace FakeWebApi.Configuration.Vault
{
        public class VaultConnectionConfiguration
    {
        public string VaultAddress { get; set; }
        public string AuthenticationToken { get; set; }
        public CommonConnection Common { get; set; }
    }

    public class CommonConnection
    {
        public string EnginePath { get; set; }
        public string ElasticSearchPath { get; set; }
        public string MonitoringPath { get; set; }
    }
}