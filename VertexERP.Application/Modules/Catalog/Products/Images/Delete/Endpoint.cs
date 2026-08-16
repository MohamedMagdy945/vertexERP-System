using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using VertexERP.Application.Common.Abstractions.Endpoint;
using VertexERP.Application.Common.Extensions;
using VertexERP.Application.Common.Security;
using VertexERP.Application.Shared.Constant;
using VertexERP.Application.Shared.Results;

namespace VertexERP.Application.Modules.Catalog.Products.Images.Delete;

public sealed class Endpoint : IEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapDelete("products/{productId:guid}/images/{imageId:guid}", HandleAsync) 
        .HasPermission(SecurityPerms.Catalog.Manage)
        .MapToApiVersion(1, 0)
        .WithTags(Tags.Catalog)
        .Produces<Result<Response>>(StatusCodes.Status200OK);
    }
    private static async Task<IResult> HandleAsync(Guid productId,Guid imageId, Handler handler,
        CancellationToken ct)
    {
        var result = await handler.HandleAsync(new Request(productId,imageId), ct);

        return result.ToMinimalResult();
    }

}