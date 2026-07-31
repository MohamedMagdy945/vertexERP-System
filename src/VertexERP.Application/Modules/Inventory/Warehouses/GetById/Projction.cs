using VertexERP.Domain.Module.Catalog.Entities;

namespace VertexERP.Application.Modules.Inventory.Warehouses.GetById;

public static class Projection
{
    public static IQueryable<Response> ToResponse(this IQueryable<Category> query)
    {
        return query.Select(u => new Response
        {
            Id = u.Id,
            Name = u.Name,
            Description = u.Description,
        });
    }
};