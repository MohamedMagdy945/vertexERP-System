using Microsoft.EntityFrameworkCore;
using VertexERP.Application.Common.Abstractions.Handler;
using VertexERP.Application.Common.Abstractions.Persistence;
using VertexERP.Application.Shared.Results;

namespace VertexERP.Application.Modules.Inventory.Stocks.Products;

public sealed class Handler(IAppDbContext dbContext) : IHandler
{
    public async Task<Result<IReadOnlyList<Response>>> HandleAsync(Request request, CancellationToken ct)
    {
        var stocks = await dbContext.Stocks
            .AsNoTracking()
            .Where(x => x.ProductId == request.ProductId)
            .ToResponse()
            .ToListAsync(ct);

        return Result<IReadOnlyList<Response>>.Success(stocks);

    }
}