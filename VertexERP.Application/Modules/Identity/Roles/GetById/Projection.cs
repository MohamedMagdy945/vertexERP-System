using VertexERP.Domain.Module.Identity.Entities;

namespace VertexERP.Application.Modules.Identity.Roles.GetById;

public static class Projection
{
    public static IQueryable<Response> ToResponse(this IQueryable<Role> query)
    {
        return query.Select(x => new Response
        {
            Id = x.Id,
            Name = x.Name!,
            Permissions = x.RolePermissions
                .Select(rp => rp.Permission)
                .ToList()
        });
    }
}

