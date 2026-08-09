using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using VertexERP.Application.Common.Abstractions.Endpoint;
using VertexERP.Application.Common.Extensions;
using VertexERP.Application.Common.Security;
using VertexERP.Application.Shared.Constant;
using VertexERP.Application.Shared.Results;

namespace VertexERP.Application.Modules.Catalog.Products.Images.Upload;

public sealed class Endpoint : IEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapPost("products/{productId:guid}/images", async (
            Guid productId,
            [FromForm] IReadOnlyList<IFormFile> images,
            Handler handler,
            CancellationToken ct) =>
        {
            var request = new Request(productId, images);

            var result = await handler.HandleAsync(request, ct);

            return result.ToMinimalResult();
        })
        .HasPermission(SecurityPerms.Catalog.Manage)
        .MapToApiVersion(1, 0)
        .WithTags(Tags.Catalog)
        .DisableAntiforgery()
        .Produces<Result<Response>>(StatusCodes.Status200OK);
    }
}