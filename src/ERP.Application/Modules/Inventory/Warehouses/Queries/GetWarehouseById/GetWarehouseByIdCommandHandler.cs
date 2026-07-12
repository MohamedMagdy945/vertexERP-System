using Mapster;
using MediatR;
using Microsoft.Extensions.Logging;
using VertexERP.Application.Abstractions.Persistence;
using VertexERP.Shared.Results;

namespace VertexERP.Application.Modules.Inventory.Warehouses.Queries.GetWarehouseById;


public class GetWarehouseByIdCommandHandler
    : IRequestHandler<GetWarehouseByIdCommand, Result<GetWarehouseByIdCommandResponse>>
{
    private readonly IApplicationDbContext _dbContext;
    private readonly ILogger<GetWarehouseByIdCommandHandler> _logger;
    public GetWarehouseByIdCommandHandler(
        IApplicationDbContext dbContext,
        ILogger<GetWarehouseByIdCommandHandler> logger)
    {
        _dbContext = dbContext;
        _logger = logger;
    }

    public async Task<Result<GetWarehouseByIdCommandResponse>> Handle(GetWarehouseByIdCommand request, CancellationToken cancellationToken)
    {
        var warehosue = await _dbContext.Warehouses.FindAsync(request.Id, cancellationToken);

        if (warehosue == null)
        {
            return Result<GetWarehouseByIdCommandResponse>.NotFound($"Warehouse with Id {request.Id} not found.");
        }

        return Result<GetWarehouseByIdCommandResponse>.Success(
            warehosue.Adapt<GetWarehouseByIdCommandResponse>(), "Warehouse found successfully.");

    }
}

