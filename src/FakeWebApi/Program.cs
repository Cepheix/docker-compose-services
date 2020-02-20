using System;
using FakeWebApi.Configuration;
using FakeWebApi.Configuration.Vault;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Serilog;

namespace FakeWebApi
{
    public class Program
    {
        public static void Main(string[] args)
        {
            IHost host = CreateHostBuilder(args).Build();
            InitializeServices(host);
            RunHost(host);
        }

        public static void InitializeServices(IHost host)
        {
            using var scope = host.Services.CreateScope();
            IServiceProvider services = scope.ServiceProvider;
            LoggingExtension.CreateSerilogLogger(services);
        }

        public static void RunHost(IHost host)
        {
            try {
                Log.Information("Starting web host");
                host.Run();
            }
            catch (Exception ex) {
                Log.Fatal(ex, "Host terminated unexpectedly");
            }
            finally {
                Log.CloseAndFlush();
            }
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureAppConfiguration((hostingContext, config) => {
                    config.AddVaultSecrets();
                })
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                })
                .UseSerilog();
    }
}
