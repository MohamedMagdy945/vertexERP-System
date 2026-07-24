using VertexERP.Domain.Module.Identity.Entities;

namespace VertexERP.Application.Modules.Identity.Authentication.Refresh;

public static class RefreshTokenQueryExtensions
{
    public static IQueryable<Context> ToContext(
        this IQueryable<RefreshToken> query)
    {
        return query.Select(refreshToken => new Context(refreshToken,
                        refreshToken.UserId, refreshToken.User.Email,
                        refreshToken.User.UserRoles.Select(userRole => userRole.Role.Name).ToList()));
    }
}