using VertexERP.Domain.Module.Identity.Entities;

namespace VertexERP.Application.Modules.Identity.Authentication.Login;

public static class Projection
{
    public static IQueryable<Context> ToContext(this IQueryable<User> query)
    {
        return query.Select(x => new Context
        {
            Id = x.Id,
            Name = x.Name,
            Email = x.Email,
            PasswordHash = x.PasswordHash,
            IsActive = x.IsActive,
            AvatarUrl = x.AvatarUrl,
            Portal = x.PortalType.ToString(),
            Roles = x.UserRoles.Select(ur => ur.Role.Name).ToList()
        });
    }
}