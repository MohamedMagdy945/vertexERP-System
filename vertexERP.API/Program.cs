using CorrelationId;
using VertexERP.API.Extensions;
using VertexERP.Infrastructure;

namespace VertexERP.API
{
    public class Program
    {
        public static void Main(string[] args)
        {

            var builder = WebApplication.CreateBuilder(args);

            builder.Host.UseSerilogLogging(builder.Configuration);

            builder.Services.AddCorrelationIdSetup();

            builder.Services.AddControllers();

            builder.Services.AddOpenApi();

            builder.Services.AddInfrastructureService(builder.Configuration);

            var app = builder.Build();
            app.UseCorrelationId();

            app.UseAppRequestLogging();

            if (app.Environment.IsDevelopment())
            {
                app.MapOpenApi();
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseRouting();

            // app.UseAuthentication();

            app.UseAuthorization();

            app.MapControllers();

            app.Run();
        }
    }
}