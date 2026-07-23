using Mapster;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using VertexERP.Application.Common.Abstractions.Endpoint;
using VertexERP.Application.Common.Extensions;
using VertexERP.Application.Common.Models.Identity;
using VertexERP.Shared.Results;

namespace VertexERP.Application.Modules.Identity.Authentication.Login;

public sealed class Endpoint : IEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapPost("/authentication/login", async (Command command, Handler handler, HttpContext httpContext, CancellationToken cancellationToken) =>
        {
            var result = await handler.HandleAsync(command, cancellationToken);


            if (!result.IsSuccess || result.Data is null)
                return result.ToMinimalResult();

            httpContext.Response.SetRefreshTokenCookie(result.Data.RefreshToken, result.Data.RefreshTokenExpiresAt, httpContext.Request.IsHttps);

            var response = result.Data.Adapt<AccessTokenResponse>();

            return Results.Ok(response);
        })
        .AddValidation<Command>()
        .WithName("Login")
        .MapToApiVersion(1, 0)
        .WithTags("Authentication")
        .Produces<Result<AccessTokenResponse>>(StatusCodes.Status200OK);
    }
}