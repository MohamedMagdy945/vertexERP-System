using VertexERP.API.Configuration;

namespace VertexERP.API;

public class Program
{
    public static async Task Main(string[] args)
    {
        LoggingConfiguration.ConfigureBootstrapLogger();

        var builder = WebApplication.CreateBuilder(args);

        builder.ConfigureSerilog();


        // Add services to the container.

        builder.Services.AddControllers();

        var app = builder.Build();

        app.UseCustomRequestLogging();


        // Configure the HTTP request pipeline.

        app.UseAuthorization();


        app.MapControllers();

        app.Run();
    }
}

