using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using VertexERP.Application.Common.Abstractions.Endpoint;
using VertexERP.Application.Common.Extensions;
using VertexERP.Application.Shared.Constant;
using VertexERP.Application.Shared.Results;

namespace VertexERP.Application.Modules.Identity.Authentication.Refresh;

public sealed class Endpoint : IEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapPost("/authentication/refresh", async (Handler handler, HttpContext httpContext, CancellationToken cancellationToken) =>
        {
            var refreshToken = httpContext.Request.GetRefreshToken();

            if (refreshToken is null)
                return Result<Response>.Unauthorized().ToMinimalResult();

            var result = await handler.HandleAsync(new Command(refreshToken), cancellationToken);

            if (!result.IsSuccess || result.Data is null)
                return result.ToMinimalResult();

            httpContext.Response.SetRefreshTokenCookie(result.Data.TokenPair.RefreshToken, httpContext.Request.IsHttps);

            var response = Result<Response>.Success(new Response(result.Data.User, result.Data.TokenPair.AccessToken));

            return response.ToMinimalResult();
        })
        .MapToApiVersion(1, 0)
        .WithTags(Tags.Authentication)
        .Produces<Result<Response>>(StatusCodes.Status200OK);
    }
}