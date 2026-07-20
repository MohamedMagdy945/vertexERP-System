using VertexERP.Domain.Common;

namespace VertexERP.Domain.Module.Identity.Entities;

public class User : BaseEntity
{
    public string FullName { get; private set; } = default!;
    public string Email { get; private set; } = default!;
    public string PasswordHash { get; private set; } = default!;
    public bool IsActive { get; private set; }
    public ICollection<UserRole> UserRoles { get; } = [];
    public ICollection<RefreshToken> RefreshTokens { get; } = [];

    public User(string fullName, string email, string passwordHash)
    {
        FullName = fullName;
        Email = email.ToLowerInvariant();
        PasswordHash = passwordHash;
        IsActive = true;
    }

    public void Activate()
    {
        IsActive = true;
    }
    public void Deactivate()
    {
        IsActive = false;
    }
    public void AddRole(UserRole userRole)
    {
        if (!UserRoles.Contains(userRole))
            UserRoles.Add(userRole);
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

