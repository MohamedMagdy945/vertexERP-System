using VertexERP.Domain.Module.Catalog.Entities;

namespace VertexERP.Application.Modules.Catalog.MeasurementUnits.GetById;

public static class Projection
{
    public static IQueryable<Response> ToResponse(this IQueryable<MeasurementUnit> query)
    {
        return query.Select(u => new Response
        {
            Id = u.Id,
            Symbol = u.Symbol,
        });
    }
};