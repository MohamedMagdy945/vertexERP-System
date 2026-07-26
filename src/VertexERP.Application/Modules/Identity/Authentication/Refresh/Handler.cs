using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using VertexERP.Application.Common.Abstractions.Handler;
using VertexERP.Application.Common.Abstractions.Identity;
using VertexERP.Application.Common.Abstractions.Persistence;
using VertexERP.Application.Common.Extensions;
using VertexERP.Application.Services;
using VertexERP.Application.Shared.Results;
using VertexERP.Application.Types.Authentication.Contracts;
using VertexERP.Application.Types.Authentication.Models;

namespace VertexERP.Application.Modules.Identity.Authentication.Refresh;

public sealed class Handler(
    IAppDbContext dbContext,
    IRefreshTokenService refreshTokenService,
    IUserPermissionService userPermissionService,
    SessionService sessionService,
    ILogger<Handler> logger) : IHandler
{
    public async Task<Result<AuthenticationResult>> HandleAsync(Request request, CancellationToken ct)
    {
        var refreshTokenHash = refreshTokenService.ComputeHash(request.RefreshToken);

        var context = await dbContext
            .RefreshTokens
            .Where(x => x.TokenHash == refreshTokenHash)
            .ToContext()
            .SingleOrDefaultAsync(ct);

        if (context is null || !context.RefreshToken.IsActive)
            return Result<AuthenticationResult>.Unauthorized();



        var roles = await dbContext.GetRoleNames(context.UserId).ToListAsync(ct);
        var userClaims = new UserTokenClaims(context.UserId, context.UserEmail, roles);


        var permissions = await userPermissionService.GetPermissionsAsync(context.UserId, ct);

        var authenticatedUser = new AuthenticatedUser
        {
            Id = context.UserId,
            Email = context.UserEmail,
            Portal = context.PortalType,
            Roles = roles,
            Permissions = permissions
        };

        var tokenPair = sessionService.Create(userClaims);

        context.RefreshToken.Revoke(
            reason: "Token rotated automatically",
            replacedByTokenHash: tokenPair.RefreshToken.Token);

        await dbContext.SaveChangesAsync(ct);

        logger.LogInformation("User {UserId} refreshed authentication tokens successfully.", context.UserId);

        return Result<AuthenticationResult>.Success(new AuthenticationResult(authenticatedUser, tokenPair));
    }
}