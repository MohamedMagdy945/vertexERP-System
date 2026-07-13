using Mapster;
using MediatR;
using Microsoft.Extensions.Logging;
using VertexERP.Application.Abstractions.Persistence;
using VertexERP.Domain.Module.Inventory.Entities;
using VertexERP.Shared.Results;

namespace VertexERP.Application.Modules.Inventory.Stocks.Commands.CreateStock;


public class CreateStockCommandHandler
    : IRequestHandler<CreateStockCommand, Result<CreateStockCommandResponse>>
{
    private readonly IApplicationDbContext _context;
    private readonly ILogger<CreateStockCommandHandler> _logger;
    public CreateStockCommandHandler(
        IApplicationDbContext dbContext,
        ILogger<CreateStockCommandHandler> logger)
    {
        _context = dbContext;
        _logger = logger;
    }

    public async Task<Result<CreateStockCommandResponse>> Handle(CreateStockCommand request, CancellationToken cancellationToken)
    {
        var response = Result<CreateStockCommandResponse>.Create();

        var stock = request.Adapt<Stock>();
        stock.LastUpdated = DateTime.UtcNow;

        await _context.Stocks.AddAsync(stock, cancellationToken);
        await _context.SaveChangesAsync(cancellationToken);

        return response.Created(stock.Adapt<CreateStockCommandResponse>());
    }
}

