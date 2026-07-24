using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using VertexERP.Application.Common.Abstractions.Endpoint;
using VertexERP.Application.Common.Extensions;
using VertexERP.Application.Common.Models.Identity;
using VertexERP.Shared.Results;

namespace VertexERP.Application.Modules.Identity.Users.Get;

public sealed class Endpoint : IEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapGet("/users", async ([AsParameters] Query query, Handler handler, HttpContext httpContext, CancellationToken cancellationToken) =>
        {
            var result = await handler.HandleAsync(query, cancellationToken);

            return result.ToMinimalResult();
        })
        .MapToApiVersion(1, 0)
        .WithTags("Identity")
        .Produces<Result<AccessTokenResponse>>(StatusCodes.Status200OK);
    }
}