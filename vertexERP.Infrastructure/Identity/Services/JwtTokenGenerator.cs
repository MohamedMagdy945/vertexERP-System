using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using VertexERP.Application.Common.Models;
using VertexERP.Infrastructure.Common.Settings;
using VertexERP.Infrastructure.Identity.Entities;

namespace VertexERP.Infrastructure.Identity.Identity
{
    public class JwtTokenGenerator
    {
        private readonly JwtSettings _jwtSettings;
        public JwtTokenGenerator(IOptions<JwtSettings> settings)
        {
            _jwtSettings = settings.Value;
        }
        public TokenResponse GenerateTokenPair(ApplicationUser user, IEnumerable<string>? permissions)
        {

            return new TokenResponse
            {
                AccessToken = GenerateAccessToken(user, permissions),
                RefreshToken = GenerateRefreshToken(),
                AccessTokenExpiration = DateTime.UtcNow.AddMinutes(_jwtSettings.AccessTokenExpiryMinutes),
                RefreshTokenExpiration = DateTime.UtcNow.AddDays(_jwtSettings.RefreshTokenExpiryDays),
            };
        }

        public string GenerateAccessToken(ApplicationUser user, IEnumerable<string>? permissions)
        {
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                new Claim(ClaimTypes.Name, user.UserName ?? ""),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
            };

            if (permissions != null)
            {
                claims.AddRange(permissions.Select(p =>
                          new Claim("permission", p)));
            }

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtSettings.AccessTokenSecret));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
               issuer: _jwtSettings.Issuer,
               audience: _jwtSettings.Audience,
               claims: claims,
               expires: DateTime.UtcNow.AddMinutes(_jwtSettings.AccessTokenExpiryMinutes),
               signingCredentials: creds
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        public string GenerateRefreshToken()
        {
            return Convert.ToBase64String(RandomNumberGenerator.GetBytes(64));
        }

        public string HashToken(string token)
        {
            var key = Encoding.UTF8.GetBytes(_jwtSettings.RefreshTokenSecret);

            using var hmac = new HMACSHA256(key);
            var bytes = hmac.ComputeHash(Encoding.UTF8.GetBytes(token));

            return Convert.ToHexString(bytes).ToLower();
        }
    }
}
