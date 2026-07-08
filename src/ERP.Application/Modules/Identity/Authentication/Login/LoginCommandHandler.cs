using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using VertexERP.Application.Abstractions.Authentication;
using VertexERP.Application.Abstractions.Persistence;
using VertexERP.Shared.Results;

namespace VertexERP.Application.Modules.Identity.Authentication.Login;

public class LoginCommandHandler
    : IRequestHandler<LoginCommand, Result<LoginResponse>>
{
    private readonly IApplicationDbContext _dbContext;
    private readonly ITokenGenerator _tokenGenerator;
    private readonly IPasswordHasher _passwordHasher;
    private readonly ILogger<LoginCommandHandler> _logger;
    public LoginCommandHandler(
        IApplicationDbContext dbContext,
        ITokenGenerator tokenGenerator,
        IPasswordHasher passwordHasher,
        ILogger<LoginCommandHandler> logger)
    {
        _dbContext = dbContext;
        _tokenGenerator = tokenGenerator;
        _passwordHasher = passwordHasher;
        _logger = logger;
    }

    public async Task<Result<LoginResponse>> Handle(LoginCommand request, CancellationToken cancellationToken)
    {
        var email = request.Email.Trim().ToLowerInvariant();

        var result = await _dbContext.Users
             .AsNoTracking()
             .Where(x => x.Email == email)
             .Select(x => new
             {
                 User = x,
                 Permissions = x.UserPermissions
                     .Select(up => up.Permission.Name)
                     .ToList()
             })
             .FirstOrDefaultAsync(cancellationToken);

        var passwordValid = _passwordHasher.Verify(
          request.Password,
          result?.User.PasswordHash ?? string.Empty);

        if (result?.User is null || !result.User.IsEnabled || !passwordValid)
            return Result<LoginResponse>.Unauthorized("Invalid email or password.");

        var tokenResponse = _tokenGenerator.GenerateTokenPair(result.User, result.Permissions);

    }
}

