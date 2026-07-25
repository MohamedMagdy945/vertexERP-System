using Microsoft.EntityFrameworkCore;
using VertexERP.Application.Common.Abstractions.Cache;
using VertexERP.Application.Common.Abstractions.Handler;
using VertexERP.Application.Common.Abstractions.Persistence;
using VertexERP.Application.Shared.Results;

namespace VertexERP.Application.Modules.Identity.Users.GetById;

public sealed class Handler(IApplicationDbContext dbContext, IUserPermissionCache userPermissionCache) : IHandler
{
    public async Task<Result<Response>> HandleAsync(Request request, CancellationToken cancellationToken)
    {
        var user = await dbContext.Users
             .AsNoTracking()
             .Where(u => u.Id == request.Id)
             .ToResponse()
             .SingleOrDefaultAsync(cancellationToken);

        if (user is null)
            return Result<Response>.NotFound("User Not Found");

        user.Permissions = await userPermissionCache.GetAsync(request.Id, cancellationToken);

        return Result<Response>.Success(user);
    }
}