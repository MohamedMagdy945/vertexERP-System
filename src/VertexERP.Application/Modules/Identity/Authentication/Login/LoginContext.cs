using VertexERP.Domain.Module.Identity.Entities;

namespace VertexERP.Application.Modules.Identity.Authentication.Login;

public sealed record LoginContext(
    Guid UserId,
    string Email,
    string PasswordHash,
    bool IsActive,
    IReadOnlyCollection<string> Permissions
);

public static class UserQueryExtensions
{
    public static IQueryable<LoginContext> ToLoginContext(
        this IQueryable<User> query)
    {
        return query.Select(user => new LoginContext(
            user.Id,
            user.Email,
            user.PasswordHash,
            user.IsActive,
            user.UserRoles
                .SelectMany(userRole => userRole.Role.RolePermissions)
                .Select(rolePermission => rolePermission.Permission.Name)
                .Distinct()
                .ToList()
        ));
    }
}