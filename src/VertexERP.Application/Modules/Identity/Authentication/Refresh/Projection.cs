using VertexERP.Domain.Module.Identity.Entities;

namespace VertexERP.Application.Modules.Identity.Authentication.Refresh;

public static class RefreshTokenQueryExtensions
{
    public static IQueryable<Context> ToContext(this IQueryable<RefreshToken> query)
    {
        return query.Select(r => new Context
        {
            RefreshToken = r,
            UserId = r.UserId,
            UserEmail = r.User.Email,
            PortalType = r.User.PortalType.ToString(),
        });
    }
}