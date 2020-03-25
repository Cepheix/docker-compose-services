using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Prometheus;

namespace FakeWebApi.Configuration.Metrics
{
    public static class MetricsExtension {

        public static void AddMetricsConfiguration(this IServiceCollection serviceCollection, IConfiguration configuration)
        {
            var metricsConfiguration = configuration.GetSection("Metrics");
            serviceCollection.Configure<MetricsConfiguration>(metricsConfiguration);
        }

        public static void AddMetricsCapturing(this IApplicationBuilder app, MetricsConfiguration configuration)
        {
            app.UseHttpMetrics();
            var pusher = new MetricPusher(configuration.Url, "FakeApi");

            pusher.Start();
        }

    }
}