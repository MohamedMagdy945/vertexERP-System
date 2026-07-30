using VertexERP.Domain.Module.Catalog.Entities;

namespace VertexERP.Application.Modules.Catalog.Products.Images.Upload;

public static class Projection
{
    public static IQueryable<Context> ToContext(this IQueryable<Product> query)
    {
        return query.Select(x => new Context
        {
            Code = x.Code,
            CurrentImagesCount = x.Images.Count
        });
    }
}
