using VertexERP.Application.Modules.Inventory.Warehouses.Get;
using VertexERP.Domain.Module.Inventory.Entities;

namespace VertexERP.Application.Services.Get;

public static class Projection
{
    public static IQueryable<Response> ToResponse(this IQueryable<Warehouse> query)
    {
        return query.Select(x => new Response
        {
            Id = x.Id,
            Name = x.Name,
            Code = x.Code,
            Location = x.Location
        });
    }
};