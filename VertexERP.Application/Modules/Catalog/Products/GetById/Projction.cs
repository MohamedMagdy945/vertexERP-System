using VertexERP.Domain.Module.Catalog.Entities;

namespace VertexERP.Application.Modules.Catalog.Products.GetById;

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
            UnitName = p.Unit.Symbol,
            IsAvailable = p.IsAvailable,
            CategoryName = p.Category.Name,
            Images = p.Images
                .Select(i => new ImageResponse(i.Id, i.Url))
                .ToList()
        });
    }
};