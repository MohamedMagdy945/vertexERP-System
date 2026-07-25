using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using VertexERP.Application.Common.Abstractions.Endpoint;
using VertexERP.Application.Common.Extensions;
using VertexERP.Application.Shared.Constant;

namespace VertexERP.Application.Modules.Identity.Users.Create;


public sealed class Endpoint : IEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapPost("users", async (Request command, Handler handler, CancellationToken cancellationToken) =>
        {
            var result = await handler.HandleAsync(command, cancellationToken);
            return result.ToMinimalResult();
        })
        .RequireRole(Roles.SecurityAdmin)
        .AddValidation<Request>()
        .MapToApiVersion(1, 0)
        .WithTags(Tags.Identity);
    }
}