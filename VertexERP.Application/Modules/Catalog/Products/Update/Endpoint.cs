using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using VertexERP.Application.Common.Abstractions.Endpoint;
using VertexERP.Application.Common.Extensions;
using VertexERP.Application.Common.Security;
using VertexERP.Application.Shared.Constant;
using VertexERP.Application.Shared.Results;

namespace VertexERP.Application.Modules.Catalog.Products.Update;

public sealed class Endpoint : IEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapPut("/products/{id:guid}", async (Guid id, Request request, Handler handler, CancellationToken ct) =>
        {
            var result = await handler.HandleAsync(request with { Id = id }, ct);

            return result.ToMinimalResult();
        })
        .AddValidation<Request>()
        .HasPermission(SecurityPerms.Catalog.Manage)
        .MapToApiVersion(1, 0)
        .WithTags(Tags.Catalog)
        .Produces<Result<Response>>(StatusCodes.Status200OK);
    }
}