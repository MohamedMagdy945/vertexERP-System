using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using VertexERP.Application.Common.Abstractions.Endpoint;
using VertexERP.Application.Common.Authorization;
using VertexERP.Application.Common.Extensions;
using VertexERP.Application.Shared.Constant;
using VertexERP.Application.Shared.Results;

namespace VertexERP.Application.Modules.Inventory.Warehouses.Delete;

public sealed class Endpoint : IEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapDelete("/categories/{id:guid}", async (Guid id, Handler handler, CancellationToken ct) =>
        {
            var result = await handler.HandleAsync(new Request(id), ct);

            return result.ToMinimalResult();
        })
        .HasPermission(Perms.Inventory.Manage)
        .MapToApiVersion(1, 0)
        .WithTags(Tags.Catalogs)
        .Produces<Result<Response>>(StatusCodes.Status200OK);
    }
}