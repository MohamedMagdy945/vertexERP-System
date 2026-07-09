using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using VertexERP.Application.Abstractions.Authentication;
using VertexERP.Application.Abstractions.Persistence;
using VertexERP.Domain.Module.Identity.Entities;
using VertexERP.Shared.Results;

namespace VertexERP.Application.Modules.Identity.Authentication.Refresh;

public class RefreshCommandHandler
    : IRequestHandler<RefreshCommand, Result<RefreshResponse>>
{
    private readonly IApplicationDbContext _dbContext;
    private readonly ITokenGenerator _tokenGenerator;
    private readonly IPasswordHasher _passwordHasher;
    private readonly ILogger<RefreshCommandHandler> _logger;
    public RefreshCommandHandler(
        IApplicationDbContext dbContext,
        ITokenGenerator tokenGenerator,
        IPasswordHasher passwordHasher,
        ILogger<RefreshCommandHandler> logger)
    {
        _dbContext = dbContext;
        _tokenGenerator = tokenGenerator;
        _passwordHasher = passwordHasher;
        _logger = logger;
    }

    public async Task<Result<RefreshResponse>> Handle(RefreshCommand request, CancellationToken cancellationToken)
    {
        var gracePeriodLimit = DateTime.UtcNow.AddMinutes(-1);

        var refreshTokenHash = _tokenGenerator.HashToken(request.RefreshToken);

        var existingToken = await _dbContext.RefreshTokens
            .FirstOrDefaultAsync(rt =>
                rt.TokenHash == refreshTokenHash &&
                rt.ExpiresAt > DateTime.UtcNow &&
                (rt.RevokedAt == null || (rt.RevokedReason == "Replaced by new token" && rt.RevokedAt > gracePeriodLimit)),
                cancellationToken);



        if (existingToken is null)
            return Result<RefreshResponse>.Unauthorized("Invalid or expired refresh token.");

        var userData = await _dbContext.Users
            .AsNoTracking()
            .Where(u => u.Id == existingToken.UserId)
            .Select(u => new
            {
                u.Id,
                u.IsEnabled,
                u.Email,
                u.FullName,
                Permissions = u.UserPermissions
                    .Select(p => p.Permission.Name)
                    .ToList()
            })
            .FirstOrDefaultAsync(cancellationToken);

        if (userData is null || !userData.IsEnabled)
            return Result<RefreshResponse>.Unauthorized("User account not found.");

        var user = new User
        {
            Id = userData.Id,
            Email = userData.Email,
            FullName = userData.FullName
        };

        var tokenResponse = _tokenGenerator.GenerateTokenPair(user, userData.Permissions);

        if (existingToken.RevokedAt == null)
        {
            existingToken.RevokedAt = DateTime.UtcNow;
            existingToken.RevokedReason = "Replaced by new token";
        }

        var refreToken = new RefreshToken
        {
            UserId = user.Id,
            TokenHash = _tokenGenerator.HashToken(tokenResponse.RefreshToken),
            ExpiresAt = tokenResponse.RefreshTokenExpiration,
            CreatedByIp = existingToken.CreatedByIp,
            DeviceInfo = existingToken.DeviceInfo
        };

        await _dbContext.RefreshTokens.AddAsync(refreToken, cancellationToken);

        await _dbContext.SaveChangesAsync(cancellationToken);

        return Result<RefreshResponse>.Success(new RefreshResponse
        {
            UserId = user.Id,
            AccessToken = tokenResponse.AccessToken,
            RefreshToken = tokenResponse.RefreshToken,
            AccessTokenExpiration = tokenResponse.AccessTokenExpiration,
            RefreshTokenExpiration = tokenResponse.RefreshTokenExpiration
        });
    }
}

