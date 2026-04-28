using Serilog;
using Serilog.Events;

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

                    if (exception is not null || httpContext.Response.StatusCode >= 500)
                        return LogEventLevel.Error;

                    return httpContext.Response.StatusCode >= 400
                        ? LogEventLevel.Warning
                        : LogEventLevel.Information;
                };

                options.EnrichDiagnosticContext = (diag, httpContext) =>
                {
                    var correlationId = httpContext.Items["X-Correlation-Id"]?.ToString()
                                        ?? httpContext.TraceIdentifier;

                    diag.Set("CorrelationId", correlationId);
                    diag.Set("UserAgent", httpContext.Request.Headers["User-Agent"]); // إضافة مفيدة
                };

                options.MessageTemplate = "HTTP {RequestMethod} {RequestPath} responded {StatusCode} in {Elapsed:0.0000} ms [ID: {CorrelationId}]";
            });
        }
    }
}



