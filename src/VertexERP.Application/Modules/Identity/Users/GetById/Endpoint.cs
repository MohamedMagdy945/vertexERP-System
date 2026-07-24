using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using VertexERP.Application.Common.Abstractions.Endpoint;
using VertexERP.Application.Common.Extensions;
using VertexERP.Shared.Constant;
using VertexERP.Shared.Results;

namespace VertexERP.Application.Modules.Identity.Users.GetById;

public sealed class Endpoint : IEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapGet("/users/{id:guid}", async (Guid id, Handler handler, CancellationToken cancellationToken) =>
        {
            var result = await handler.HandleAsync(new Query(id), cancellationToken);

            return result.ToMinimalResult();
        })
        .MapToApiVersion(1, 0)
        .RequireRole(Roles.Security)
        .WithTags("Identity")
        .Produces<Result<Response>>(StatusCodes.Status200OK);
    }
}