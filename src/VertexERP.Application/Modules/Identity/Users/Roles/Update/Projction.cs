using VertexERP.Domain.Module.Identity.Entities;

namespace VertexERP.Application.Modules.Identity.Users.Roles.Update;

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
            .Select(ur => new RoleResponse
            {
                Id = ur.Role.Id,
                Name = ur.Role.Name
            })
            .ToList()
        });
    }
};