using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using VertexERP.Application.Common.Abstractions.Endpoint;
using VertexERP.Application.Common.Authorization;
using VertexERP.Application.Common.Extensions;
using VertexERP.Application.Shared.Constant;
using VertexERP.Application.Shared.Results;

namespace VertexERP.Application.Modules.Catalog.MeasurementUnits.Get;

public sealed class Endpoint : IEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapGet("/measurement-units", async ([AsParameters] Request query, Handler handler, CancellationToken ct) =>
        {
            var result = await handler.HandleAsync(query, ct);

            return result.ToMinimalResult();
        })
        .HasPermission(Perms.Catalog.View)
        .MapToApiVersion(1, 0)
        .WithTags(Tags.Catalog)
        .Produces<Result<Response>>(StatusCodes.Status200OK);
    }
}