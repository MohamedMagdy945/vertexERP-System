using VertexERP.Application.Common.Abstractions.Http;
using VertexERP.Application.Common.Abstractions.Identity;
using VertexERP.Application.Common.Abstractions.Persistence;
using VertexERP.Application.Common.Models.Identity;
using VertexERP.Domain.Module.Identity.Entities;

namespace VertexERP.Infrastructure.Identity.Authentication;

public class AuthenticationService(
    IApplicationDbContext context,
    ITokenPairGenerator tokenPairGenerator,
    IClientInfoProvider clientInfoProvider
    ) : IAuthenticationService
{
    public async Task<TokenPair> CreateSessionAsync(UserTokenClaims claims, CancellationToken cancellationToken)
    {
        var result = tokenPairGenerator.Generate(claims);

        var refreshToken = new RefreshToken
        (
            tokenHash: result.RefreshTokenHash,
            userId: claims.UserId,
            expiresAt: result.RefreshTokenExpiresAt,
            createdByIp: clientInfoProvider.GetIpAddress(),
            deviceInfo: clientInfoProvider.GetUserAgent()
        );

        await context.RefreshTokens.AddAsync(refreshToken, cancellationToken);
        await context.SaveChangesAsync(cancellationToken);

        return result;
    }

}

