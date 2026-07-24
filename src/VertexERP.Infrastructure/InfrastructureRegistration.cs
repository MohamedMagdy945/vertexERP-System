using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using VertexERP.Application.Common.Abstractions.Cache;
using VertexERP.Application.Common.Abstractions.Http;
using VertexERP.Application.Common.Abstractions.Identity;
using VertexERP.Application.Common.Abstractions.Persistence;
using VertexERP.Application.Common.Abstractions.Storage;
using VertexERP.Infrastructure.Common.Extensions;
using VertexERP.Infrastructure.Common.Settings;
using VertexERP.Infrastructure.Persistence;
using VertexERP.Infrastructure.Services.Cache;
using VertexERP.Infrastructure.Services.Http;
using VertexERP.Infrastructure.Services.Identity.Authentication;
using VertexERP.Infrastructure.Services.Identity.UserPermission;
using VertexERP.Infrastructure.Services.Storage;

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
        services.AddSingleton<IRefreshTokenHasher, RefreshTokenHasher>();

        services.AddSingleton<IClientInfoProvider, ClientInfoProvider>();

        services.AddSingleton<IFileStorage, LocalFileStorage>();

        services.AddJwtAuthentication(configuration);

        services.AddMemoryCache();

        services.AddScoped<IUserPermissionCache, MemoryUserPermissionCache>();
        services.AddScoped<IUserPermissionService, UserPermissionService>();


        return services;
    }
}