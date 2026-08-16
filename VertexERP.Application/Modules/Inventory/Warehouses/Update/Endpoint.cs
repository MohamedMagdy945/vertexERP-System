using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using VertexERP.Application.Common.Abstractions.Endpoint;
using VertexERP.Application.Common.Extensions;
using VertexERP.Application.Common.Security;
using VertexERP.Application.Shared.Constant;
using VertexERP.Application.Shared.Results;

namespace VertexERP.Application.Modules.Inventory.Warehouses.Update;

public sealed class Endpoint : IEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapPut("warehouses/{id:guid}", HandleAsync)
            .AddValidation<Request>()
            .HasPermission(SecurityPerms.Inventory.Manage)
            .MapToApiVersion(1, 0)
            .WithTags(Tags.Inventory)
            .Produces<Result<Response>>(StatusCodes.Status200OK);
    }

    private static async Task<IResult> HandleAsync(Guid id, Request request, Handler handler,
        CancellationToken ct)
    {
        var result = await handler.HandleAsync(id, request, ct);

        return result.ToMinimalResult();
    }
}