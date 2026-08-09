using VertexERP.Domain.Module.Identity.Entities;

namespace VertexERP.Application.Modules.Identity.Roles.Get;

public static class Projection
{
    public static IQueryable<Response> ToResponse(this IQueryable<Role> query)
    {
        return query.Select(role => new Response
        {
            Id = role.Id,
            Name = role.Name,
            UsersCount = role.UserRoles.Count()
        });
    }
}
