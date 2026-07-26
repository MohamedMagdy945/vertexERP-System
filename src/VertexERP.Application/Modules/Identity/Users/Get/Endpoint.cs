using Azure;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using VertexERP.Application.Common.Abstractions.Endpoint;
using VertexERP.Application.Common.Extensions;
using VertexERP.Application.Shared.Constant;
using VertexERP.Application.Shared.Results;

namespace VertexERP.Application.Modules.Identity.Users.Get;

public sealed class Endpoint : IEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapGet("/users", async ([AsParameters] Request request, Handler handler, HttpContext httpContext, CancellationToken cancellationToken) =>
        {
            var result = await handler.HandleAsync(request, cancellationToken);

            return result.ToMinimalResult();
        })
        .RequireRole(RoleNames.SecurityAdmin)
        .MapToApiVersion(1, 0)
        .WithTags(Tags.Identity)
        .Produces<Result<Page<Response>>>(StatusCodes.Status200OK);
    }
}