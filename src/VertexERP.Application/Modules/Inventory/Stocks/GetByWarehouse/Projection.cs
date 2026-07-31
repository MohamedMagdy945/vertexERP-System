using VertexERP.Domain.Module.Inventory.Entities;

namespace VertexERP.Application.Modules.Inventory.Stocks.GetByWarehouse;

public static class Projection
{
    public static IQueryable<Response> ToResponse(this IQueryable<Stock> query)
    {
        return query.Select(x => new Response
        {
            ProductId = x.ProductId,
            ProductName = x.Product.Name,
            ProductCode = x.Product.Code,
            Quantity = x.Quantity
        });
    }
}