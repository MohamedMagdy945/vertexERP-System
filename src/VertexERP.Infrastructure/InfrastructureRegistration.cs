using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using VertexERP.Application.Common.Abstractions.Http;
using VertexERP.Application.Common.Abstractions.Identity;
using VertexERP.Application.Common.Abstractions.Persistence;
using VertexERP.Infrastructure.Http.Services;
using VertexERP.Infrastructure.Identity.Authentication;
using VertexERP.Infrastructure.Identity.Configuration;
using VertexERP.Infrastructure.Persistence;

namespace VertexERP.Infrastructure;

public static class InfrastructureRegistration
{
    public static IServiceCollection AddInfrastructureServices(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContext<IApplicationDbContext, ApplicationDbContext>(options =>
        {
            options.UseSqlServer(configuration.GetConnectionString("DefaultConnection"));
        });

        services.Configure<TokenPairSettings>(configuration.GetRequiredSection(nameof(TokenPairSettings)));

        services.AddSingleton<ITokenPairGenerator, TokenPairGenerator>();

        services.AddSingleton<IPasswordHasher, PasswordHasher>();

        services.AddSingleton<IClientInfoProvider, ClientInfoProvider>();


        services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer();


        return services;
    }
}