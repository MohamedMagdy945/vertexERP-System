using Mapster;
using VertexERP.Application.Common.Abstractions.Handler;
using VertexERP.Application.Common.Abstractions.Persistence;
using VertexERP.Application.Shared.Results;
using VertexERP.Domain.Module.Inventory.Entities;

namespace VertexERP.Application.Modules.Inventory.Warehouses.Create;

public sealed class Handler(IAppDbContext dbContext) : IHandler
{
    public async Task<Result<Response>> HandleAsync(Request request, CancellationToken ct)
    {
        var warehouse = new Warehouse(request.Name, request.Code, request.Location);

        dbContext.Warehouses.Add(warehouse);

        await dbContext.SaveChangesAsync(ct);

        return Result<Response>.Created(warehouse.Adapt<Response>());
    }
}