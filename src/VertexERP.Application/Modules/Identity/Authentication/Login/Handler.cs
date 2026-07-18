using Mediator;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using VertexERP.Application.Common.Abstractions.Identity;
using VertexERP.Application.Common.Abstractions.Persistence;
using VertexERP.Application.Common.Abstractions.System;
using VertexERP.Application.Common.Models.Identity;
using VertexERP.Domain.Module.Identity.Entities;
using VertexERP.Shared.Results;

namespace VertexERP.Application.Modules.Identity.Authentication.Login;

public sealed class Handler(
    IApplicationDbContext context,
    IAccessTokenGenerator accessTokenGenerator,
    IRefreshTokenGenerator refreshTokenGenerator,
    IPasswordHasher passwordHasher,
    ILogger<Handler> logger,
    IClientInfoProvider clientInfoProvider)
    : IRequestHandler<Command, Result<Response>>
{
    public async ValueTask<Result<Response>> Handle(Command request, CancellationToken cancellationToken)
    {
        var userData = await context.Users
           .AsNoTracking()
           .Where(u => u.Email == request.Email)
           .Select(u => new
           {
               u.Id,
               u.PasswordHash,
               u.IsActive,
               Permissions = u.UserRoles
                   .Select(r => r.Rolepermission)
                   .Select(p => p.Permission.Name)
                   .ToList()
           })
           .FirstOrDefaultAsync(cancellationToken);

        var passwordValid = passwordHasher.Verify(request.Password, userData?.PasswordHash ?? string.Empty);

        if (userData is null || !userData.IsActive || !passwordValid)
        {
            logger.LogWarning("Failed login attempt for email: {Email}", request.Email);
            return Result<Response>.Unauthorized("Invalid email or password.");
        }

        var claims = new UserTokenClaims(
            userData.Id,
            userData.Email,
            userData.permissions);

        var accessToken = accessTokenGenerator.GenerateAccessToken(claims);
        var referhoekm = refreshTokenGenerator.GenerateRefreshToken();

        var refreshToken = new RefreshToken
        {
            UserId = userData.Id,
            TokenHash = accessTokenGenerator.HashToken(tokenResponse.RefreshToken),
            ExpiresAt = tokenResponse.RefreshTokenExpiration,
            CreatedByIp = clientInfoProvider.GetIpAddress(),
            DeviceInfo = clientInfoProvider.GetUserAgent()
        };

        await context.RefreshTokens.AddAsync(refreshToken, cancellationToken);
        await context.SaveChangesAsync(cancellationToken);

        logger.LogInformation("User {UserId} logged in successfully.", user.Id);

        var tokenResponse =

        return Result<Response>.Success(tokenResponse.Adapt<Response>());
    }
}
a