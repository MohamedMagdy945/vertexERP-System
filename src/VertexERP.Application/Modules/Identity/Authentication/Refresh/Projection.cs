using VertexERP.Domain.Module.Identity.Entities;

namespace VertexERP.Application.Modules.Identity.Authentication.Refresh;

public static class RefreshTokenQueryExtensions
{
    public static IQueryable<Context> ToContext(
      this IQueryable<RefreshToken> query)
    {
        return query.Select(x => new Context
        {
            Id = x.User.Id,
            Name = x.User.Name,
            Email = x.User.Email,
            IsActive = x.User.IsActive,
            AvatarUrl = x.User.AvatarUrl,
            Portal = x.User.PortalType.ToString(),
            Roles = x.User.UserRoles.Select(ur => ur.Role.Name).ToList(),
            RefreshToken = x
        });
    }
}