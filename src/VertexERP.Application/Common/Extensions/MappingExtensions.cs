using Mapster;
using Microsoft.Extensions.DependencyInjection;

namespace VertexERP.Application.Common.Extensions;

public static class MappingExtensions
{
    public static IServiceCollection AddMapsterConfigurations(this IServiceCollection services)
    {
        var config = TypeAdapterConfig.GlobalSettings;

        config.Scan(typeof(ApplicationAssemblyMarker).Assembly);

        services.AddSingleton(config);

        return services;
    }
}