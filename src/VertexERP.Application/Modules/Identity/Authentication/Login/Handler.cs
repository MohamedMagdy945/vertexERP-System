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
    public async Task<Result<Response>> HandleAsync(Query request, CancellationToken cancellationToken)
    {
        var email = request.Email.Trim().ToLowerInvariant();

        var context = await dbContext.Users.Where(x => x.Email == email).AsNoTracking()
                            .ToContext().SingleOrDefaultAsync(cancellationToken);

        if (context is null || !context.IsActive || !passwordHasher.Verify(request.Password, context.PasswordHash))
        {
            logger.LogWarning("Failed login attempt for email: {Email}", email);

            return Result<Response>.Unauthorized("Invalid email or password.");
        }

        var userClaims = new UserTokenClaims(context.UserId, context.Email, context.Roles);

        var tokenPair = authenticationService.CreateSession(userClaims);

        await dbContext.SaveChangesAsync(cancellationToken);

        logger.LogInformation("User {UserId} logged in successfully.", context.UserId);

        return Result<Response>.Success(tokenPair.Adapt<Response>());
    }
}