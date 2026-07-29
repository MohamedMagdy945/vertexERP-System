using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using VertexERP.Application.Common.Abstractions.Endpoint;
using VertexERP.Application.Common.Authorization;
using VertexERP.Application.Common.Extensions;
using VertexERP.Application.Shared.Constant;
using VertexERP.Application.Shared.Results;

namespace VertexERP.Application.Modules.Catalog.Products.Create;

public sealed class Endpoint : IEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapPost("/products", async ([FromForm] Request command, Handler handler, CancellationToken cancellationToken) =>
        {
            var result = await handler.Handle(command, cancellationToken);

            return result.ToMinimalResult();
        })
        .AddValidation<Request>()
        .HasPermission(SecurityPermissions.Categories.Manage)
        .MapToApiVersion(1, 0)
        .WithTags(Tags.Catalogs)
        .DisableAntiforgery()
        .Produces<Result<Response>>(StatusCodes.Status200OK);
    }
}