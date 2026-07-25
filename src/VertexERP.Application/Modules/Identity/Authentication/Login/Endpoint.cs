using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using VertexERP.Application.Common.Abstractions.Endpoint;
using VertexERP.Application.Common.Extensions;
using VertexERP.Application.Shared.Results;

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

            httpContext.Response.SetRefreshTokenCookie(result.Data.TokenPair.RefreshToken, httpContext.Request.IsHttps);

            var response = Result<Response>.Success(new Response(result.Data.User, result.Data.TokenPair.AccessToken));

            return response.ToMinimalResult();
        })
        .AddValidation<Command>()
        .MapToApiVersion(1, 0)
        .WithTags("Authentication")
        .Produces<Result<Response>>(StatusCodes.Status200OK);
    }
}