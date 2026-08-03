using Microsoft.EntityFrameworkCore;
using VertexERP.Application.Common.Abstractions.Endpoint;
using VertexERP.Application.Common.Abstractions.Persistence;
using VertexERP.Application.Shared.Results;

namespace VertexERP.Application.Modules.Inventory.Warehouses.GetById;

public sealed class Handler(IAppDbContext dbContext) : IHandler
{
    public async Task<Result<Response>> HandleAsync(Request request, CancellationToken ct)
    {
        var category = await dbContext.Warehouses
                .AsNoTracking()
                .Where(x => x.Id == request.Id)
                .ToResponse()
                .SingleOrDefaultAsync(ct);

        if (category is null)
            return Result<Response>.NotFound("Category not found.");

        return Result<Response>.Success(category);
    }
}