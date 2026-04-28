using Microsoft.OpenApi;
using VertexERP.API.Configurations.Logging;
using VertexERP.API.Configurations.Versioning;
using VertexERP.API.Middleware;
using VertexERP.Application;
using VertexERP.Infrastructure;
using VertexERP.Infrastructure.Persistence.SeederRunner;

namespace VertexERP.API
{
    public class Program
    {
        public static async Task Main(string[] args)
        {

            var builder = WebApplication.CreateBuilder(args);



            builder.Host.AddSerilogLogging(builder.Configuration);


            builder.Services.AddControllers();

            builder.Services.AddSwaggerGen(options =>
            {
                options.SwaggerDoc("v1", new OpenApiInfo
                {
                    Title = "VertexERP System",
                    Version = "v1",
                    Description = "ERP System",
                    Contact = new OpenApiContact
                    {
                        Name = "Mohamed Magdy",
                        Email = "mohamedmagdy000022@gmail.com"
                    }
                });
            });
            builder.Services.AddApiVersioningConfig();
            builder.Services.AddApplicationService();
            builder.Services.AddInfrastructureService(builder.Configuration);

            var app = builder.Build();

            app.UseMiddleware<CorrelationIdMiddleware>();
            app.UseMiddleware<ErrorHandlerMiddleware>();


            app.UseAppRequestLogging();

            using (var scope = app.Services.CreateScope())
            {
                var runner = scope.ServiceProvider.GetRequiredService<DataSeederRunner>();
                await runner.SeedAsync();
            }
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI(c =>
                {
                    c.DocumentTitle = "Vertex ERP System";
                    c.DisplayRequestDuration();
                    c.EnableTryItOutByDefault();
                });
            }

            app.UseRouting();

            // app.UseAuthentication();

            app.UseAuthorization();

            app.MapControllers();

            app.Run();
        }
    }
}