using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.JsonWebTokens;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using VertexERP.Application.Common.Abstractions.Identity;
using VertexERP.Application.Common.Models.Identity;
using VertexERP.Infrastructure.Identity.Settings;

namespace VertexERP.Infrastructure.Identity.Authorization;

public class JwtAccessTokenGenerator(IOptions<AccessTokenSettings> accessTokenSettings) : IAccessTokenGenerator
{
    private readonly AccessTokenSettings _accessTokenSettings = accessTokenSettings.Value;
    private static readonly JsonWebTokenHandler TokenHandler = new();

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

}

