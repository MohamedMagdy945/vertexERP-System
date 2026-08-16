using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using VertexERP.Application.Common.Abstractions.Endpoint;
using VertexERP.Application.Common.Extensions;
using VertexERP.Application.Common.Security;
using VertexERP.Application.Shared.Constant;

namespace VertexERP.Application.Modules.Catalog.Categories.Create;

public sealed class Endpoint : IEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapPost("categories", HandleAsync)
            .AddValidation<Request>()
            .HasPermission(SecurityPerms.Catalog.Manage)
            .MapToApiVersion(1, 0)
            .WithTags(Tags.Identity)
            .DisableAntiforgery();
    }

    private static async Task<IResult> HandleAsync([FromForm] Request request, Handler handler,
       CancellationToken ct)
    {
        var result = await handler.HandleAsync(request, ct);

        return result.ToMinimalResult();
    }
}