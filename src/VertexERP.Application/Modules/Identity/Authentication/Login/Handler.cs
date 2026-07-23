using Mapster;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using VertexERP.Application.Common.Abstractions.Handler;
using VertexERP.Application.Common.Abstractions.Identity;
using VertexERP.Application.Common.Abstractions.Persistence;
using VertexERP.Application.Common.Models.Identity;
using VertexERP.Application.Services;
using VertexERP.Shared.Results;

namespace VertexERP.Application.Modules.Identity.Authentication.Login;

public sealed class Handler(IApplicationDbContext dbContext, IPasswordHasher passwordHasher,
    ILogger<Handler> logger, AuthenticationService authenticationService) : IHandler
{
    public async Task<Result<Response>> HandleAsync(Command request, CancellationToken cancellationToken)
    {
        var email = request.Email.Trim().ToLowerInvariant();

        var loginContext = await dbContext.Users.Where(x => x.Email == email)
                             .ToLoginContext().SingleOrDefaultAsync(cancellationToken);

        if (loginContext is null || !loginContext.IsActive || !passwordHasher.Verify(request.Password, loginContext.PasswordHash))
        {
            logger.LogWarning("Failed login attempt for email: {Email}", email);

            return Result<Response>.Unauthorized("Invalid email or password.");
        }

        var userClaims = new UserTokenClaims(loginContext.UserId, loginContext.Email, loginContext.Permissions);

        var tokenPair = authenticationService.CreateSession(userClaims);

        await dbContext.SaveChangesAsync(cancellationToken);

        logger.LogInformation("User {UserId} logged in successfully.", loginContext.UserId);

        return Result<Response>.Success(tokenPair.Adapt<Response>());
    }
}