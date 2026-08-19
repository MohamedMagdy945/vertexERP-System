using Serilog;
using VertexERP.API.Configurations;
using VertexERP.API.Extensions;
using VertexERP.API.Middlewares;
using VertexERP.Application;
using VertexERP.Infrastructure;
using VertexERP.Infrastructure.Common.Extensions;
using VertexERP.Infrastructure.SignalR;

namespace VertexERP.API;

public class Program
{
    public static async Task Main(string[] args)
    {
        LoggingConfiguration.ConfigureBootstrapLogger();

        try
        {
            Log.Information("Starting VertexERP API");

            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.AddSerilogLogging();
            builder.Services.AddSwaggerConfiguration();
            builder.Services.AddApiVersioningConfiguration();

            builder.Services.AddExceptionHandler<GlobalExceptionHandler>();
            builder.Services.AddProblemDetails();
            builder.Services.AddHttpContextAccessor();

            builder.Services.AddApplicationServices();

            builder.Services.AddInfrastructureServices(builder.Configuration);

            var app = builder.Build();

            // Configure the HTTP request pipeline.

            app.UseSwaggerDocumentation();

            await app.SeedDataAsync();

            app.UseMiddleware<CorrelationIdMiddleware>();

            app.UseExceptionHandler();

            app.UseMiddleware<RequestResponseLoggingMiddleware>();

            app.UseAuthentication();

            app.UseAuthorization();

            app.MapEndpoints();

            app.MapHub<NotificationHub>("/hubs/notifications");

            foreach (var endpointDataSource in app.Services.GetServices<EndpointDataSource>())
            {
                foreach (var endpoint in endpointDataSource.Endpoints)
                {
                    try
                    {
                        _ = endpoint.Metadata;
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine($"❌ CRASHING ENDPOINT FOUND: {endpoint.DisplayName}");
                        throw;
                    }
                }
            }

            app.Run();
        }
        catch (Exception ex)
        {
            Log.Fatal(ex, "Application terminated unexpectedly");
        }
        finally
        {
            Log.CloseAndFlush();
        }
    }
}
