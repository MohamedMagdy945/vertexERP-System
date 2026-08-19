using VertexERP.Application.Common.Abstractions.Endpoint;
using VertexERP.Application.Common.Abstractions.Identity;
using VertexERP.Application.Common.Abstractions.Persistence;
using VertexERP.Application.Shared.Results;
using VertexERP.Domain.Module.Inventory.Entities;

namespace VertexERP.Application.Modules.Inventory.StockAdjustments.Create;

public sealed class Handler(IAppDbContext dbContext , ICurrentUser currentUser) : IHandler
{
    public async Task<Result<Response>> HandleAsync(Request request, CancellationToken ct)
    {
        var adjustment = StockAdjustment.Create(
            request.WarehouseId,
            request.ProductId,
            request.Quantity,
            request.Reason,
            currentUser.UserId);

        dbContext.StockAdjustments.Add(adjustment);

        await dbContext.SaveChangesAsync(ct);

        var response = new Response(
            adjustment.Id,
            adjustment.WarehouseId,
            adjustment.ProductId,
            adjustment.Quantity,
            adjustment.Reason,
            adjustment.Status);

        return Result<Response>.Created( response, "Stock adjustment created successfully.");
    }
}