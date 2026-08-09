using Asp.Versioning;
using VertexERP.Application.Common;
using VertexERP.Application.Common.Abstractions.Endpoint;

namespace VertexERP.API.Extensions;

public static class EndpointExtensions
{
    public static WebApplication MapEndpoints(this WebApplication app)
    {
        var versionSet = app.NewApiVersionSet()
            .HasApiVersion(new ApiVersion(1, 0))
            .HasApiVersion(new ApiVersion(2, 0))
            .ReportApiVersions()
            .Build();

        var versionedGroup = app.MapGroup("api/v{version:apiVersion}")
                                .WithApiVersionSet(versionSet);

        var endpointTypes = typeof(ApplicationAssemblyMarker).Assembly
            .DefinedTypes
            .Where(type => typeof(IEndpoint).IsAssignableFrom(type)
                           && !type.IsInterface
                           && !type.IsAbstract);

        foreach (var type in endpointTypes)
        {
            var endpoint = Activator.CreateInstance(type) as IEndpoint;

            endpoint?.MapEndpoint(versionedGroup);
        }

        return app;
    }
}