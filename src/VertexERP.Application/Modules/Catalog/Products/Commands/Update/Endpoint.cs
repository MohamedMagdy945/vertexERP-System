using Mediator;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using VertexERP.Application.Common.Abstractions.Endpoint;
using VertexERP.Application.Common.Extensions;
using VertexERP.Shared.Results;

namespace VertexERP.Application.Modules.Catalog.Products.Commands.Update;

public sealed class Endpoint : IEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapPut("/products/{id:guid}", async (Guid id, Command command, ISender sender, CancellationToken cancellationToken) =>
        {
            var commandWithId = command with { Id = id };

            var result = await sender.Send(command, cancellationToken);

            return result.ToMinimalResult();
        })
        .MapToApiVersion(1, 0)
        .WithTags("Catalog")
        .Produces<Result<Response>>(StatusCodes.Status201Created);
    }
}