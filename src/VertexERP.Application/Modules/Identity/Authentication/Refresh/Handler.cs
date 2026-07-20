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

public sealed class Handler(
    IApplicationDbContext dbContext,
    IRefreshTokenHasher refreshTokenHasher,
    ILogger<Handler> logger,
    AuthenticationService authenticationService)
    : IRequestHandler<Command, Result<Response>>
{
    public async ValueTask<Result<Response>> Handle(Command request, CancellationToken cancellationToken)
    {
        var refreshTokenHash = refreshTokenHasher.Hash(request.RefreshToken);

        var tokenContext = await dbContext.RefreshTokens.Where(x => x.TokenHash == refreshTokenHash)
            .ToRefreshTokenContext().FirstOrDefaultAsync(cancellationToken);

        if (tokenContext is null || !tokenContext.RefreshToken.IsActive)
        {
            return Result<Response>.Unauthorized();
        }

        var currentToken = tokenContext.RefreshToken;

        var claims = new UserTokenClaims(tokenContext.UserId, tokenContext.UserEmail, tokenContext.Permissions);

        var tokenPair = await authenticationService.CreateSessionAsync(claims, cancellationToken);

        currentToken.Revoke(reason: "Token rotated automatically", replacedByTokenHash: tokenPair.RefreshToken);

        await dbContext.SaveChangesAsync(cancellationToken);

        logger.LogInformation("User {UserId} refreshed authentication tokens successfully.", tokenContext.UserId);

        return Result<Response>.Success(tokenPair.Adapt<Response>());
    }
}