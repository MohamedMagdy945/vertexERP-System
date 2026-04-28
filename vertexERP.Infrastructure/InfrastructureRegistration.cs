using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using VertexERP.Application.Identity.Interfaces;
using VertexERP.Infrastructure.Common.Settings;
using VertexERP.Infrastructure.Identity.Entities;
using VertexERP.Infrastructure.Identity.Identity;
using VertexERP.Infrastructure.Persistence.DbContext;
using VertexERP.Infrastructure.Persistence.Seeder;
using VertexERP.Infrastructure.Persistence.SeederRunner;

namespace VertexERP.Infrastructure
{
    public static class InfrastructureRegistration
    {
        public static IServiceCollection AddInfrastructureService(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlServer(configuration.GetConnectionString("DefaultConnection")));


            services
            .AddIdentity<ApplicationUser, ApplicationRole>()
            .AddEntityFrameworkStores<ApplicationDbContext>()
            .AddDefaultTokenProviders();


            services.Configure<JwtSettings>(configuration.GetSection("JwtSettings"));
            services.AddScoped<IAuthService, AuthService>();
            services.AddScoped<PermissionService>();
            services.AddScoped<JwtTokenGenerator>();
            services.AddScoped<RefreshTokenService>();


            services.AddScoped<IDataSeeder, RoleSeeder>();
            services.AddScoped<IDataSeeder, PermissionSeeder>();
            services.AddScoped<IDataSeeder, UserSeeder>();
            services.AddScoped<DataSeederRunner>();

            return services;
        }
    }
}
