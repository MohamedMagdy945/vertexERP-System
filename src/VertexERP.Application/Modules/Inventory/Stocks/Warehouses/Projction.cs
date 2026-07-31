using VertexERP.Domain.Module.Inventory.Entities;

namespace VertexERP.Application.Modules.Inventory.Stocks.Warehouses;

public static class Projection
{
    public static IQueryable<Response> ToResponse(this IQueryable<Stock> query)
    {
        return query.Select(x => new Response
        {
            ProductId = x.ProductId,
            ProductName = x.Product.Name,
            Quantity = x.Quantity
        });
    }
};