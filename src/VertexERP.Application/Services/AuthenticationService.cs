using VertexERP.Application.Common.Abstractions.Http;
using VertexERP.Application.Common.Abstractions.Identity;
using VertexERP.Application.Common.Abstractions.Persistence;
using VertexERP.Application.Common.Models.Identity;
using VertexERP.Domain.Module.Identity.Entities;

namespace VertexERP.Application.Services;

public sealed class AuthenticationService(IApplicationDbContext dbContext, ITokenPairGenerator tokenPairGenerator,
    IRefreshTokenHasher refreshTokenHasher, IClientInfoProvider clientInfoProvider)
{
    public TokenPair CreateSession(UserTokenClaims claims)
    {
        var tokenPair = tokenPairGenerator.Generate(claims);

        var refreshToken = new RefreshToken
        (
            tokenHash: refreshTokenHasher.Hash(tokenPair.RefreshToken),
            userId: claims.UserId,
            expiresAt: tokenPair.RefreshTokenExpiresAt,
            createdByIp: clientInfoProvider.GetIpAddress(),
            deviceInfo: clientInfoProvider.GetUserAgent()
        );

        dbContext.RefreshTokens.Add(refreshToken);
        return tokenPair;
    }

}

