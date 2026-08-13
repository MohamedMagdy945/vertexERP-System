using Azure;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using VertexERP.Application.Common.Abstractions.Endpoint;
using VertexERP.Application.Common.Extensions;
using VertexERP.Application.Common.Security;
using VertexERP.Application.Shared.Constant;
using VertexERP.Application.Shared.Results;

namespace VertexERP.Application.Modules.Identity.Users.Update;

public sealed class Endpoint : IEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapPut( "users/{id:guid}",   HandleAsync)
            .HasPermission(SecurityPerms.Identity.Manage)
            .MapToApiVersion(1, 0)
            .WithTags(Tags.Identity)
            .Produces<Result<Response>>(StatusCodes.Status200OK);
    }

    private static async Task<IResult> HandleAsync(Guid id, Request request,Handler handler,
        CancellationToken ct)
    {
        var result = await handler.HandleAsync(id, request, ct);

        return result.ToMinimalResult();
    }
}