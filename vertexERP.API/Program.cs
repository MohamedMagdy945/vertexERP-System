using CorrelationId;
using CorrelationId.DependencyInjection;
using Serilog;
using Serilog.Events;
using Serilog.Exceptions;
using VertexERP.Infrastructure;

namespace VertexERP.API
{
    public class Program
    {
        public static void Main(string[] args)
        {
            Log.Logger = new LoggerConfiguration()
            .MinimumLevel.Information()
            .MinimumLevel.Override("Microsoft", LogEventLevel.Warning)
            .MinimumLevel.Override("System", LogEventLevel.Warning)
            .WriteTo.Console()
            .CreateBootstrapLogger();

            var builder = WebApplication.CreateBuilder(args);

            builder.Host.UseSerilog((context, services, config) =>
            {
                config
                    .ReadFrom.Configuration(context.Configuration)
                    .ReadFrom.Services(services)
                    .Enrich.WithExceptionDetails();
            });
            builder.Services.AddDefaultCorrelationId(options =>
            {
                options.AddToLoggingScope = true;
                options.UpdateTraceIdentifier = true;
                options.RequestHeader = "X-Correlation-Id";
                options.ResponseHeader = "X-Correlation-Id";
                options.IncludeInResponse = true;
            });
            builder.Services.AddControllers();

            builder.Services.AddOpenApi();

            builder.Services.AddInfrastructureService(builder.Configuration);

            var app = builder.Build();
            app.UseCorrelationId();

            app.UseSerilogRequestLogging(options =>
            {
                options.EnrichDiagnosticContext = (diag, httpContext) =>
                {
                    diag.Set("CorrelationId", httpContext.TraceIdentifier);
                };
                options.MessageTemplate =
                    "HTTP {RequestMethod} {RequestPath} responded {StatusCode} in {Elapsed:0.0000} ms CorrelationId={CorrelationId}";

                options.GetLevel = (httpContext, elapsed, ex) =>
                {
                    if (ex != null || httpContext.Response.StatusCode >= 500)
                        return LogEventLevel.Error;

                    if (httpContext.Response.StatusCode >= 400)
                        return LogEventLevel.Warning;

                    return LogEventLevel.Information;
                };
            });

            // Development only
            if (app.Environment.IsDevelopment())
            {
                app.MapOpenApi();
            }

            app.UseRouting();

            // app.UseAuthentication();

            app.UseAuthorization();

            app.MapControllers();

            app.Run();
        }
    }
}