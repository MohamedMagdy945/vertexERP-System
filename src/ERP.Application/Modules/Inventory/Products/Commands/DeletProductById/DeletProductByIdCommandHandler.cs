using MediatR;
using Microsoft.Extensions.Logging;
using VertexERP.Application.Abstractions.Persistence;
using VertexERP.Application.Abstractions.Storage;
using VertexERP.Shared.Results;

namespace VertexERP.Application.Modules.Inventory.Products.Commands.DeletProduct;


public class DeletProductCommandHandler
    : IRequestHandler<DeletProductCommand, Result<DeletProductCommandResponse>>
{
    private readonly IApplicationDbContext _dbContext;
    private readonly ILogger<DeletProductCommandHandler> _logger;
    private readonly IFileStorage _fileStorage;
    public DeletProductCommandHandler(
        IApplicationDbContext dbContext,
        ILogger<DeletProductCommandHandler> logger,
        IFileStorage fileStorage)
    {
        _dbContext = dbContext;
        _logger = logger;
        _fileStorage = fileStorage;
    }

    public async Task<Result<DeletProductCommandResponse>> Handle(DeletProductCommand request, CancellationToken cancellationToken)
    {
        var product = await _dbContext.Products.FindAsync(request.Id, cancellationToken);
        if (product == null)
        {
            return Result<DeletProductCommandResponse>.NotFound($"Product with Id {request.Id} not found.");
        }

        _dbContext.Products.Remove(product);
        await _dbContext.SaveChangesAsync(cancellationToken);

        return Result<DeletProductCommandResponse>.Success(new DeletProductCommandResponse
        {
            Id = product.Id,
        }, "Product deleted successfully.");

    }
}

