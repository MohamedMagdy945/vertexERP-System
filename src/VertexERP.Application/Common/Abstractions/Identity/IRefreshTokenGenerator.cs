namespace VertexERP.Application.Common.Abstractions.Identity;

public interface IRefreshTokenGenerator
{
    string Generate();

    string Hash(string refreshToken);
    public DateTime GetExpirationTime();

}