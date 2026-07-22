using Mediator;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using VertexERP.Application.Common.Abstractions.Identity;
using VertexERP.Application.Common.Abstractions.Persistence;
using VertexERP.Shared.Results;

namespace VertexERP.Application.Modules.Identity.Authentication.Logout;

public sealed class Handler(
    IApplicationDbContext dbContext,
    IRefreshTokenHasher refreshTokenHasher,
    ILogger<Handler> logger)
    : IRequestHandler<Command, Result<Response>>
{
    public async ValueTask<Result<Response>> Handle(Command request, CancellationToken cancellationToken)
    {
        var refreshTokenHash = refreshTokenHasher.Hash(request.RefreshToken);

        var refreshToken = await dbContext.RefreshTokens.FirstOrDefaultAsync(x => x.TokenHash == refreshTokenHash, cancellationToken);

        if (refreshToken is null || !refreshToken.IsActive)
            return Result<Response>.Failure("Session.NotFound", "Active session not found or already logged out.");

        refreshToken.Revoke("Logged out by user");

        await dbContext.SaveChangesAsync(cancellationToken);

        logger.LogInformation("User logged out successfully for user {UserId}", refreshToken.UserId);

        return Result<Response>.Success(new Response());
    }
}