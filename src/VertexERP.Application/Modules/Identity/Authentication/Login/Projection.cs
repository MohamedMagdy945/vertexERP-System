using VertexERP.Domain.Module.Identity.Entities;

namespace VertexERP.Application.Modules.Identity.Authentication.Login;

public static class Projection
{
    public static IQueryable<Context> ToContext(this IQueryable<User> query)
    {
        return query.Select(user => new Context
        {
            UserId = user.Id,
            Email = user.Email,
            PasswordHash = user.PasswordHash,
            IsActive = user.IsActive,
            Roles = user.UserRoles
               .Select(userRole => userRole.Role.Name)
               .ToList()
        });
    }
}