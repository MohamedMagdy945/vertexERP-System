using Microsoft.Extensions.DependencyInjection;
using VertexERP.Infrastructure.Persistence.Seeding.SeederRunner;

namespace VertexERP.Infrastructure.Common.Extensions;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddDataSeeders(this IServiceCollection services)
    {
        services.Scan(scan => scan
            .FromAssemblyOf<IDataSeeder>()
            .AddClasses(classes => classes.AssignableTo<IDataSeeder>())
            .AsImplementedInterfaces()
            .WithScopedLifetime());

        return services;
    }
}
