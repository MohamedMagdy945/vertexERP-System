using VertexERP.Domain.Common;

namespace VertexERP.Domain.Module.Identity.Entities;

public class User : Entity
{
    public string Name { get; private set; } = default!;
    public string Email { get; private set; } = default!;
    public string PasswordHash { get; private set; } = default!;
    public bool IsActive { get; private set; }
    public bool IsEmailConfirmed { get; private set; }

    public ICollection<UserRole> UserRoles { get; } = [];
    public ICollection<RefreshToken> RefreshTokens { get; } = [];

    public User(string name, string email, string passwordHash)
    {
        Name = name;
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

