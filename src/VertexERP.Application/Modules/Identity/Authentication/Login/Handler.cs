using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using VertexERP.Application.Common.Abstractions.Endpoint;
using VertexERP.Application.Common.Abstractions.Identity;
using VertexERP.Application.Common.Abstractions.Persistence;
using VertexERP.Application.Common.Extensions;
using VertexERP.Application.Common.Types.Authentication.Contracts;
using VertexERP.Application.Services;
using VertexERP.Application.Shared.Results;

namespace VertexERP.Application.Modules.Identity.Authentication.Login;

public sealed class Handler(
    IAppDbContext dbContext,
    IPasswordHasher passwordHasher,
    AuthService authService,
    ILogger<Handler> logger)
    : IHandler
{
    public async Task<Result<AuthenticationResult>> HandleAsync(
        Request request,
        CancellationToken ct)
    {
        var email = request.Email.ToCleanString();

        var context = await dbContext.Users
            .AsNoTracking()
            .Where(x => x.Email == email)
            .ToContext()
            .SingleOrDefaultAsync(ct);


        if (context is null || !context.IsActive || !passwordHasher.Verify(request.Password, context.PasswordHash))
        {
            logger.LogWarning("Failed login attempt for email {Email}.", email);

            return Result<AuthenticationResult>.Unauthorized("Invalid email or password.");
        }
        var sessionUser = new SessionUser
        {
            Id = context.Id,
            Name = context.Name,
            Email = context.Email,
            AvatarUrl = context.AvatarUrl,
            Portal = context.Portal
        };

        var roles = await dbContext
            .GetRoleNames(sessionUser.Id)
            .ToListAsync(ct);


        var authenticationResult = await authService.CreateSessionAsync(sessionUser, ct);

        logger.LogInformation("User {UserId} logged in successfully.", sessionUser.Id);

        return Result<AuthenticationResult>.Success(authenticationResult);
    }
}