using Mapster;
using Microsoft.EntityFrameworkCore;
using VertexERP.Application.Common.Abstractions.Handler;
using VertexERP.Application.Common.Abstractions.Identity;
using VertexERP.Application.Common.Abstractions.Persistence;
using VertexERP.Application.Shared.Results;
using VertexERP.Domain.Module.Inventory.Entities;
using VertexERP.Domain.Module.Inventory.Enums;

namespace VertexERP.Application.Modules.Inventory.StockMovements.Create;

public sealed class Handler(IAppDbContext dbContext, ICurrentUserService currentUserService) : IHandler
{
    public async Task<Result<Response>> HandleAsync(Request request, CancellationToken ct)
    {
        var stock = await dbContext.Stocks
         .FirstOrDefaultAsync(x =>
             x.WarehouseId == request.WarehouseId &&
             x.ProductId == request.ProductId,
             ct);


        if (stock is null)
        {
            if (request.Direction == StockMovementDirection.Out)
            {
                return Result<Response>.BadRequest(
                    "Cannot remove stock because stock does not exist.");
            }

            stock = new Stock(
                request.WarehouseId,
                request.ProductId,
                request.Quantity);

            dbContext.Stocks.Add(stock);
        }
        else
        {
            if (request.Direction == StockMovementDirection.In)
            {
                stock.Increase(request.Quantity);
            }
            else
            {
                if (stock.Quantity < request.Quantity)
                {
                    return Result<Response>.BadRequest(
                        "Insufficient stock quantity.");
                }

                stock.Decrease(request.Quantity);
            }
        }


        var movement = new StockMovement(
            request.WarehouseId,
            request.ProductId,
            request.Quantity,
            currentUserService.UserId,
            request.Direction,
            request.Type,
            request.TransactionDate,
            request.ReferenceNumber,
            request.Notes
        );


        dbContext.StockMovements.Add(movement);


        await dbContext.SaveChangesAsync(ct);


        return Result<Response>.Success(
            movement.Adapt<Response>());
    }
}