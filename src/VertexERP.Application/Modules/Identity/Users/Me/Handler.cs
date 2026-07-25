using Mapster;
using Microsoft.EntityFrameworkCore;
using VertexERP.Application.Common.Abstractions.Handler;
using VertexERP.Application.Common.Abstractions.Identity;
using VertexERP.Application.Common.Abstractions.Persistence;
using VertexERP.Application.Shared.Results;

namespace VertexERP.Application.Modules.Identity.Users.Me;

public sealed class Handler(IApplicationDbContext dbContext, ICurrentUserService currentUser) : IHandler
{
    public async Task<Result<Response>> HandleAsync(CancellationToken cancellationToken)
    {
        var response = await dbContext.Users
              .AsNoTracking().Where(u => u.Id == currentUser.UserId)
              .ProjectToType<Response>().SingleOrDefaultAsync(cancellationToken);

        if (response is null)
            return Result<Response>.NotFound();

        return Result<Response>.Success(response);
    }
}