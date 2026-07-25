using VertexERP.Domain.Module.Identity.Entities;

namespace VertexERP.Application.Modules.Identity.Users.GetById;

public static class Projection
{
    public static IQueryable<Response> ToResponse(this IQueryable<User> query)
    {
        return query.Select(u => new Response
        {
            Id = u.Id,
            Name = u.Name,
            Email = u.Email,
            IsActive = u.IsActive,
            PortalType = u.PortalType.ToString(),
            CreatedAt = u.CreatedAt,
            Roles = u.UserRoles
             .Select(ur => ur.Role.Name)
             .ToList()
        });
    }
};