using FluentValidation;
using Microsoft.Extensions.DependencyInjection;
using VertexERP.Application.Common;

namespace VertexERP.Infrastructure;

public static class ApplicationRegistration
{
    public static IServiceCollection AddInfrastructureServices(this IServiceCollection services)
    {
        services.AddValidatorsFromAssembly(typeof(ApplicationAssemblyMarker).Assembly);

        return services;
    }
}