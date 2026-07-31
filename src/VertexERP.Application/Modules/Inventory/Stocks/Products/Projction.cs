using VertexERP.Domain.Module.Inventory.Entities;

namespace VertexERP.Application.Modules.Inventory.Stocks.Products;

public static class Projection
{
    public static IQueryable<Response> ToResponse(this IQueryable<Stock> query)
    {
        return query.Select(x => new Response
        {
            WarehouseId = x.WarehouseId,
            WarehouseName = x.Warehouse.Name,
            Quantity = x.Quantity
        });
    }
};