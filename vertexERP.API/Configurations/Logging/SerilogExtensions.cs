using Serilog;
using Serilog.Events;
using VertexERP.API.Middleware;

namespace VertexERP.API.Configurations.Logging
{
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



