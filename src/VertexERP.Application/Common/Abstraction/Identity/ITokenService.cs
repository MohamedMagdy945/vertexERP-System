using VertexERP.Application.Common.Models.Identity;


namespace VertexERP.Application.Common.Abstraction.Identity;

public interface ITokenService
{
    string GenerateRefreshToken();
    string GenerateAccessToken(UserTokenClaims userClaims);
    string HashRefreshToken(string refreshToken);
}