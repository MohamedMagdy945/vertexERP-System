using Microsoft.EntityFrameworkCore;
using VertexERP.Application.Common.Abstractions.Handler;
using VertexERP.Application.Common.Abstractions.Persistence;
using VertexERP.Application.Shared.Results;

namespace VertexERP.Application.Modules.Identity.Users.GetById;

public sealed class Handler(IApplicationDbContext dbContext) : IHandler
{
    public async Task<Result<Response>> HandleAsync(Query request, CancellationToken cancellationToken)
    {
        var user = await dbContext.Users
            .AsNoTracking()
            .Where(u => u.Id == request.Id)
            .ToResponse()
            .FirstOrDefaultAsync(cancellationToken);

        if (user is null)
            return Result<Response>.NotFound("User Not Found");

        return Result<Response>.Success(user);
    }
}