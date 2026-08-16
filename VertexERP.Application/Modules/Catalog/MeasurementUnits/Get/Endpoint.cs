using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using VertexERP.Application.Common.Abstractions.Endpoint;
using VertexERP.Application.Common.Extensions;
using VertexERP.Application.Common.Security;
using VertexERP.Application.Shared.Constant;
using VertexERP.Application.Shared.Results;

namespace VertexERP.Application.Modules.Catalog.MeasurementUnits.Get;

public sealed class Endpoint : IEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapGet("measurement-units", HandleAsync)
        .HasPermission(SecurityPerms.Catalog.View)
        .MapToApiVersion(1, 0)
        .WithTags(Tags.Catalog)
        .Produces<Result<Response>>(StatusCodes.Status200OK);
    }
    private static async Task<IResult> HandleAsync([AsParameters] Request request, Handler handler,
        CancellationToken ct)
    {
        var result = await handler.HandleAsync(request, ct);

        return result.ToMinimalResult();
    }
}