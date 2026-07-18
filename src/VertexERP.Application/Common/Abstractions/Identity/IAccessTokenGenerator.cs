using VertexERP.Application.Common.Models.Identity;


namespace VertexERP.Application.Common.Abstractions.Identity;

public interface IAccessTokenGenerator
{
    string Generate(UserTokenClaims userClaims);
    DateTime GetExpirationTime();
}