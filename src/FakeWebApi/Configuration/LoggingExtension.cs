using System;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Serilog;
using Serilog.Exceptions;
using Serilog.Sinks.Elasticsearch;

namespace FakeWebApi.Configuration
{
    public static class LoggingExtension
    {
        public static void CreateSerilogLogger(IServiceProvider serviceProvider)
        {
            var loggingConfiguration = serviceProvider.GetRequiredService<IOptionsMonitor<ElasticsearchConfiguration>>();
            AssignLogger(loggingConfiguration.CurrentValue);
        }

        public static void AssignLogger(ElasticsearchConfiguration configuration)
        {
            var loggerConfiguration = new LoggerConfiguration()
                .Enrich.FromLogContext()
                .Enrich.WithExceptionDetails()
                .WriteTo.Elasticsearch(new ElasticsearchSinkOptions(new Uri(configuration.Url))
                {
                    AutoRegisterTemplate = true
                });

            var logger = loggerConfiguration.CreateLogger();

            Log.Logger = logger;
        }

        public static void AddLoggerConfiguration(this IServiceCollection services, IConfiguration configuration)
        {
            var loggingConfiguration = configuration.GetSection("Elasticsearch");
            services.Configure<ElasticsearchConfiguration>(loggingConfiguration);
        }
    }
}