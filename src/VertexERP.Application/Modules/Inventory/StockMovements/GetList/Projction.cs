using VertexERP.Domain.Module.Inventory.Entities;

namespace VertexERP.Application.Modules.Inventory.StockMovements.GetList;

public static class Projection
{
    public static IQueryable<Response> ToResponse(this IQueryable<StockMovement> query)
    {
        return query.Select(x => new Response
        {
            Id = x.Id,
            ProductId = x.ProductId,
            WarehouseId = x.WarehouseId,
            Quantity = x.Quantity,
            Direction = x.Direction,
            Type = x.Type,
            TransactionDate = x.TransactionDate,
            ReferenceNumber = x.ReferenceNumber
        });
    }
};