using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using VertexERP.Application.Common.Abstractions.Endpoint;
using VertexERP.Application.Common.Abstractions.Http;
using VertexERP.Application.Common.Abstractions.Identity;
using VertexERP.Application.Common.Abstractions.Persistence;
using VertexERP.Application.Common.Types.Authentication.Contracts;
using VertexERP.Application.Services;
using VertexERP.Application.Shared.Results;

namespace VertexERP.Application.Modules.Identity.Authentication.Refresh;

public sealed class Handler(IAppDbContext dbContext,
    IRefreshTokenService refreshTokenService,
    IClientInfoProvider clientInfoProvider,
     AuthService authService,
    ILogger<Handler> logger) : IHandler
{
    public async Task<Result<AuthenticationResult>> HandleAsync(Request request, CancellationToken ct)
    {
        var tokenHash = refreshTokenService.ComputeHash(request.RefreshToken);

        var context = await dbContext.RefreshTokens
            .Where(x => x.TokenHash == tokenHash)
            .ToContext()
            .SingleOrDefaultAsync(ct);

        if (context is null || context.RefreshToken.IsRevoked || context.RefreshToken.IsExpired || !context.IsActive)
        {
            logger.LogWarning("Invalid refresh token attempt.");

            return Result<AuthenticationResult>.Unauthorized("Invalid refresh token.");
        }

        context.RefreshToken.Revoke(clientInfoProvider.GetIpAddress());

        var sessionUser = new SessionUser
        {
            Id = context.Id,
            Name = context.Name,
            Email = context.Email,
            AvatarUrl = context.AvatarUrl,
            Portal = context.Portal,
        };

        var authenticationResult = await authService.CreateSessionAsync(sessionUser, ct);

        logger.LogInformation("User {UserId} refreshed authentication successfully.", context.Id);

        return Result<AuthenticationResult>.Success(authenticationResult);
    }
}