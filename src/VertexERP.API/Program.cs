using Microsoft.AspNetCore.Authorization;
using Serilog;
using VertexERP.API.Authorization;
using VertexERP.API.Configurations;
using VertexERP.API.Middlewares;
using VertexERP.Application;
using VertexERP.Infrastructure;

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
            builder.Services.AddControllers();
            builder.Services.AddApiVersioningConfiguration();

            builder.Services.AddExceptionHandler<GlobalExceptionHandler>();
            builder.Services.AddProblemDetails();
            builder.Services.AddHttpContextAccessor();
            builder.Services.AddSingleton<IAuthorizationHandler,
             PermissionAuthorizationHandler>();

            builder.Services.AddSingleton<IAuthorizationPolicyProvider,
                PermissionPolicyProvider>();

            builder.Services.AddApplicationServices();

            builder.Services.AddInfrastructureServices(builder.Configuration);

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            app.UseSwaggerDocumentation();

            app.UseMiddleware<CorrelationIdMiddleware>();

            app.UseExceptionHandler();

            app.UseMiddleware<RequestResponseLoggingMiddleware>();

            app.UseAuthorization();


            app.MapControllers();

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
