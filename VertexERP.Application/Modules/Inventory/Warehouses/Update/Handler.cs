using Mapster;
using Microsoft.EntityFrameworkCore;
using VertexERP.Application.Common.Abstractions.Endpoint;
using VertexERP.Application.Common.Abstractions.Persistence;
using VertexERP.Application.Shared.Results;

namespace VertexERP.Application.Modules.Inventory.Warehouses.Update;

public sealed class Handler(IAppDbContext dbContext) : IHandler
{
    public async Task<Result<Response>> HandleAsync(Guid id, Request request, CancellationToken ct)
    {
        var affectedRows = await dbContext.Warehouses
        .Where(x => x.Id == id)
        .ExecuteUpdateAsync(setters => setters
            .SetProperty(x => x.Name, request.Name)
            .SetProperty(x => x.Code, request.Code)
            .SetProperty(x => x.Location, request.Location)
            .SetProperty(x => x.UpdatedAt, DateTime.UtcNow), ct);

        if (affectedRows == 0)
            return Result<Response>.NotFound("Warehouse not found.");

        return Result<Response>.Success(request.Adapt<Response>());
    }
}