using Microsoft.EntityFrameworkCore;
using VertexERP.Application.Common.Abstractions.Endpoint;
using VertexERP.Application.Common.Abstractions.Persistence;
using VertexERP.Application.Shared.Pagination;
using VertexERP.Application.Shared.Results;

namespace VertexERP.Application.Modules.Catalog.MeasurementUnits.Get;

public sealed class Handler(IAppDbContext dbContext) : IHandler
{
    public async Task<Result<Page<Response>>> HandleAsync(Request request, CancellationToken ct)
    {
        var query = dbContext.MeasurementUnits.AsNoTracking();

        var totalCount = await query.CountAsync(ct);

        if (totalCount == 0)
        {
            return Result<Page<Response>>.Success(Page<Response>.Create([], 0, request.PageNumber, request.PageSize));
        }

        var page = await query
            .OrderBy(x => x.Symbol)
            .ToResponse()
            .ToPageAsync(request, ct);

        return Result<Page<Response>>.Success(page);
    }
}