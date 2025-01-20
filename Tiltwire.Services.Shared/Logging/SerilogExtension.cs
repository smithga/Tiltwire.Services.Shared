using System.Collections.Specialized;
using Elastic.Apm.SerilogEnricher;
using Elastic.Serilog.Sinks;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Logging;
using Serilog;
using Serilog.Events;

namespace Tiltwire.Services.Shared.Logging;

public static class SerilogExtension
{
    //private const string serviceName = "ServiceRegistryService";

    public static void AddLogging(this WebApplicationBuilder builder)
    {
        var cfg = new LoggerConfiguration()
            .MinimumLevel.Information()
            .Enrich.FromLogContext()
            .Enrich.WithProperty("service.name", ServiceConfig.ServiceName)
            .Enrich.WithElasticApmCorrelationInfo()
            .WriteTo.Console();

        if (ServiceConfig.ElasticNodes != string.Empty)
            cfg.WriteTo.Elasticsearch([new Uri(ServiceConfig.ElasticNodes)],
                opts =>
                {
                    opts.MinimumLevel = LogEventLevel.Debug;
                    opts.ConfigureChannel = channel =>
                    {
                        //channel.ExportResponseCallback = (response, buffer) => 
                        //    Console.WriteLine($"Written  {buffer.Count} logs to Elasticsearch: {response.ApiCallDetails.HttpStatusCode} {Utils.GetStringFromBulkResponseBites(response)}");
                    };
                },
                trans =>
                {
                    var headers = new NameValueCollection { { "Authorization", $"ApiKey {ServiceConfig.ElasticApiKey}" } };
                    trans.GlobalHeaders(headers);
                });

        Log.Logger = cfg.CreateLogger();

        builder.Logging.ClearProviders();
        builder.Host.UseSerilog();
    }
}