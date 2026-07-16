using VertexERP.Application.Common.Abstractions.Identity;
namespace VertexERP.Infrastructure.Identity.Authorization;

public sealed class BCryptPasswordHasher : IPasswordHasherService
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
