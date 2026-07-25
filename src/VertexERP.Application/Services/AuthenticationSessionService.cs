using VertexERP.Application.Common.Abstractions.Http;
using VertexERP.Application.Common.Abstractions.Identity;
using VertexERP.Application.Common.Abstractions.Persistence;
using VertexERP.Application.Types.Authentication.Contracts;
using VertexERP.Application.Types.Authentication.Models;
using VertexERP.Domain.Module.Identity.Entities;

namespace VertexERP.Application.Services;

public sealed class AuthenticationSessionService(
    IAccessTokenGenerator accessTokenGenerator,
    IRefreshTokenService refreshTokenService,
    IClientInfoProvider clientInfoProvider,
    IApplicationDbContext dbContext)
{
    public AuthenticationResult Create(AuthenticatedUser user, UserTokenClaims userClaims)
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

        return new AuthenticationResult(user, accessTokenInfo, refreshTokenInfo);
    }
}