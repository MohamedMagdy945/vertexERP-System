using Serilog;
using Serilog.Events;
using Serilog.Exceptions;
using VertexERP.API.Middleware;

namespace VertexERP.API.Configurations
{
    public static class SerilogConfiguration
    {
        public static ConfigureHostBuilder AddSerilogLogging(
            this ConfigureHostBuilder host,
            IConfiguration configuration)
        {
            Log.Logger = new LoggerConfiguration()
                .MinimumLevel.Information()
                .MinimumLevel.Override("Microsoft", LogEventLevel.Warning)
                .MinimumLevel.Override("System", LogEventLevel.Warning)
                .Enrich.FromLogContext()
                .Enrich.WithExceptionDetails()
                .CreateBootstrapLogger();

            host.UseSerilog((context, services, config) =>
            {
                config
                    .ReadFrom.Configuration(context.Configuration)
                    .ReadFrom.Services(services)
                    .MinimumLevel.Information()
                    .MinimumLevel.Override("Microsoft", LogEventLevel.Warning)
                    .MinimumLevel.Override("System", LogEventLevel.Warning)
                    .Enrich.FromLogContext()
                    .Enrich.WithExceptionDetails();
            });

            return host;
        }

    }
    public static class SerilogExtensions
    {
        public static IApplicationBuilder UseAppRequestLogging(this IApplicationBuilder app)
        {
            return app.UseSerilogRequestLogging(options =>
            {
                options.GetLevel = (httpContext, _, exception) =>
                {
                    if (httpContext.Request.Path.StartsWithSegments("/swagger"))
                        return LogEventLevel.Verbose;


                    if (exception is not null)
                        return LogEventLevel.Verbose;

                    return httpContext.Response.StatusCode switch
                    {
                        >= 500 => LogEventLevel.Error,
                        >= 400 => LogEventLevel.Warning,
                        _ => LogEventLevel.Information
                    };
                };

                options.EnrichDiagnosticContext = (diag, httpContext) =>
                {
                    var correlationId =
                         httpContext.Items[CorrelationIdMiddleware.HeaderName] as string
                         ?? httpContext.TraceIdentifier;

                    diag.Set("CorrelationId", correlationId);

                };

                options.MessageTemplate = "HTTP {RequestMethod} {RequestPath} responded {StatusCode} in {Elapsed:0.0000} ms [ID: {CorrelationId}]";
            });
        }
    }
}

