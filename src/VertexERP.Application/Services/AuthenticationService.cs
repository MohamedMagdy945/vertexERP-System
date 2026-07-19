using VertexERP.Application.Common.Abstractions.Http;
using VertexERP.Application.Common.Abstractions.Identity;
using VertexERP.Application.Common.Abstractions.Persistence;
using VertexERP.Application.Common.Models.Identity;
using VertexERP.Domain.Module.Identity.Entities;

namespace VertexERP.Application.Services;

public sealed class AuthenticationService(IApplicationDbContext context,
    ITokenPairGenerator tokenPairGenerator,
    IClientInfoProvider clientInfoProvider)
{
    public async Task<TokenPair> CreateSessionAsync(UserTokenClaims claims, CancellationToken cancellationToken)
    {
        var tokenPair = tokenPairGenerator.Generate(claims);

        var refreshToken = new RefreshToken
        (
            tokenHash: tokenPair.RefreshTokenHash,
            userId: claims.UserId,
            expiresAt: tokenPair.RefreshTokenExpiresAt,
            createdByIp: clientInfoProvider.GetIpAddress(),
            deviceInfo: clientInfoProvider.GetUserAgent()
        );

        await context.RefreshTokens.AddAsync(refreshToken, cancellationToken);
        await context.SaveChangesAsync(cancellationToken);

        return tokenPair with { RefreshTokenHash = string.Empty };
    }

}

