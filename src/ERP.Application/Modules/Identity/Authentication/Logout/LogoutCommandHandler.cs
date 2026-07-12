using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using VertexERP.Application.Abstractions.Authentication;
using VertexERP.Application.Abstractions.Persistence;
using VertexERP.Shared.Results;

namespace VertexERP.Application.Modules.Identity.Authentication.Logout;

public class LogoutCommandHandler
    : IRequestHandler<LogoutCommand, Result<LogoutResponse>>
{
    private readonly IApplicationDbContext _dbContext;
    private readonly ITokenGenerator _tokenGenerator;
    private readonly IPasswordHasher _passwordHasher;
    private readonly ILogger<LogoutCommandHandler> _logger;
    public LogoutCommandHandler(
        IApplicationDbContext dbContext,
        ITokenGenerator tokenGenerator,
        IPasswordHasher passwordHasher,
        ILogger<LogoutCommandHandler> logger)
    {
        _dbContext = dbContext;
        _tokenGenerator = tokenGenerator;
        _passwordHasher = passwordHasher;
        _logger = logger;
    }

    public async Task<Result<LogoutResponse>> Handle(LogoutCommand request, CancellationToken cancellationToken)
    {
        var result = Result<LogoutResponse>.Create();

        var refreshTokenHash = _tokenGenerator.HashToken(request.RefreshToken);

        var refreshToken = await _dbContext.RefreshTokens
         .FirstOrDefaultAsync(
             x => x.TokenHash == refreshTokenHash,
             cancellationToken);


        if (refreshToken is null || refreshToken.RevokedAt is not null)
            return result.Success(new LogoutResponse());

        refreshToken.RevokedAt = DateTime.UtcNow;
        refreshToken.RevokedReason = "User logged out.";

        await _dbContext.SaveChangesAsync(cancellationToken);

        return result.Success(new LogoutResponse());
    }
}

