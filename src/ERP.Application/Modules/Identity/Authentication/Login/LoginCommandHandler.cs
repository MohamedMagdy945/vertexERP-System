using Mapster;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using VertexERP.Application.Abstractions.Authentication;
using VertexERP.Application.Abstractions.Persistence;
using VertexERP.Domain.Module.Identity.Entities;
using VertexERP.Shared.Results;

namespace VertexERP.Application.Modules.Identity.Authentication.Login;

public class LoginCommandHandler
    : IRequestHandler<LoginCommand, Result<LoginResponse>>
{
    private readonly IApplicationDbContext _dbContext;
    private readonly ITokenGenerator _tokenGenerator;
    private readonly IPasswordHasher _passwordHasher;
    private readonly ILogger<LoginCommandHandler> _logger;
    private readonly IClientInfoProvider _clientInfoProvider;
    public LoginCommandHandler(
        IApplicationDbContext dbContext,
        ITokenGenerator tokenGenerator,
        IPasswordHasher passwordHasher,
        ILogger<LoginCommandHandler> logger,
        IClientInfoProvider clientInfoProvider)
    {
        _dbContext = dbContext;
        _tokenGenerator = tokenGenerator;
        _passwordHasher = passwordHasher;
        _logger = logger;
        _clientInfoProvider = clientInfoProvider;
    }

    public async Task<Result<LoginResponse>> Handle(LoginCommand request, CancellationToken cancellationToken)
    {
        var email = request.Email.Trim().ToLowerInvariant();

        var userData = await _dbContext.Users
           .AsNoTracking()
           .Where(u => u.Email == email)
           .Select(u => new
           {
               u.Id,
               u.PasswordHash,
               u.IsEnabled,
               u.Email,
               u.FullName,
               Permissions = u.UserPermissions
                   .Select(p => p.Permission.Name)
                   .ToList()
           })
           .FirstOrDefaultAsync(cancellationToken);


        var passwordValid = _passwordHasher.Verify(request.Password,
            userData?.PasswordHash ?? string.Empty);

        if (userData is null || !userData.IsEnabled || !passwordValid)
        {
            _logger.LogWarning("Failed login attempt for email: {Email}", email);
            return Result<LoginResponse>.Unauthorized("Invalid email or password.");
        }

        var user = new User
        {
            Id = userData.Id,
            Email = userData.Email,
            FullName = userData.FullName
        };

        var tokenResponse = _tokenGenerator.GenerateTokenPair(user, userData.Permissions);

        var refreshToken = new RefreshToken
        {
            UserId = userData.Id,
            TokenHash = _tokenGenerator.HashToken(tokenResponse.RefreshToken),
            ExpiresAt = tokenResponse.RefreshTokenExpiration,
            CreatedByIp = _clientInfoProvider.GetIpAddress(),
            DeviceInfo = _clientInfoProvider.GetUserAgent()
        };

        await _dbContext.RefreshTokens.AddAsync(refreshToken, cancellationToken);
        await _dbContext.SaveChangesAsync(cancellationToken);

        _logger.LogInformation(
              "User {UserId} logged in successfully.",
              user.Id);

        return Result<LoginResponse>.Success(tokenResponse.Adapt<LoginResponse>());

    }
}

