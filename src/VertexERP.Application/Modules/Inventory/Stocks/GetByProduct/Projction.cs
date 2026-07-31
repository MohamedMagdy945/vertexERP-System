using VertexERP.Domain.Module.Inventory.Entities;

namespace VertexERP.Application.Modules.Inventory.Stocks.GetByProduct;

public static class Projection
{
    public static IQueryable<Response> ToResponse(this IQueryable<Stock> query)
    {
        return query.Select(x => new Response
        {
            ProductId = x.ProductId,
            ProductName = x.Product.Name,
            ProductCode = x.Product.Code,

            TotalQuantity = query.Sum(s => s.Quantity),

            Warehouses = query.Select(s => new WarehouseResponse
            {
                WarehouseId = s.WarehouseId,
                WarehouseName = s.Warehouse.Name,
                Quantity = s.Quantity
            }).ToList()
        });
    }
}