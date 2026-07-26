using VertexERP.Application.Common.Abstractions.Http;
using VertexERP.Application.Common.Abstractions.Identity;
using VertexERP.Application.Common.Abstractions.Persistence;
using VertexERP.Application.Types.Authentication.Contracts;
using VertexERP.Application.Types.Authentication.Models;
using VertexERP.Domain.Module.Identity.Entities;

namespace VertexERP.Application.Services;

public sealed class SessionService(
    IAccessTokenGenerator accessTokenGenerator,
    IRefreshTokenService refreshTokenService,
    IClientInfoProvider clientInfoProvider,
    IAppDbContext dbContext)
{
    public TokenPair Create(UserTokenClaims userClaims)
    {
        var accessTokenInfo = accessTokenGenerator.Generate(userClaims);

        var refreshTokenInfo = refreshTokenService.Generate();

        var refreshToken = new RefreshToken
        (
            tokenHash: refreshTokenService.ComputeHash(refreshTokenInfo.Token),
            userId: userClaims.UserId,
            expiresAt: refreshTokenInfo.ExpiresAt,
            createdByIp: clientInfoProvider.GetIpAddress(),
            deviceInfo: clientInfoProvider.GetUserAgent()
        );

        dbContext.RefreshTokens.Add(refreshToken);

        return new TokenPair(accessTokenInfo, refreshTokenInfo);
    }
}