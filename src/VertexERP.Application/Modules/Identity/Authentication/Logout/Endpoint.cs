using Mediator;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using VertexERP.Application.Common.Abstractions.Endpoint;
using VertexERP.Application.Common.Extensions;
using VertexERP.Shared.Results;

namespace VertexERP.Application.Modules.Identity.Authentication.Logout;

public sealed class Endpoint : IEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapPost("/authentication/logout", async (ISender sender, HttpContext httpContext, CancellationToken cancellationToken) =>
        {
            var refreshToken = httpContext.Request.GetRefreshToken();

            if (refreshToken is null)
                return Result<Response>.Unauthorized().ToMinimalResult();

            var result = await sender.Send(new Command(refreshToken), cancellationToken);

            return result.ToMinimalResult();

        })
        .WithName("Logout")
        .MapToApiVersion(1, 0)
        .WithTags("Authentication")
        .Produces<Result<Response>>(StatusCodes.Status200OK);
    }
}