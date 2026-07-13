using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using VertexERP.Application.Abstractions.Persistence;
using VertexERP.Shared.Results;

namespace VertexERP.Application.Modules.Inventory.Stocks.Commands.DeleteStock;


public class DeleteStockCommandHandler
    : IRequestHandler<DeleteStockCommand, Result<DeleteStockCommandResponse>>
{
    private readonly IApplicationDbContext _context;
    private readonly ILogger<DeleteStockCommandHandler> _logger;
    public DeleteStockCommandHandler(
        IApplicationDbContext dbContext,
        ILogger<DeleteStockCommandHandler> logger)
    {
        _context = dbContext;
        _logger = logger;
    }

    public async Task<Result<DeleteStockCommandResponse>> Handle(DeleteStockCommand request, CancellationToken cancellationToken)
    {
        var response = Result<DeleteStockCommandResponse>.Create();

        var affectedRows = await _context.Stocks
            .Where(s => s.ProductId == request.ProductId && s.WarehouseId == request.WarehouseId)
            .ExecuteDeleteAsync(cancellationToken);

        if (affectedRows == 0)
        {
            return response.NotFound();
        }

        return response.Deleted();
    }
}

