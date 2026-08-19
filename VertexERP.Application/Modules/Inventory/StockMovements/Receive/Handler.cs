using Microsoft.EntityFrameworkCore;
using VertexERP.Application.Common.Abstractions.Endpoint;
using VertexERP.Application.Common.Abstractions.Identity;
using VertexERP.Application.Common.Abstractions.Persistence;
using VertexERP.Application.Shared.Results;
using VertexERP.Domain.Module.Inventory.Entities;
using VertexERP.Domain.Module.Inventory.Enums;

namespace VertexERP.Application.Modules.Inventory.StockMovements.Receive;

public sealed class Handler(IAppDbContext dbContext , ICurrentUser currentUser) : IHandler
{
    public async Task<Result<Response>> HandleAsync(Request request, CancellationToken ct)
    {
        var stock = await dbContext.Stocks
            .SingleOrDefaultAsync(x => x.ProductId == request.ProductId && x.WarehouseId == request.WarehouseId,ct);

        var previousQuantity = stock?.Quantity ?? 0;

        if (stock is null)
        {
            stock = Stock.Create(request.ProductId,request.WarehouseId);

            dbContext.Stocks.Add(stock);
        }
        var result = stock.Receive(request.Quantity);

        if (result.IsFailure) return Result<Response>.Failure(result.Error!);

        var movement = new StockMovement(
             warehouseId: request.WarehouseId,
             productId: request.ProductId,
             quantity: request.Quantity,
             performedByUserId: currentUser.UserId,
             direction: StockMovementDirection.In,
             type: StockMovementType.Purchase,
             transactionDate: DateTime.UtcNow,
             referenceNumber: request.ReferenceNumber,
             notes: request.Description);

        dbContext.StockMovements.Add(movement);

        dbContext.StockMovements.Add(movement);

        await dbContext.SaveChangesAsync(ct);

        var response = new Response(
           stock.ProductId,
           stock.WarehouseId,
           previousQuantity,
           request.Quantity,
           stock.Quantity);

        return Result<Response>.Success(response,"Stock received successfully.");

    }
}