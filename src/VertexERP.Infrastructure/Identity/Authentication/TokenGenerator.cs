using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.JsonWebTokens;
using Microsoft.IdentityModel.Tokens;
using System.Security.Cryptography;
using System.Text;
using VertexERP.Application.Common.Abstractions.Identity;
using VertexERP.Application.Common.Models.Identity;
using VertexERP.Infrastructure.Identity.Configuration;

namespace VertexERP.Infrastructure.Identity.Authentication;

public sealed class TokenGenerator(IOptions<TokenSettings> options) : ITokenGenerator
{
    private const string PermissionsClaim = "permissions";
    private const int RefreshTokenSize = 32;

    private static readonly JsonWebTokenHandler TokenHandler = new();

    private readonly TokenSettings _settings = options.Value;

    public TokenPair GenerateTokenPair(UserTokenClaims claims)
    {
        var accessTokenExpiresAt = DateTime.UtcNow.AddMinutes(_settings.AccessTokenExpirationInMinutes);
        var refreshTokenExpiresAt = DateTime.UtcNow.AddDays(_settings.RefreshTokenExpirationInDays);

        var refreshToken = GenerateRefreshToken();

        return new TokenPair(
            AccessToken: GenerateAccessToken(claims, accessTokenExpiresAt),
            AccessTokenExpiresAt: accessTokenExpiresAt,
            RefreshToken: refreshToken,
            RefreshTokenHash: HashRefreshToken(refreshToken),
            RefreshTokenExpiresAt: refreshTokenExpiresAt);
    }

    private string GenerateAccessToken(UserTokenClaims userClaims, DateTime expiresAt)
    {
        var claims = new Dictionary<string, object>
        {
            [JwtRegisteredClaimNames.Sub] = userClaims.UserId,
            [JwtRegisteredClaimNames.Email] = userClaims.Email,
            [JwtRegisteredClaimNames.Jti] = Guid.NewGuid().ToString()
        };

        if (userClaims.Permissions is not null && userClaims.Permissions.Any())
        {
            claims["permissions"] = userClaims.Permissions;
        }

        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Issuer = _settings.Issuer,
            Audience = _settings.Audience,
            Claims = claims,
            Expires = expiresAt,
            SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_settings.SecretKey)), SecurityAlgorithms.HmacSha256)
        };

        return TokenHandler.CreateToken(tokenDescriptor);
    }

    private static string GenerateRefreshToken()
    {
        Span<byte> bytes = stackalloc byte[RefreshTokenSize];
        RandomNumberGenerator.Fill(bytes);

        return Convert.ToBase64String(bytes);
    }

    private string HashRefreshToken(string refreshToken)
    {
        int byteCount = Encoding.UTF8.GetByteCount(refreshToken);
        Span<byte> buffer = stackalloc byte[byteCount];
        Encoding.UTF8.GetBytes(refreshToken, buffer);

        return Convert.ToHexString(SHA256.HashData(buffer));
    }
}