using VertexERP.Domain.Module.Identity.Entities;

namespace VertexERP.Application.Modules.Identity.Authentication.Refresh;

public sealed class RefreshTokenContext
{
    public RefreshToken RefreshToken { get; init; } = default!;
    public Guid UserId { get; init; }
    public string UserEmail { get; init; } = default!;
    public IEnumerable<string> Permissions { get; init; } = [];
}
public static class RefreshTokenQueryExtensions
{
    public static IQueryable<RefreshTokenContext> ToRefreshTokenContext(
        this IQueryable<RefreshToken> query)
    {
        return query.Select(rt => new RefreshTokenContext
        {
            RefreshToken = rt,
            UserId = rt.UserId,
            UserEmail = rt.User.Email,
            Permissions = rt.User.UserRoles
                .SelectMany(ur => ur.Role.RolePermissions)
                .Select(rp => rp.Permission.Name)
                .Distinct()
        });
    }
}