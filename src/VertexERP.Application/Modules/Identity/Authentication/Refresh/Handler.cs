using Mapster;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using VertexERP.Application.Common.Abstractions.Handler;
using VertexERP.Application.Common.Abstractions.Identity;
using VertexERP.Application.Common.Abstractions.Persistence;
using VertexERP.Application.Common.Models.Identity;
using VertexERP.Application.Services;
using VertexERP.Shared.Results;

namespace VertexERP.Application.Modules.Identity.Authentication.Refresh;

public sealed class Handler(IApplicationDbContext dbContext, IRefreshTokenHasher refreshTokenHasher
    , AuthenticationService authenticationService, ILogger<Handler> logger) : IHandler
{
    public async ValueTask<Result<Response>> HandleAsync(Command request, CancellationToken cancellationToken)
    {
        var refreshTokenHash = refreshTokenHasher.Hash(request.RefreshToken);

        var context = await dbContext.RefreshTokens.Where(x => x.TokenHash == refreshTokenHash)
                            .ToContext().SingleOrDefaultAsync(cancellationToken);

        if (context is null || !context.RefreshToken.IsActive)
            return Result<Response>.Unauthorized();

        var userClaims = new UserTokenClaims(context.UserId, context.UserEmail, context.Roles);

        var tokenPair = authenticationService.CreateSession(userClaims);

        var newRefreshTokenHash = refreshTokenHasher.Hash(tokenPair.RefreshToken);

        context.RefreshToken.Revoke(reason: "Token rotated automatically", replacedByTokenHash: newRefreshTokenHash);

        await dbContext.SaveChangesAsync(cancellationToken);

        logger.LogInformation("User {UserId} refreshed authentication tokens successfully.", context.UserId);

        return Result<Response>.Success(tokenPair.Adapt<Response>());
    }
}