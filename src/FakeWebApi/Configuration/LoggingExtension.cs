using System;
using Serilog;
using Serilog.Exceptions;
using Serilog.Sinks.Elasticsearch;

namespace FakeWebApi.Configuration
{
    public static class LoggingExtension
    {
        public static void CreateSerilogLogger(IServiceProvider serviceProvider)
        {
            var config = new ElasticsearchConfiguration()
            {
                Url = "http://localhost:9200/"
            };

            AssignLogger(config);
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
    }
}