using VertexERP.Application.Common.Abstractions.Identity;
namespace VertexERP.Infrastructure.Identity.Services;

public sealed class PasswordHasherService : IPasswordHasherService
{
    public string Hash(string password)
    {
        return BCrypt.Net.BCrypt.EnhancedHashPassword(password);
    }

    public bool Verify(string password, string hashedPassword)
    {
        return BCrypt.Net.BCrypt.EnhancedVerify(password, hashedPassword);
    }
}
