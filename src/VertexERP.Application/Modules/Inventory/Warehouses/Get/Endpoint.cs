using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using VertexERP.Application.Common.Abstractions.Endpoint;
using VertexERP.Application.Common.Authorization;
using VertexERP.Application.Common.Extensions;
using VertexERP.Application.Shared.Constant;
using VertexERP.Application.Shared.Results;

namespace VertexERP.Application.Modules.Inventory.Warehouses.Get;

public sealed class Endpoint : IEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapGet("/warehouses", async ([AsParameters] Request request, Handler handler, CancellationToken ct) =>
        {
            var result = await handler.HandleAsync(request, ct);

            return result.ToMinimalResult();
        })
        .HasPermission(Perms.Inventory.View)
        .MapToApiVersion(1, 0)
        .WithTags(Tags.Inventory)
        .Produces<Result<Response>>(StatusCodes.Status200OK);
    }
}