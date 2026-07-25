using VertexERP.Application.Types.Authentication.Models;

namespace VertexERP.Application.Common.Abstractions.Identity;

public interface IAccessTokenGenerator
{
    AccessTokenInfo Generate(UserTokenClaims claims);
}