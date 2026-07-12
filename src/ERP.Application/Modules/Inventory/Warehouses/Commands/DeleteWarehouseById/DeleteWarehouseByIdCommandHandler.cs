using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using VertexERP.Application.Abstractions.Persistence;
using VertexERP.Application.Abstractions.Storage;
using VertexERP.Shared.Results;

namespace VertexERP.Application.Modules.Inventory.Warehouses.Commands.DeleteWarehouseById;


public class DeleteWarehouseByIdCommandHandler
    : IRequestHandler<DeleteWarehouseByIdCommand, Result<DeleteWarehouseByIdCommandResponse>>
{
    private readonly IApplicationDbContext _dbContext;
    private readonly ILogger<DeleteWarehouseByIdCommandHandler> _logger;
    private readonly IFileStorage _fileStorage;

    public DeleteWarehouseByIdCommandHandler(
        IApplicationDbContext dbContext,
        ILogger<DeleteWarehouseByIdCommandHandler> logger,
        IFileStorage fileStorage)
    {
        _dbContext = dbContext;
        _logger = logger;
        _fileStorage = fileStorage;
    }

    public async Task<Result<DeleteWarehouseByIdCommandResponse>> Handle(DeleteWarehouseByIdCommand request, CancellationToken cancellationToken)
    {
        var response = Result<DeleteWarehouseByIdCommandResponse>.Create();

        var affectedRows = await _dbContext.Warehouses.Where(w => w.Id == request.Id).ExecuteDeleteAsync(cancellationToken);

        if (affectedRows == 0)
        {
            return response.NotFound($"Warehouse with Id {request.Id} not found.");
        }

        return response.Deleted();
    }
}

