using Serilog;
using Serilog.Events;
using Serilog.Exceptions;

namespace VertexERP.API.Extensions
{
    public static class LoggingExtension
    {

        public static ConfigureHostBuilder UseSerilogLogging(
            this ConfigureHostBuilder host,
            IConfiguration configuration)
        {

            Log.Logger = new LoggerConfiguration()
            .MinimumLevel.Information()
            .MinimumLevel.Override("Microsoft", LogEventLevel.Warning)
            .MinimumLevel.Override("System", LogEventLevel.Warning)
            .WriteTo.Console()
            .CreateBootstrapLogger();

            host.UseSerilog((context, services, config) =>
            {
                config
                    .ReadFrom.Configuration(context.Configuration)
                    .ReadFrom.Services(services)
                    .Enrich.WithExceptionDetails();


            });

            return host;
        }
    }
}
