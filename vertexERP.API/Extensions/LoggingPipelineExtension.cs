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

                options.GetLevel = (httpContext, _, __) =>
                    httpContext.Request.Path.StartsWithSegments("/swagger")
                        ? LogEventLevel.Verbose
                        : LogEventLevel.Information;

                options.EnrichDiagnosticContext = (diag, httpContext) =>
                {
                    var correlationId =
                        httpContext.Items["X-Correlation-Id"]?.ToString()
                        ?? httpContext.TraceIdentifier;

                    diag.Set("CorrelationId", correlationId);
                };

                options.MessageTemplate =
                    "HTTP {RequestMethod} {RequestPath} responded {StatusCode} in {Elapsed:0.0000} ms CorrelationId={CorrelationId}";
            });

            return app;
        }
    }
}
