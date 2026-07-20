namespace VertexERP.Application.Common.Abstractions.Identity;

public interface IRefreshTokenHasher
{
    string Hash(string refreshToken);
}
