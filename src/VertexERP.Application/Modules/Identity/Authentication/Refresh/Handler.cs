using Mapster;
using Mediator;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using VertexERP.Application.Common.Abstractions.Identity;
using VertexERP.Application.Common.Abstractions.Persistence;
using VertexERP.Application.Common.Models.Identity;
using VertexERP.Application.Services;
using VertexERP.Shared.Results;

namespace VertexERP.Application.Modules.Identity.Authentication.Refresh;

public sealed class Handler(IApplicationDbContext dbContext, IRefreshTokenHasher refreshTokenHasher,
    ILogger<Handler> logger, AuthenticationService authenticationService)
    : IRequestHandler<Command, Result<Response>>
{
    public async ValueTask<Result<Response>> Handle(Command request, CancellationToken cancellationToken)
    {
        var refreshTokenHash = refreshTokenHasher.Hash(request.RefreshToken);

        var refreshTokenContext = await dbContext.RefreshTokens.Where(x => x.TokenHash == refreshTokenHash)
            .ToRefreshTokenContext().SingleOrDefaultAsync(cancellationToken);

        if (refreshTokenContext is null || !refreshTokenContext.RefreshToken.IsActive)
            return Result<Response>.Unauthorized();



        var userClaims = new UserTokenClaims(refreshTokenContext.UserId, refreshTokenContext.UserEmail, refreshTokenContext.Permissions);
        var tokenPair = authenticationService.CreateSession(userClaims);

        refreshTokenContext.RefreshToken.Revoke(reason: "Token rotated automatically", replacedByTokenHash: tokenPair.RefreshToken);

        await dbContext.SaveChangesAsync(cancellationToken);

        logger.LogInformation("User {UserId} refreshed authentication tokens successfully.", refreshTokenContext.UserId);

        return Result<Response>.Success(tokenPair.Adapt<Response>());
    }
}