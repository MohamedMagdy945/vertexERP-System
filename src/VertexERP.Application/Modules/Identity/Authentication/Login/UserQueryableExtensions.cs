using VertexERP.Domain.Module.Identity.Entities;

namespace VertexERP.Application.Modules.Identity.Authentication.Login;

public static class UserQueryableExtensions
{
    public static IQueryable<RefreshTokenData> ToLoginData(this IQueryable<User> query)
    {
        return query.Select(u => new RefreshTokenData(
            u.Id, u.Email, u.PasswordHash, u.IsActive
            , u.UserRoles.SelectMany(ur => ur.Role.RolePermissions)
                .Select(rp => rp.Permission.Name)
                .Distinct()
                .ToList())
        );
    }
}

