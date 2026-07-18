using VertexERP.Application.Common.Models.Identity;

namespace VertexERP.Application.Common.Abstractions.Identity;

public interface ITokenPairGenerator
{
    TokenPair Generate(UserTokenClaims claims);
}