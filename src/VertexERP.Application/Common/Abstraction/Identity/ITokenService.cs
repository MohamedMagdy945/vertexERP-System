using VertexERP.Application.Common.Models.Identity;


namespace VertexERP.Application.Common.Abstraction.Identity;

public interface ITokenService
{
    TokenPair GenerateTokenPair(UserTokenClaims userClaims);
    string GenerateAccessToken(UserTokenClaims userClaims);
    string GenerateRefreshToken();
    string HashRefreshToken(string refreshToken);
}