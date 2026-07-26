using Microsoft.EntityFrameworkCore;
using VertexERP.Application.Common.Abstractions.Handler;
using VertexERP.Application.Common.Abstractions.Persistence;
using VertexERP.Application.Shared.Results;

namespace VertexERP.Application.Modules.Identity.Users.Roles.Get;

public sealed class Handler(IApplicationDbContext dbContext) : IHandler
{
    public async Task<Result<Response>> HandleAsync(Request request, CancellationToken cancellationToken)
    {
        var exists = await dbContext.Users
            .AsNoTracking()
            .AnyAsync(x => x.Id == request.Id, cancellationToken);

        if (!exists)
            return Result<Response>.NotFound("User not found.");


        var roles = await dbContext.Users
        .Where(x => x.Id == request.Id)
        .AsNoTracking()
        .ToResponse()
        .ToListAsync(cancellationToken);

        return Result<Response>.Success(new Response { Roles = roles });
    }
}