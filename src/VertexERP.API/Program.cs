using Serilog;
using VertexERP.API.Configurations;
using VertexERP.API.Middlewares;

namespace VertexERP.API;

public class Program
{
    public static void Main(string[] args)
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

            builder.Services.AddExceptionHandler<GlobalExceptionHandler>();
            builder.Services.AddProblemDetails();

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
