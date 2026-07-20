using Mapster;
using Mediator;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using VertexERP.Application.Common.Abstractions.Endpoint;
using VertexERP.Application.Common.Extensions;
using VertexERP.Application.Common.Models.Identity;

namespace VertexERP.Application.Modules.Identity.Authentication.Login;

public sealed class Endpoint : IEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapPost("/authentication/login", async (Command command, ISender sender, HttpContext httpContext, CancellationToken cancellationToken) =>
        {
            var result = await sender.Send(command, cancellationToken);

            if (!result.IsSuccess || result.Data is null)
                return result.ToMinimalResult();

            httpContext.Response.SetRefreshTokenCookie(result.Data.RefreshToken, result.Data.RefreshTokenExpiresAt, httpContext.Request.IsHttps);

            var response = result.Data.Adapt<AccessTokenResponse>();

            return Results.Ok(response);
        })
        .WithName("Login")
        .MapToApiVersion(1, 0)
        .WithTags("Authentication")
        .Produces<AccessTokenResponse>(StatusCodes.Status200OK)
        .Produces<ProblemDetails>(StatusCodes.Status401Unauthorized);
    }
}