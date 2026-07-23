using System.Security.Cryptography;
using System.Text;
using VertexERP.Application.Common.Abstractions.Identity;

namespace VertexERP.Infrastructure.Services.Identity.Authentication;

public sealed class RefreshTokenHasher : IRefreshTokenHasher
{
    public string Hash(string refreshToken)
    {
        int byteCount = Encoding.UTF8.GetByteCount(refreshToken);
        Span<byte> buffer = stackalloc byte[byteCount];
        Encoding.UTF8.GetBytes(refreshToken, buffer);

        return Convert.ToHexString(SHA256.HashData(buffer));
    }
}