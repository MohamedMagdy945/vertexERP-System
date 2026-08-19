using Microsoft.EntityFrameworkCore;
using VertexERP.Application.Common.Abstractions.Endpoint;
using VertexERP.Application.Common.Abstractions.Identity;
using VertexERP.Application.Common.Abstractions.Persistence;
using VertexERP.Application.Shared.Results;
using VertexERP.Domain.Module.Inventory.Entities;

namespace VertexERP.Application.Modules.Inventory.StockAdjustments.Apply;

public sealed class Handler(IAppDbContext dbContext , ICurrentUser currentUser) : IHandler
{
    public async Task<Result<Response>> HandleAsync(Guid id ,Request request, CancellationToken ct)
    {
        var adjustment = await dbContext.StockAdjustments
        .SingleOrDefaultAsync(x => x.Id == id, ct);

        if (adjustment is null)
            return Result<Response>.NotFound(
                "Stock adjustment was not found.");

        var stock = await dbContext.Stocks
            .SingleOrDefaultAsync(
                x => x.ProductId == adjustment.ProductId &&
                     x.WarehouseId == adjustment.WarehouseId,
                ct);

        if (stock is null)
            return Result<Response>.NotFound(
                "Stock was not found.");

        var stockResult = stock.ApplyAdjustment(adjustment.Quantity);

        if (stockResult.IsFailure)
            return Result<Response>.Failure(
                stockResult.Error!);

        var adjustmentResult = adjustment.Apply();

        if (adjustmentResult.IsFailure)
            return Result<Response>.Failure(
                adjustmentResult.Error!);

        var movement = StockMovement.CreateAdjustment(
            adjustment.WarehouseId,
            adjustment.ProductId,
            adjustment.Quantity,
            currentUser.UserId);

        dbContext.StockMovements.Add(movement);

        await dbContext.SaveChangesAsync(ct);

        var response = new Response(
            adjustment.Id,
            adjustment.WarehouseId,
            adjustment.ProductId,
            adjustment.Quantity,
            stock.Quantity,
            adjustment.Status);

        return Result<Response>.Success( response,"Stock adjustment applied successfully.");
    }
}