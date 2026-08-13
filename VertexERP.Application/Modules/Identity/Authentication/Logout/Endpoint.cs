using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using VertexERP.Application.Common.Abstractions.Endpoint;
using VertexERP.Application.Common.Extensions;
using VertexERP.Application.Shared.Constant;
using VertexERP.Application.Shared.Results;

namespace VertexERP.Application.Modules.Identity.Authentication.Logout;

public sealed class Endpoint : IEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapPost("/authentication/logout", HandleAsync)
            .MapToApiVersion(1, 0)
            .WithTags(Tags.Authentication)
            .Produces<Result<Response>>(StatusCodes.Status200OK);
    }
    private static async Task<IResult> HandleAsync(Handler handler,HttpContext httpContext,
        CancellationToken ct)
    {
        var refreshToken = httpContext.Request.GetRefreshToken();

        if (refreshToken is null)
            return Result<Response>.Unauthorized().ToMinimalResult();


        var result = await handler.HandleAsync(new Request(refreshToken), ct);

        return result.ToMinimalResult();
    }
}