using VertexERP.Domain.Module.Identity.Entities;

namespace VertexERP.Application.Modules.Identity.Users.Get;

public static class Projection
{
    public static IQueryable<UserResponse> ToResponse(this IQueryable<User> query)
    {
        return query.Select(u => new UserResponse(
            u.Id,
            u.Name,
            u.Email,
            u.IsActive,
            u.PortalType.ToString(),
            u.CreatedAt,
            u.UserRoles.Select(ur => ur.Role.Name).ToList()
        ));
    }
};