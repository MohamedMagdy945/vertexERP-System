using FluentValidation;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace VertexERP.Application;

public static class ApplicationRegistrationService
{
    public static IServiceCollection AddApplicationServices(this IServiceCollection services)
    {

        services.AddMediatR(cfg =>
        {
            cfg.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly());

            cfg.AddOpenBehavior(typeof(Behaviors.ValidationBehavior<,>));
        });


        services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());

        return services;
    }
}

