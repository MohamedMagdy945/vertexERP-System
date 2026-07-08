using VertexERP.API.Configuration;
using VertexERP.API.Exceptions;
using VertexERP.API.middlewares;

namespace VertexERP.API;

public class Program
{
    public static async Task Main(string[] args)
    {
        LoggingConfiguration.ConfigureBootstrapLogger();

        var builder = WebApplication.CreateBuilder(args);

        builder.ConfigureSerilog();

        builder.Services.AddExceptionHandler<GlobalExceptionHandler>();

        // Add services to the container.

        builder.Services.AddControllers();

        var app = builder.Build();

        app.UseMiddleware<CorrelationIdMiddleware>();

        app.UseExceptionHandler();

        app.UseCustomRequestLogging();

        // Configure the HTTP request pipeline.

        app.UseAuthorization();

        app.MapControllers();

        app.Run();
    }
}

