using Microsoft.Extensions.Options;
using System.Security.Cryptography;
using System.Text;
using VertexERP.Application.Common.Abstractions.Identity;
using VertexERP.Application.Common.Types.Authentication.Models;
using VertexERP.Infrastructure.Common.Settings;

namespace VertexERP.Infrastructure.Services.Identity;

public sealed class RefreshTokenService(IOptions<AccessTokenSettings> options) : IRefreshTokenService
{
    private const int RefreshTokenSize = 32;

    private readonly AccessTokenSettings _settings = options.Value;

    public RefreshTokenInfo Generate()
    {
        var expiresAt = DateTime.UtcNow.AddDays(_settings.ExpirationInMinutes);

        Span<byte> bytes = stackalloc byte[RefreshTokenSize];
        RandomNumberGenerator.Fill(bytes);

        var refreshToken = Convert.ToBase64String(bytes);

        return new RefreshTokenInfo(refreshToken, expiresAt);
    }
    public string ComputeHash(string refreshToken)
    {
        int byteCount = Encoding.UTF8.GetByteCount(refreshToken);

        Span<byte> buffer = stackalloc byte[byteCount];
        Encoding.UTF8.GetBytes(refreshToken, buffer);

        return Convert.ToHexString(SHA256.HashData(buffer));
    }
}