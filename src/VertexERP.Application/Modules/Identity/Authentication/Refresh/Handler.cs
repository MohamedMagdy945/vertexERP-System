using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using VertexERP.Application.Common.Abstractions.Handler;
using VertexERP.Application.Common.Abstractions.Identity;
using VertexERP.Application.Common.Abstractions.Persistence;
using VertexERP.Application.Services;
using VertexERP.Application.Shared.Results;
using VertexERP.Application.Types.Authentication.Contracts;
using VertexERP.Application.Types.Authentication.Models;

namespace VertexERP.Application.Modules.Identity.Authentication.Refresh;

public sealed class Handler(IApplicationDbContext dbContext, IRefreshTokenService refreshTokenService,
    SessionService sessionService,
    ILogger<Handler> logger) : IHandler
{
    public async Task<Result<AuthenticationResult>> HandleAsync(Request request, CancellationToken cancellationToken)
    {
        var refreshTokenHash = refreshTokenService.ComputeHash(request.RefreshToken);

        var context = await dbContext
            .RefreshTokens
            .Where(x => x.TokenHash == refreshTokenHash)
            .ToContext()
            .SingleOrDefaultAsync(cancellationToken);

        if (context is null || !context.RefreshToken.IsActive)
            return Result<AuthenticationResult>.Unauthorized();


        var userClaims = new UserTokenClaims(context.UserId, context.UserEmail, context.Roles);
        var authenticatedUser = new AuthenticatedUser(context.UserId, context.UserEmail, string.Empty, context.Roles);

        var tokenPair = sessionService.Create(userClaims);

        context.RefreshToken.Revoke(reason: "Token rotated automatically", replacedByTokenHash: tokenPair.RefreshToken.Token);

        await dbContext.SaveChangesAsync(cancellationToken);

        logger.LogInformation("User {UserId} refreshed authentication tokens successfully.", context.UserId);

        return Result<AuthenticationResult>.Success(new AuthenticationResult(authenticatedUser, tokenPair));
    }
}