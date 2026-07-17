using FluentValidation;
using Microsoft.Extensions.DependencyInjection;
using VertexERP.Application.Common;

namespace VertexERP.Application;

public static class ApplicationRegistration
{
    public static IServiceCollection AddApplicationServices(this IServiceCollection services)
    {
        services.AddMediator(options =>
        {
            options.ServiceLifetime = ServiceLifetime.Scoped;

            options.PipelineBehaviors =
              [
                  typeof(ValidationBehavior<,>)
              ];
        });


        services.AddValidatorsFromAssembly(typeof(ApplicationAssemblyMarker).Assembly);

        return services;
    }
}