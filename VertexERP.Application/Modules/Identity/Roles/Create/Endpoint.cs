using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using VertexERP.Application.Common.Abstractions.Endpoint;
using VertexERP.Application.Common.Extensions;
using VertexERP.Application.Common.Security;
using VertexERP.Application.Shared.Constant;

namespace VertexERP.Application.Modules.Identity.Roles.Create;

public sealed class Endpoint : IEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapPost("roles", HandleAsync)
            .HasPermission(SecurityPerms.Identity.Manage)
            .AddValidation<Request>()
            .MapToApiVersion(1, 0)
            .WithTags(Tags.Identity);
    }

    private static async Task<IResult> HandleAsync(Request command, Handler handler,
        CancellationToken ct)
    {
        var result = await handler.HandleAsync(command, ct);

        return result.ToMinimalResult();
    }
}