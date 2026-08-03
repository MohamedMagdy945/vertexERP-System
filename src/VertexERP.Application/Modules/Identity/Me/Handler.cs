using Microsoft.EntityFrameworkCore;
using VertexERP.Application.Common.Abstractions.Cache;
using VertexERP.Application.Common.Abstractions.Endpoint;
using VertexERP.Application.Common.Abstractions.Identity;
using VertexERP.Application.Common.Abstractions.Persistence;
using VertexERP.Application.Shared.Results;

namespace VertexERP.Application.Modules.Identity.Me;

public sealed class Handler(IAppDbContext dbContext,
    ICurrentUser currentUserService, IUserPermissionCache userPermissionCache) : IHandler
{
    public async Task<Result<Response>> HandleAsync(Request request, CancellationToken cancellationToken)
    {
        var user = await dbContext.Users
            .AsNoTracking()
            .Where(u => u.Id == currentUserService.UserId)
            .ToResponse()
            .SingleOrDefaultAsync(cancellationToken);

        if (user is null)
            return Result<Response>.Unauthorized();

        user.Permissions = await userPermissionCache.GetAsync(currentUserService.UserId, cancellationToken);

        return Result<Response>.Success(user);
    }
}