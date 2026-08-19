using Microsoft.EntityFrameworkCore;
using VertexERP.Application.Common.Abstractions.Endpoint;
using VertexERP.Application.Common.Abstractions.Persistence;
using VertexERP.Application.Common.Extensions;
using VertexERP.Application.Shared.Pagination;
using VertexERP.Application.Shared.Results;
using VertexERP.Domain.Module.Inventory.Entities;

namespace VertexERP.Application.Modules.Inventory.StockMovements.Receive;

public sealed class Handler(IAppDbContext dbContext) : IHandler
{
    public async Task<Result<Response>> HandleAsync(Request request, CancellationToken ct)
    {
        var stock = await dbContext.Stocks
            .SingleOrDefaultAsync(x => x.ProductId == request.ProductId && x.WarehouseId == request.WarehouseId,ct);

        if (stock is null)
        {
            stock = Stock.Create(request.ProductId,request.WarehouseId);

            dbContext.Stocks.Add(stock);
        }
        var result = stock.Receive(request.Quantity);

        if (result.IsFailure) return Result<Response>.Failure(result.Error!);

        await dbContext.SaveChangesAsync(ct);

        var response = new Response( stock.ProductId,stock.WarehouseId,stock.Quantity);

        return Result<Response>.Success(response,"Stock received successfully.");

    }
}