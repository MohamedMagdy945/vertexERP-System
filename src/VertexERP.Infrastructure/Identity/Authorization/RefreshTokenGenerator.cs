using System.Security.Cryptography;
using System.Text;
using VertexERP.Application.Common.Abstractions.Identity;

namespace VertexERP.Infrastructure.Identity.Authorization;

public class RefreshTokenGenerator : IRefreshTokenGenerator
{

    public string GenerateRefreshToken()
    {
        Span<byte> bytes = stackalloc byte[32];

        RandomNumberGenerator.Fill(bytes);

        return Convert.ToBase64String(bytes);
    }

    public string HashRefreshToken(string refreshToken)
    {
        return Convert.ToHexString(SHA256.HashData(Encoding.UTF8.GetBytes(refreshToken)));
    }
}

