using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.JsonWebTokens;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using VertexERP.Application.Common.Abstractions.Identity;
using VertexERP.Application.Common.Types.Authentication.Models;
using VertexERP.Infrastructure.Common.Settings;

namespace VertexERP.Infrastructure.Services.Identity;

public sealed class AccessTokenGenerator(IOptions<AccessTokenSettings> options) : IAccessTokenGenerator
{
    private const string Role = "role";

    private readonly AccessTokenSettings _settings = options.Value;

    private static readonly JsonWebTokenHandler JwtHandler = new();

    private readonly SigningCredentials _signingCredentials = new(
        new SymmetricSecurityKey(Encoding.UTF8.GetBytes(options.Value.SecretKey)), SecurityAlgorithms.HmacSha256);

    public AccessTokenInfo Generate(UserTokenClaims userClaims)
    {

        var expiresAt = DateTime.UtcNow.AddMinutes(_settings.ExpirationInMinutes);

        var claims = new Dictionary<string, object>
        {
            [JwtRegisteredClaimNames.Sub] = userClaims.UserId,
            [JwtRegisteredClaimNames.Email] = userClaims.Email,
            [JwtRegisteredClaimNames.Jti] = Guid.NewGuid().ToString(),
        };

        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Issuer = _settings.Issuer,
            Audience = _settings.Audience,
            Claims = claims,
            Expires = expiresAt,
            SigningCredentials = _signingCredentials
        };

        var tokenHandler = JwtHandler.CreateToken(tokenDescriptor);

        return new AccessTokenInfo(tokenHandler, expiresAt);
    }

}