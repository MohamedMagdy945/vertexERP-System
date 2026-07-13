using Microsoft.AspNetCore.Authorization;
using VertexERP.API.Authorization;
using VertexERP.API.Configuration;
using VertexERP.API.Configurations;
using VertexERP.API.Exceptions;
using VertexERP.API.middlewares;
using VertexERP.Application;
using VertexERP.Infrastructure;

namespace VertexERP.API;

public class Program
{
    public static async Task Main(string[] args)
    {
        LoggingConfiguration.ConfigureBootstrapLogger();

        var builder = WebApplication.CreateBuilder(args);

        builder.ConfigureSerilog();

        builder.Services.AddExceptionHandler<GlobalExceptionHandler>();
        builder.Services.AddProblemDetails();
        // Add services to the container.

        builder.Services.AddControllers();

        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerConfiguration();

        builder.Services.AddHttpContextAccessor();
        builder.Services.AddAuthorization();

        builder.Services.AddSingleton<IAuthorizationHandler,
               PermissionAuthorizationHandler>();

        builder.Services.AddSingleton<IAuthorizationPolicyProvider,
            PermissionPolicyProvider>();



        builder.Services.AddApplicationServices();
        builder.Services.AddInfrastructureServices(builder.Configuration);

        var app = builder.Build();


        //using (var scope = app.Services.CreateScope())
        //{
        //    //var seeder = scope.ServiceProvider.GetRequiredService<DatabaseSeeder>();
        //    //await seeder.SeedAsync();
        //}

        app.UseSwaggerDocumentation();

        app.UseMiddleware<CorrelationIdMiddleware>();

        app.UseExceptionHandler();

        app.UseCustomRequestLogging();

        // Configure the HTTP request pipeline.
        app.UseAuthentication();

        app.UseAuthorization();

        app.MapControllers();

        app.Run();
    }
}

