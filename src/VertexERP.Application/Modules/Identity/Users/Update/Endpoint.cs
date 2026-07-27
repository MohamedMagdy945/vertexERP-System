using Azure;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using VertexERP.Application.Common.Abstractions.Endpoint;
using VertexERP.Application.Common.Authorization;
using VertexERP.Application.Common.Extensions;
using VertexERP.Application.Shared.Constant;
using VertexERP.Application.Shared.Results;

namespace VertexERP.Application.Modules.Identity.Users.Update;

public sealed class Endpoint : IEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapPut("/users/{id}", async (Guid id, Request request, Handler handler, CancellationToken ct) =>
        {
            var result = await handler.HandleAsync(request with { Id = id }, ct);

            return result.ToMinimalResult();
        })
        .RequireRole(SecurityRoles.SystemAdmin, SecurityRoles.SecurityAdmin)
        .MapToApiVersion(1, 0)
        .WithTags(Tags.Identity)
        .Produces<Result<Page<Response>>>(StatusCodes.Status200OK);
    }
}