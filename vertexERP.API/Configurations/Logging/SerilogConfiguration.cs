using Serilog;
using Serilog.Events;
using Serilog.Exceptions;

namespace VertexERP.API.Configurations.Logging
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
}

