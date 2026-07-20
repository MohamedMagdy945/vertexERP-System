using Mapster;
using Mediator;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using VertexERP.Application.Common.Abstractions.Identity;
using VertexERP.Application.Common.Abstractions.Persistence;
using VertexERP.Application.Common.Models.Identity;
using VertexERP.Application.Modules.Identity.Authentication.Login;
using VertexERP.Application.Services;
using VertexERP.Shared.Results;

public sealed class Handler(
    IApplicationDbContext dbContext,
    IPasswordHasher passwordHasher,
    ILogger<Handler> logger,
    AuthenticationService authenticationService)
    : IRequestHandler<Command, Result<Response>>
{
    public async ValueTask<Result<Response>> Handle(Command request, CancellationToken cancellationToken)
    {
        var loginData = await dbContext.Users.Where(x => x.Email == request.Email)
            .ToLoginData().FirstOrDefaultAsync(cancellationToken);

        if (loginData is null || !loginData.IsActive || !passwordHasher.Verify(request.Password, loginData.PasswordHash))
        {
            logger.LogWarning("Failed login attempt for email: {Email}", request.Email);

            return Result<Response>.Unauthorized("Invalid email or password.");
        }
        var claims = new UserTokenClaims(loginData.Id, loginData.Email, loginData.Permissions);

        var tokenPair = await authenticationService.CreateSessionAsync(claims, cancellationToken);

        logger.LogInformation("User {UserId} logged in successfully.", loginData.Id);

        return Result<Response>.Success(tokenPair.Adapt<Response>());
    }
}