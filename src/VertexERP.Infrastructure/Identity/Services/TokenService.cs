using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.JsonWebTokens;
using Microsoft.IdentityModel.Tokens;
using System.Security.Cryptography;
using System.Text;
using VertexERP.Application.Common.Abstraction.Identity;
using VertexERP.Application.Common.Models.Identity;
using VertexERP.Infrastructure.Identity.Settings;

namespace VertexERP.Infrastructure.Identity.Services;

public class TokenService(IOptions<AccessTokenSettings> accessTokenSettings) : ITokenService
{
    private readonly AccessTokenSettings _accessTokenSettings = accessTokenSettings.Value;
    private static readonly JsonWebTokenHandler TokenHandler = new();
    public string GenerateRefreshToken()
    {
        Span<byte> bytes = stackalloc byte[32];

        RandomNumberGenerator.Fill(bytes);

        return Convert.ToBase64String(bytes);
    }

    public string GenerateAccessToken(UserTokenClaims userClaims)
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

        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_accessTokenSettings.SecretKey));
        var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Issuer = _accessTokenSettings.Issuer,
            Audience = _accessTokenSettings.Audience,
            Claims = claims,
            Expires = DateTime.UtcNow.AddMinutes(_accessTokenSettings.ExpirationInMinutes),
            SigningCredentials = creds
        };

        return TokenHandler.CreateToken(tokenDescriptor);
    }
    public string HashRefreshToken(string refreshToken)
    {
        return Convert.ToHexString(SHA256.HashData(Encoding.UTF8.GetBytes(refreshToken)));
    }


}

