using Mediator;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using VertexERP.Application.Common.Abstractions.Identity;
using VertexERP.Application.Common.Abstractions.Persistence;
using VertexERP.Shared.Results;

namespace VertexERP.Application.Modules.Identity.Authentication.Logout;

public sealed class Handler(
    IApplicationDbContext dbContext,
    IRefreshTokenHasher refreshTokenHasher,
    ILogger<Handler> logger)
    : IRequestHandler<Command, Result<Response>>
{
    public async ValueTask<Result<Response>> Handle(Command request, CancellationToken cancellationToken)
    {
        var refreshTokenHash = refreshTokenHasher.Hash(request.RefreshToken);

        var token = await dbContext.RefreshTokens.FirstOrDefaultAsync(t => t.TokenHash == refreshTokenHash, cancellationToken);

        if (token is null || !token.IsActive)
            return Result<Response>.Failure("Session.NotFound", "Active session not found or already logged out.");

        token.Revoke("Logged out by user");

        await dbContext.SaveChangesAsync(cancellationToken);

        return Result<Response>.Success(new Response());
    }
}