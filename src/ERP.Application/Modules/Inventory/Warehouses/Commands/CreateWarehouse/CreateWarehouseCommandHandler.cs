using Mapster;
using MediatR;
using Microsoft.Extensions.Logging;
using VertexERP.Application.Abstractions.Persistence;
using VertexERP.Application.Abstractions.Storage;
using VertexERP.Domain.Module.Inventory.Entities;
using VertexERP.Shared.Results;

namespace VertexERP.Application.Modules.Inventory.Warehouses.Commands.CreateWarehouse;


public class CreateWarehouseCommandHandler
    : IRequestHandler<CreateWarehouseCommand, Result<CreateWarehouseCommandResponse>>
{
    private readonly IApplicationDbContext _dbContext;
    private readonly ILogger<CreateWarehouseCommandHandler> _logger;
    private readonly IFileStorage _fileStorage;
    public CreateWarehouseCommandHandler(
        IApplicationDbContext dbContext,
        ILogger<CreateWarehouseCommandHandler> logger,
        IFileStorage fileStorage)
    {
        _dbContext = dbContext;
        _logger = logger;
        _fileStorage = fileStorage;
    }

    public async Task<Result<CreateWarehouseCommandResponse>> Handle(CreateWarehouseCommand request, CancellationToken cancellationToken)
    {


        var warehouse = request.Adapt<Warehouse>();


        await _dbContext.Warehouses.AddAsync(warehouse, cancellationToken);
        await _dbContext.SaveChangesAsync(cancellationToken);

        return Result<CreateWarehouseCommandResponse>.
            Success(warehouse.Adapt<CreateWarehouseCommandResponse>(), "Warehouse created successfully");

    }
}

