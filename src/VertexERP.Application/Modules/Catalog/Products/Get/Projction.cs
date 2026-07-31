using VertexERP.Domain.Module.Catalog.Entities;

namespace VertexERP.Application.Modules.Catalog.Products.Get;

public static class Projection
{
    public static IQueryable<Response> ToResponse(this IQueryable<Product> query)
    {
        return query.Select(p => new Response
        {
            Id = p.Id,
            Name = p.Name,
            Code = p.Code,
            Description = p.Description,
            SellingPrice = p.SellingPrice,
            UnitSymbol = p.Unit.Symbol,
            IsAvailable = p.IsAvailable,
            Images = p.Images.Select(i => i.Url).ToList()
        });
    }
};