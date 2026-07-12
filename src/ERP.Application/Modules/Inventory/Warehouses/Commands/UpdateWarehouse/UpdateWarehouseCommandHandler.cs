using Mapster;
using MediatR;
using Microsoft.Extensions.Logging;
using VertexERP.Application.Abstractions.Persistence;
using VertexERP.Application.Abstractions.Storage;
using VertexERP.Shared.Results;

namespace VertexERP.Application.Modules.Inventory.Warehouses.Commands.UpdateWarehouse;


public class UpdateWarehouseCommandHandler
    : IRequestHandler<UpdateWarehouseCommand, Result<UpdateWarehouseCommandResponse>>
{
    private readonly IApplicationDbContext _dbContext;
    private readonly ILogger<UpdateWarehouseCommandHandler> _logger;
    private readonly IFileStorage _fileStorage;
    public UpdateWarehouseCommandHandler(
        IApplicationDbContext dbContext,
        ILogger<UpdateWarehouseCommandHandler> logger,
        IFileStorage fileStorage)
    {
        _dbContext = dbContext;
        _logger = logger;
        _fileStorage = fileStorage;
    }

    public async Task<Result<UpdateWarehouseCommandResponse>> Handle(UpdateWarehouseCommand request, CancellationToken cancellationToken)
    {


        var existingWarehouse = await _dbContext.Warehouses.FindAsync(request.Id, cancellationToken);

        if (existingWarehouse == null)
            return Result<UpdateWarehouseCommandResponse>.NotFound($"Warehouse with Id {request.Id} not found");


        request.Adapt(existingWarehouse);

        await _dbContext.SaveChangesAsync(cancellationToken);

        return Result<UpdateWarehouseCommandResponse>.Success(
            existingWarehouse.Adapt<UpdateWarehouseCommandResponse>(), "Product updated successfully");
    }
}

