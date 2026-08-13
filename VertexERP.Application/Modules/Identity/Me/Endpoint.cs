using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using VertexERP.Application.Common.Abstractions.Endpoint;
using VertexERP.Application.Common.Extensions;
using VertexERP.Application.Shared.Constant;
using VertexERP.Application.Shared.Results;

namespace VertexERP.Application.Modules.Identity.Me;

public sealed class Endpoint : IEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapGet("/me", HandleAsync)
            .RequireAuthorization()
            .MapToApiVersion(1, 0)
            .WithTags(Tags.Identity)
            .Produces<Result<Response>>(StatusCodes.Status200OK);
    }

    private static async Task<IResult> HandleAsync(Handler handler,
        CancellationToken ct)
    {
        var result = await handler.HandleAsync(ct);

        return result.ToMinimalResult();
    }
}