using VertexERP.Domain.Module.Identity.Entities;

namespace VertexERP.Application.Modules.Identity.Users.GetById;

public static class Projection
{
    public static IQueryable<Response> ToResponse(this IQueryable<User> query)
    {
        return query.Select(u => new Response(
            u.Id,
            u.Name,
            u.Email,
            u.IsActive,
            u.CreatedAt,
            u.UserRoles.Select(ur => ur.Role.Name).ToList(),
            u.UserRoles
                .SelectMany(ur => ur.Role.RolePermissions)
                .Select(rp => rp.Permission.Name)
                .Distinct()
                .ToList()));
    }
};