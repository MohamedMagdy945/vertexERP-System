using VertexERP.Application.Common.Abstractions.Http;
using VertexERP.Application.Common.Abstractions.Identity;
using VertexERP.Application.Common.Abstractions.Persistence;
using VertexERP.Application.Common.Types.Authentication.Contracts;
using VertexERP.Application.Common.Types.Authentication.Models;
using VertexERP.Domain.Module.Identity.Entities;


namespace VertexERP.Application.Services;

public sealed class AuthService(
    IAppDbContext dbContext,
    IAccessTokenGenerator accessTokenGenerator,
    IRefreshTokenService refreshTokenService,
    IClientInfoProvider clientInfoProvider,
    IUserPermissionService userPermissionService)
{
    public async Task<AuthenticationResult> CreateSessionAsync(
        SessionUser user,
        CancellationToken ct = default)
    {

        var permissions = await userPermissionService
            .GetPermissionsAsync(user.Id, ct);

        var userClaims = new UserTokenClaims(user.Id, user.Email);

        var accessTokenInfo = accessTokenGenerator.Generate(userClaims);

        var refreshTokenInfo = refreshTokenService.Generate();

        var refreshToken = new RefreshToken(
            tokenHash: refreshTokenService.ComputeHash(refreshTokenInfo.Token),
            userId: user.Id,
            expiresAt: refreshTokenInfo.ExpiresAt,
            createdByIp: clientInfoProvider.GetIpAddress(),
            deviceInfo: clientInfoProvider.GetUserAgent());

        dbContext.RefreshTokens.Add(refreshToken);

        await dbContext.SaveChangesAsync(ct);

        var authenticatedUser = new AuthenticatedUser
        {
            Id = user.Id,
            Name = user.Name,
            Email = user.Email,
            AvatarUrl = user.AvatarUrl,
            Portal = user.Portal,
            Roles = user.Roles,
            Permissions = permissions
        };

        return new AuthenticationResult(authenticatedUser, accessTokenInfo, refreshTokenInfo);
    }
}