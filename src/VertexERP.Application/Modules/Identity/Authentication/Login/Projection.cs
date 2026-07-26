using VertexERP.Domain.Module.Identity.Entities;

namespace VertexERP.Application.Modules.Identity.Authentication.Login;

public static class Projection
{
    public static IQueryable<Context> ToContext(this IQueryable<User> query)
    {
        return query.Select(u => new Context
        {
            UserId = u.Id,
            Email = u.Email,
            PasswordHash = u.PasswordHash,
            IsActive = u.IsActive,
            PortalType = u.PortalType.ToString(),
        });
    }
}