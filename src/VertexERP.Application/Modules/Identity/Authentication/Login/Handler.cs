using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using VertexERP.Application.Common.Abstractions.Handler;
using VertexERP.Application.Common.Abstractions.Identity;
using VertexERP.Application.Common.Abstractions.Persistence;
using VertexERP.Application.Services;
using VertexERP.Application.Shared.Results;
using VertexERP.Application.Types.Authentication.Contracts;
using VertexERP.Application.Types.Authentication.Models;

namespace VertexERP.Application.Modules.Identity.Authentication.Login;

public sealed class Handler(IAppDbContext dbContext, IPasswordHasher passwordHasher
    , SessionService sessionService, ILogger<Handler> logger) : IHandler
{
    public async Task<Result<AuthenticationResult>> HandleAsync(Request request, CancellationToken ct)
    {
        var email = request.Email.Trim().ToLowerInvariant();

        var context = await dbContext.Users
            .Where(x => x.Email == email)
            .AsNoTracking()
            .ToContext()
            .SingleOrDefaultAsync(ct);

        if (context is null || !context.IsActive || !passwordHasher.Verify(request.Password, context.PasswordHash))
        {
            logger.LogWarning("Failed login attempt for email: {Email}", email);

            return Result<AuthenticationResult>.Unauthorized("Invalid email or password.");
        }

        var userClaims = new UserTokenClaims(context.UserId, context.Email, context.Roles);
        var authenticatedUser = new AuthenticatedUser(context.UserId, context.Email, string.Empty, context.Roles);

        var tokenPair = sessionService.Create(userClaims);

        await dbContext.SaveChangesAsync(ct);

        logger.LogInformation("User {UserId} logged in successfully.",
            context.UserId);

        return Result<AuthenticationResult>.Success(new AuthenticationResult(authenticatedUser, tokenPair));
    }
}