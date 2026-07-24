using FluentValidation;
using Microsoft.Extensions.DependencyInjection;
using System.Text.Json.Serialization;
using VertexERP.Application.Common;
using VertexERP.Application.Common.Abstractions.Handler;
using VertexERP.Application.Common.Extensions;
using VertexERP.Application.Services;

namespace VertexERP.Application;

public static class ApplicationRegistration
{
    public static IServiceCollection AddApplicationServices(this IServiceCollection services)
    {
        services.Scan(scan => scan
         .FromAssemblyOf<ApplicationAssemblyMarker>()
         .AddClasses(classes => classes.AssignableTo<IHandler>())
         .AsSelf()
         .WithScopedLifetime());

        services.ConfigureHttpJsonOptions(options =>
        {
            options.SerializerOptions.DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull;
        });


        services.AddScoped<AuthenticationService>();

        services.AddValidatorsFromAssembly(typeof(ApplicationAssemblyMarker).Assembly);

        services.AddMapsterConfigurations();

        return services;
    }
}