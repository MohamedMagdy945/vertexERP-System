using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using VertexERP.Application.Common.Abstractions.Endpoint;
using VertexERP.Application.Common.Extensions;
using VertexERP.Application.Shared.Results;

namespace VertexERP.Application.Modules.Identity.Users.Me;

public sealed class Endpoint : IEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapGet("/users/me", async (Handler handler, CancellationToken cancellationToken) =>
        {
            var result = await handler.HandleAsync(cancellationToken);

            return result.ToMinimalResult();
        })
        .MapToApiVersion(1, 0)
        .RequireAuthorization()
        .WithTags("Identity")
        .Produces<Result<Response>>(StatusCodes.Status200OK);
    }
}