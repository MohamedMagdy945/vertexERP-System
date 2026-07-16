using VertexERP.Domain.Common;

namespace VertexERP.Domain.Module.Identity.Entities;

public class User : BaseEntity
{
    public string FirstName { get; private set; } = default!;
    public string LastName { get; private set; } = default!;
    public string Email { get; private set; } = default!;
    public string PasswordHash { get; private set; } = default!;
    public bool IsActive { get; private set; }

    public string PhoneNumber { get; private set; } = default!;
    public ICollection<UserRole> UserRoles { get; } = [];
    public ICollection<RefreshToken> RefreshTokens { get; } = [];

    public User(string firstName, string lastName, string email, string passwordHash)
    {
        FirstName = firstName;
        LastName = lastName;
        Email = email.ToLowerInvariant();
        PasswordHash = passwordHash;
        IsActive = true;
    }
    public void UpdateProfile(string firstName, string lastName, string phoneNumber)
    {
        FirstName = firstName;
        LastName = lastName;
        PhoneNumber = phoneNumber;
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

