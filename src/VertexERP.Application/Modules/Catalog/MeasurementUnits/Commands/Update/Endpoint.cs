using Mediator;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using VertexERP.Application.Common.Abstractions.Endpoint;
using VertexERP.Application.Common.Extensions;
using VertexERP.Shared.Results;

namespace VertexERP.Application.Modules.Catalog.Units.Command.Create;

public sealed class Endpoint : IEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapPut("/categories/{id:guid}", async (Guid id, Command command, ISender sender, CancellationToken cancellationToken) =>
        {
            var result = await sender.Send(command with { Id = id }, cancellationToken);

            return result.ToMinimalResult();
        })
        .MapToApiVersion(1, 0)
        .WithTags("Catalog")
        .Produces<Result<Response>>(StatusCodes.Status201Created);
    }
}