using VertexERP.Domain.Module.Identity.Entities;

namespace VertexERP.Application.Modules.Identity.Authentication.Refresh;

public sealed record RefreshTokenContext(
    RefreshToken RefreshToken,
    Guid UserId,
    string UserEmail,
    IReadOnlyCollection<string> Permissions
);

public static class RefreshTokenQueryExtensions
{
    public static IQueryable<RefreshTokenContext> ToRefreshTokenContext(
        this IQueryable<RefreshToken> query)
    {
        return query.Select(refreshToken => new RefreshTokenContext(
            refreshToken,
            refreshToken.UserId,
            refreshToken.User.Email,
            refreshToken.User.UserRoles
                .SelectMany(userRole => userRole.Role.RolePermissions)
                .Select(rolePermission => rolePermission.Permission.Name)
                .Distinct()
                .ToList()
        ));
    }
}