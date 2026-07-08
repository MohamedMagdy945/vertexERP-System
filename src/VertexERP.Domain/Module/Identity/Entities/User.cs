namespace VertexERP.Domain.Module.Identity.Entities;

public class User
{
    public string FullName { get; set; } = string.Empty;
    public string FirstName { get; set; } = string.Empty;
    public string LastName { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public string PhoneNumber { get; set; } = string.Empty;
    public string ImageUrl { get; set; } = string.Empty;
    public string PasswordHash { get; set; } = string.Empty;
    public bool IsEnabled { get; set; } = false;
    public ICollection<UserPermission> UserPermissions { get; set; } = new List<UserPermission>();
    public ICollection<RefreshToken> RefreshTokens { get; set; } = new List<RefreshToken>();
}

