namespace VertexERP.API.Configuration;

public static class LoggingConfiguration
{
    public static void ConfigureBootstrapLogger()
    {
        Log.Logger = new LoggerConfiguration()
            .MinimumLevel.Information()
            .WriteTo.Console(outputTemplate: "[{Timestamp:HH:mm:ss} {Level:u3}] " +
            "{Message:lj}{NewLine}{Exception}")
            .Enrich.FromLogContext()
            .Enrich.WithExceptionDetails()
            .CreateBootstrapLogger();
    }

    public static WebApplicationBuilder ConfigureSerilog(this WebApplicationBuilder builder)
    {
        var logginSetting = new LogginSetting();

        builder.Configuration
           .GetSection("LoggingOptions")
           .Bind(loggingOptions);

        Log.Logger = new LoggerConfiguration()
        .ReadFrom.Configuration(builder.Configuration)
        .CreateLogger();

        if (logginSetting.UseConsole)
        {
            loggerConfiguration.WriteTo.Console(
               outputTemplate: "[{Timestamp:HH:mm:ss} {Level:u3}] " +
               "{Message:lj} [{CorrelationId}]{NewLine}{Exception}"
            );
        }

        if (logginSetting.UseSeq)
        {
            loggerConfiguration.WriteTo.Seq(loggingOptions.SeqUrl);
        }

        if (logginSetting.UseElasticsearch)
        {
            loggerConfiguration.WriteTo.Elasticsearch(
             new[] { new Uri(loggingOptions.ElasticsearchUrl) },
             opts =>
             {
                 opts.DataStream = new DataStreamName(
                     loggingOptions.ServiceName,
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