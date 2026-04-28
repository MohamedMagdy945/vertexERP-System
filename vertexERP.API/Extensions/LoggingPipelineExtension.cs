using Serilog;
using Serilog.Events;

namespace VertexERP.API.Extensions
{
    public static class LoggingPipelineExtension
    {
        public static WebApplication UseAppRequestLogging(this WebApplication app)
        {
            app.UseSerilogRequestLogging(options =>
            {
                options.EnrichDiagnosticContext = (diag, httpContext) =>
                {
                    var correlationId =
                        httpContext.Request.Headers["X-Correlation-Id"].FirstOrDefault()
                        ?? httpContext.TraceIdentifier;

                    diag.Set("CorrelationId", correlationId);
                };

                options.MessageTemplate =
                    "HTTP {RequestMethod} {RequestPath} responded {StatusCode} in {Elapsed:0.0000} ms CorrelationId={CorrelationId}";

                options.GetLevel = (httpContext, elapsed, ex) =>
                {
                    return ex != null || httpContext.Response.StatusCode >= 500
                        ? LogEventLevel.Error
                        : httpContext.Response.StatusCode >= 400
                            ? LogEventLevel.Warning
                            : LogEventLevel.Information;
                };
            });

            return app;
        }
    }
}
