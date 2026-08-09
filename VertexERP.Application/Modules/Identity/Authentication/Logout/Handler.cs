using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using VertexERP.Application.Common.Abstractions.Endpoint;
using VertexERP.Application.Common.Abstractions.Identity;
using VertexERP.Application.Common.Abstractions.Persistence;
using VertexERP.Application.Shared.Results;

namespace VertexERP.Application.Modules.Identity.Authentication.Logout;

public sealed class Handler(IAppDbContext dbContext, IRefreshTokenService refreshTokenService, ILogger<Handler> logger) : IHandler
{
    public async Task<Result<Response>> HandleAsync(Request request, CancellationToken ct)
    {
        var refreshTokenHash = refreshTokenService.ComputeHash(request.RefreshToken);

        var refreshToken = await dbContext.RefreshTokens.FirstOrDefaultAsync(x => x.TokenHash == refreshTokenHash, ct);

        if (refreshToken is null || !refreshToken.IsActive)
            return Result<Response>.Unauthorized();

        refreshToken.Revoke("Logged out by user");

        await dbContext.SaveChangesAsync(ct);

        logger.LogInformation("User logged out successfully for user {UserId}", refreshToken.UserId);

        return Result<Response>.Success(new Response());
    }
}


