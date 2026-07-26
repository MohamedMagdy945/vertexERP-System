using VertexERP.Domain.Module.Identity.Entities;

namespace VertexERP.Application.Modules.Identity.Users.Get;

public static class Projection
{
    public static IQueryable<UserResponse> ToResponse(this IQueryable<User> query)
    {
        return query.Select(u => new UserResponse
        {
            Id = u.Id,
            Name = u.Name,
            Email = u.Email,
            IsActive = u.IsActive,
            PortalType = u.PortalType.ToString(),
            CreatedAt = u.CreatedAt,
        });
    }
};