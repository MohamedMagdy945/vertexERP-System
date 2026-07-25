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

public sealed class Handler(IApplicationDbContext dbContext, IPasswordHasher passwordHasher
    , AuthenticationSessionService authenticationSessionService, ILogger<Handler> logger) : IHandler
{
    public async Task<Result<AuthenticationResult>> HandleAsync(Command request, CancellationToken cancellationToken)
    {
        var email = request.Email.Trim().ToLowerInvariant();

        var context = await dbContext.Users
            .Where(x => x.Email == email)
            .AsNoTracking()
            .ToContext()
            .SingleOrDefaultAsync(cancellationToken);

        if (context is null || !context.IsActive || !passwordHasher.Verify(request.Password, context.PasswordHash))
        {
            logger.LogWarning("Failed login attempt for email: {Email}", email);

            return Result<AuthenticationResult>.Unauthorized("Invalid email or password.");
        }

        var userClaims = new UserTokenClaims(context.UserId, context.Email, context.Roles);
        var authenticatedUser = new AuthenticatedUser(context.UserId, context.Email, string.Empty);

        var authenticationResult = authenticationSessionService.Create(authenticatedUser, userClaims);

        await dbContext.SaveChangesAsync(cancellationToken);

        logger.LogInformation("User {UserId} logged in successfully.",
            context.UserId);

        return Result<AuthenticationResult>.Success(authenticationResult);
    }
}