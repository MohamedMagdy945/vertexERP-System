using Microsoft.EntityFrameworkCore;
using VertexERP.Application.Common.Abstractions.Endpoint;
using VertexERP.Application.Common.Abstractions.Persistence;
using VertexERP.Application.Shared.Results;

namespace VertexERP.Application.Modules.Identity.Roles.GetById;

public sealed class Handler(IAppDbContext dbContext) : IHandler
{
    public async Task<Result<Response>> HandleAsync(Guid id, CancellationToken ct)
    {
        var role = await dbContext.Roles
         .AsNoTracking()
         .Where(x => x.Id == id)
         .ToResponse()
         .SingleOrDefaultAsync(ct);

        if (role is null)
            return Result<Response>.NotFound("Role not found.");

        return Result<Response>.Success(role);
    }
}