using VertexERP.Domain.Module.Identity.Entities;

namespace VertexERP.Application.Modules.Identity.Authentication.Refresh;

public static class RefreshTokenQueryExtensions
{
    public static IQueryable<Context> ToContext(this IQueryable<RefreshToken> query)
    {
        return query.Select(refreshToken => new Context
        {
            RefreshToken = refreshToken,
            UserId = refreshToken.UserId,
            UserEmail = refreshToken.User.Email,
            Roles = refreshToken.User.UserRoles
                    .Select(userRole => userRole.Role.Name).ToList()
        });
    }
}