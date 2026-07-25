using VertexERP.Domain.Module.Identity.Entities;

namespace VertexERP.Application.Modules.Identity.Me;

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
            IsEmailConfirmed = u.IsEmailConfirmed,
            CreatedAt = u.CreatedAt,
            UpdatedAt = u.UpdatedAt,
            Roles = u.UserRoles
             .Select(ur => ur.Role.Name)
             .ToList()
        });
    }
};