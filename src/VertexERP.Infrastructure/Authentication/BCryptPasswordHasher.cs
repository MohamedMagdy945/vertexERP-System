using VertexERP.Application.Abstractions.Authentication;

namespace VertexERP.Infrastructure.Authentication;

public class BCryptPasswordHasher : IPasswordHasher
{
    private const int WorkFactor = 12;

    private static readonly string DummyHash =
        BCrypt.Net.BCrypt.HashPassword("timing-attack-mitigation", WorkFactor);

    public string Hash(string password)
        => BCrypt.Net.BCrypt.HashPassword(password, WorkFactor);

    public bool Verify(string password, string hashedPassword)
    {
        var hash = string.IsNullOrEmpty(hashedPassword) ? DummyHash : hashedPassword;
        return BCrypt.Net.BCrypt.Verify(password, hash);
    }
}