namespace VertexERP.Application.Common.Abstractions.Identity;

public interface IRefreshTokenGenerator
{
    string GenerateRefreshToken();

    string HashRefreshToken(string refreshToken);
}