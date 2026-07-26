using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using VertexERP.Application.Common.Abstractions.Endpoint;
using VertexERP.Application.Common.Extensions;
using VertexERP.Application.Shared.Constant;
using VertexERP.Application.Shared.Results;

namespace VertexERP.Application.Modules.Identity.Authentication.Login;

public sealed class Endpoint : IEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapPost("/authentication/login", async (Request request, Handler handler, HttpContext httpContext, CancellationToken ct) =>
        {
            var result = await handler.HandleAsync(request, ct);

            if (!result.IsSuccess || result.Data is null)
                return result.ToMinimalResult();

            httpContext.Response.SetRefreshTokenCookie(result.Data.TokenPair.RefreshToken, httpContext.Request.IsHttps);

            var response = new Response
            {
                User = result.Data.User,
                AccessToken = result.Data.TokenPair.AccessToken,
            };

            return Result<Response>.Success(response).ToMinimalResult();
        })
        .AddValidation<Request>()
        .MapToApiVersion(1, 0)
        .WithTags(Tags.Authentication)
        .Produces<Result<Response>>(StatusCodes.Status200OK);
    }
}