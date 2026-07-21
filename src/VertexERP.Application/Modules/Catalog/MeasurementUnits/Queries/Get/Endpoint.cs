using Mediator;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using VertexERP.Application.Common.Abstractions.Endpoint;
using VertexERP.Application.Common.Extensions;
using VertexERP.Application.Modules.Catalog.Units.Commands.Create;
using VertexERP.Shared.Results;

namespace VertexERP.Application.Modules.Catalog.Units.Command.Create;

public sealed class Endpoint : IEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapGet("/categories", async ([AsParameters] Query query, ISender sender, CancellationToken cancellationToken) =>
        {
            var result = await sender.Send(query, cancellationToken);

            return result.ToMinimalResult();
        })
        .MapToApiVersion(1, 0)
        .WithTags("Inventory")
        .Produces<Result<Response>>(StatusCodes.Status201Created);
    }
}