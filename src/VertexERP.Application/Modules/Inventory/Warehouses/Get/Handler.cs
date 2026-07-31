using Microsoft.EntityFrameworkCore;
using VertexERP.Application.Common.Abstractions.Handler;
using VertexERP.Application.Common.Abstractions.Persistence;
using VertexERP.Application.Services.Get;
using VertexERP.Application.Shared.Results;

namespace VertexERP.Application.Modules.Inventory.Warehouses.Get;

public sealed class Handler(IAppDbContext dbContext) : IHandler
{
    public async Task<Result<IReadOnlyList<Response>>> HandleAsync(Request request, CancellationToken ct)
    {
        var warehouses = await dbContext.Warehouses
            .AsNoTracking()
            .ToResponse()
            .ToListAsync(ct);

        return Result<IReadOnlyList<Response>>.Success(warehouses);
    }
}