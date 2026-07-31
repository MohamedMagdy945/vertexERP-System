using Microsoft.EntityFrameworkCore;
using VertexERP.Application.Common.Abstractions.Handler;
using VertexERP.Application.Common.Abstractions.Persistence;
using VertexERP.Application.Shared.Results;

namespace VertexERP.Application.Modules.Inventory.Stocks.GetByProduct;

public sealed class Handler(IAppDbContext dbContext) : IHandler
{
    public async Task<Result<Response>> HandleAsync(Request request, CancellationToken ct)
    {
        var response = await dbContext.Stocks
               .AsNoTracking()
               .Where(x => x.ProductId == request.ProductId)
               .ToResponse()
               .FirstOrDefaultAsync(ct);

        if (response is null)
            return Result<Response>.NotFound("Stock not found.");

        return Result<Response>.Success(response);
    }
}