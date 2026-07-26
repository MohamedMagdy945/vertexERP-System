using Microsoft.EntityFrameworkCore;
using VertexERP.Application.Common.Abstractions.Cache;
using VertexERP.Application.Common.Abstractions.Handler;
using VertexERP.Application.Common.Abstractions.Persistence;
using VertexERP.Application.Shared.Results;

namespace VertexERP.Application.Modules.Identity.Users.Delete;

public sealed class Handler(IAppDbContext dbContext, IUserPermissionCache userPermissionCache) : IHandler
{
    public async Task<Result<Response>> HandleAsync(Request request, CancellationToken cancellationToken)
    {
        var user = await dbContext.Users
            .SingleOrDefaultAsync(x => x.Id == request.Id, cancellationToken);

        if (user is null)
        {
            return Result<Response>.NotFound("User not found.");
        }

        dbContext.Users.Remove(user);

        await dbContext.SaveChangesAsync(cancellationToken);

        return Result<Response>.Success(new Response { Id = user.Id }, "User deleted successfully.");
    }
}