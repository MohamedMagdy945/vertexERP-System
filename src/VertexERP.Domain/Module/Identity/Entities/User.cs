using VertexERP.Domain.Common;
using VertexERP.Domain.Module.Identity.Enum;

namespace VertexERP.Domain.Module.Identity.Entities;

public class User : Entity
{
    public string Name { get; private set; } = default!;
    public string Email { get; private set; } = default!;
    public string PasswordHash { get; private set; } = default!;
    public bool IsActive { get; private set; }
    public bool IsEmailConfirmed { get; private set; }
    public PortalType PortalType { get; private set; }
    public ICollection<UserRole> UserRoles { get; } = [];
    public ICollection<RefreshToken> RefreshTokens { get; } = [];

    public User(string name, string email, string passwordHash, PortalType portalType = PortalType.User)
    {
        Name = name;
        Email = email;
        PasswordHash = passwordHash;
        IsActive = true;
        PortalType = portalType;
    }
    public void Update(string name, PortalType portalType)
    {
        Name = name;
        PortalType = portalType;
    }
    public void Activate()
    {
        IsActive = true;
    }
    public void Deactivate()
    {
        IsActive = false;
    }
    public void AssignRole(Guid roleId)
    {
        if (UserRoles.Any(r => r.RoleId == roleId))
            return;

        UserRoles.Add(new UserRole(Id, roleId));
    }
    public void RemoveRole(UserRole userRole)
    {
        UserRoles.Remove(userRole);
    }

    public void AddRefreshToken(RefreshToken refreshToken)
    {
        RefreshTokens.Add(refreshToken);
    }
    public void RevokeAllRefreshTokens()
    {
        foreach (var token in RefreshTokens.Where(t => t.IsActive))
        {
            token.Revoke();
        }
    }
}

