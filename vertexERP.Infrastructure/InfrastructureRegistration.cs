using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using VertexERP.Infrastructure.Common.Settings;
using VertexERP.Infrastructure.Persistence.DbContext;

namespace VertexERP.Infrastructure
{
    public static class InfrastructureRegistration
    {
        public static IServiceCollection AddInfrastructureService(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlServer(configuration.GetConnectionString("DefaultConnection")));

            services.Configure<JwtSettings>(configuration.GetSection("JwtSettings"));
            return services;
        }
    }
}
