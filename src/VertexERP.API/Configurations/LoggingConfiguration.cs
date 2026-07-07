using Elastic.Ingest.Elasticsearch.DataStreams;
using Elastic.Serilog.Sinks;
using Serilog;
using Serilog.Events;
using Serilog.Exceptions;
using VertexERP.API.settings;

namespace VertexERP.API.Configuration;

public static class LoggingConfiguration
{
    public static void ConfigureBootstrapLogger()
    {
        Log.Logger = new LoggerConfiguration()
            .MinimumLevel.Information()
            .Enrich.FromLogContext()
            .Enrich.WithExceptionDetails()
            .WriteTo.Console(
                outputTemplate: "[{Timestamp:HH:mm:ss} {Level:u3}] {Message:lj}{NewLine}{Exception}")
            .CreateBootstrapLogger();
    }

    public static WebApplicationBuilder ConfigureSerilog(this WebApplicationBuilder builder)
    {
        var loggingSetting = new LoggingSetting();

        builder.Configuration
            .GetSection("LoggingOptions")
            .Bind(loggingSetting);

        var loggerConfiguration = new LoggerConfiguration()
            .ReadFrom.Configuration(builder.Configuration)
            .Enrich.FromLogContext()
            .Enrich.WithExceptionDetails();

        if (loggingSetting.UseConsole)
        {
            loggerConfiguration.WriteTo.Console(
                outputTemplate: "[{Timestamp:HH:mm:ss} {Level:u3}] " +
                "{Message:lj} [{CorrelationId}]{NewLine}{Exception}");
        }

        if (loggingSetting.UseSeq)
        {
            loggerConfiguration.WriteTo.Seq(loggingSetting.SeqUrl);
        }

        if (loggingSetting.UseElasticsearch)
        {
            loggerConfiguration.WriteTo.Elasticsearch(
                new[] { new Uri(loggingSetting.ElasticsearchUrl) },
                opts =>
                {
                    opts.DataStream = new DataStreamName(
                        loggingSetting.ServiceName,
                        "logs",
                        "application");
                });
        }

        Log.Logger = loggerConfiguration.CreateLogger();

        builder.Host.UseSerilog();

        return builder;
    }

    public static IApplicationBuilder UseCustomRequestLogging(this IApplicationBuilder app)
    {
        return app.UseSerilogRequestLogging(options =>
        {
            options.GetLevel = (ctx, _, ex) =>
                ex != null ? LogEventLevel.Error :
                ctx.Response.StatusCode >= 500 ? LogEventLevel.Error :
                ctx.Response.StatusCode >= 400 ? LogEventLevel.Warning :
                LogEventLevel.Information;

            options.MessageTemplate = "HTTP {RequestMethod} {RequestPath} -> {StatusCode} in {Elapsed:0.0000} ms";
        });
    }
}