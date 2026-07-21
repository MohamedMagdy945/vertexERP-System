using Mediator;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using VertexERP.Application.Common.Abstractions.Endpoint;
using VertexERP.Application.Common.Extensions;
using VertexERP.Shared.Results;

namespace VertexERP.Application.Modules.Catalog.Units.Queries.GetById;

public sealed class Endpoint : IEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapGet("/categories/{id:guid}", async (Guid id, ISender sender, CancellationToken cancellationToken) =>
        {
            var result = await sender.Send(new Query(id), cancellationToken);

            return result.ToMinimalResult();
        })
        .MapToApiVersion(1, 0)
        .WithTags("Inventory")
        .Produces<Result<Response>>(StatusCodes.Status201Created);
    }
}