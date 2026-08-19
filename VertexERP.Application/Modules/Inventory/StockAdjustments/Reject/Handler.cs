using Microsoft.EntityFrameworkCore;
using VertexERP.Application.Common.Abstractions.Endpoint;
using VertexERP.Application.Common.Abstractions.Identity;
using VertexERP.Application.Common.Abstractions.Persistence;
using VertexERP.Application.Shared.Results;

namespace VertexERP.Application.Modules.Inventory.StockAdjustments.Reject;

public sealed class Handler(IAppDbContext dbContext , ICurrentUser currentUser) : IHandler
{
    public async Task<Result<Response>> HandleAsync(Guid id ,Request request, CancellationToken ct)
    {
        var adjustment = await dbContext.StockAdjustments
            .SingleOrDefaultAsync(x => x.Id == id, ct);

        if (adjustment is null)
            return Result<Response>.NotFound(
                "Stock adjustment was not found.");

        var result = adjustment.Reject(currentUser.UserId,request.Reason);

        if (result.IsFailure)
            return Result<Response>.Failure(result.Error!);

        await dbContext.SaveChangesAsync(ct);

        var response = new Response(
            adjustment.Id,
            adjustment.WarehouseId,
            adjustment.ProductId,
            adjustment.Quantity,
            request.Reason,
            adjustment.Status);

        return Result<Response>.Success( response,"Stock adjustment rejected successfully.");
    }
}