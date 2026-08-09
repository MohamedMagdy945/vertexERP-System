using Microsoft.EntityFrameworkCore;
using VertexERP.Application.Common.Abstractions.Endpoint;
using VertexERP.Application.Common.Abstractions.Persistence;
using VertexERP.Application.Common.Extensions;
using VertexERP.Application.Shared.Pagination;
using VertexERP.Application.Shared.Results;

namespace VertexERP.Application.Modules.Inventory.StockMovements.GetList;

public sealed class Handler(IAppDbContext dbContext) : IHandler
{
    public async Task<Result<Page<Response>>> HandleAsync(Request request, CancellationToken ct)
    {
        var query = dbContext.StockMovements
         .AsNoTracking();

        if (request.ProductId.HasValue)
            query = query.Where(x => x.ProductId == request.ProductId);

        if (request.WarehouseId.HasValue)
            query = query.Where(x => x.WarehouseId == request.WarehouseId);

        if (request.Type.HasValue)
            query = query.Where(x => x.Type == request.Type);

        var page = await query
            .OrderByDescending(x => x.TransactionDate)
            .ToResponse()
            .ToPageAsync(request, ct);

        return Result<Page<Response>>.Success(page);
    }
}