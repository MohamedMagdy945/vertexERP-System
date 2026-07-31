using Microsoft.EntityFrameworkCore;
using VertexERP.Application.Common.Abstractions.Handler;
using VertexERP.Application.Common.Abstractions.Persistence;
using VertexERP.Application.Shared.Pagination;
using VertexERP.Application.Shared.Results;

namespace VertexERP.Application.Modules.Inventory.Stocks.GetByWarehouse;

public sealed class Handler(IAppDbContext dbContext) : IHandler
{
    public async Task<Result<Page<Response>>> HandleAsync(Request request, CancellationToken ct)
    {
        var page = await dbContext.Stocks
            .AsNoTracking()
            .Where(x => x.WarehouseId == request.WarehouseId)
            .OrderBy(x => x.ProductId)
            .ToResponse()
            .ToPageAsync(request, ct);

        return Result<Page<Response>>.Success(page);
    }
}