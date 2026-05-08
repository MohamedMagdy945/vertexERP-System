using VertexERP.API.Configurations;
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

            builder.Services.AddControllers(options =>
            {
                options.RespectBrowserAcceptHeader = true;
            });

            builder.Services.AddSwaggerDocumentation();
            builder.Services.AddApiVersioningConfig();
            builder.Services.AddApplicationService();
            builder.Services.AddInfrastructureService(builder.Configuration);


            var app = builder.Build();


            app.UseMiddleware<CorrelationIdMiddleware>();
            app.UseAppRequestLogging();
            app.UseMiddleware<ErrorHandlerMiddleware>();



            app.UseStaticFiles();
            if (app.Environment.IsDevelopment())
            {
                app.UseSwaggerDocumentation();

                using (var scope = app.Services.CreateScope())
                {
                    var runner = scope.ServiceProvider.GetRequiredService<DataSeederRunner>();
                    await runner.SeedAsync();
                }
            }

            app.UseRouting();

            app.UseAuthentication();

            app.UseAuthorization();

            app.MapControllers();

            app.Run();
        }
    }
}