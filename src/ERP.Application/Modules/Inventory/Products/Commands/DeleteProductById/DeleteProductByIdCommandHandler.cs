using MediatR;
using Microsoft.Extensions.Logging;
using VertexERP.Application.Abstractions.Persistence;
using VertexERP.Application.Abstractions.Storage;
using VertexERP.Shared.Results;

namespace VertexERP.Application.Modules.Inventory.Products.Commands.DeleteProductById;


public class DeleteProductByIdCommandHandler
    : IRequestHandler<DeleteProductByIdCommand, Result<DeleteProductByIdCommandResponse>>
{
    private readonly IApplicationDbContext _dbContext;
    private readonly ILogger<DeleteProductByIdCommandHandler> _logger;
    private readonly IFileStorage _fileStorage;
    public DeleteProductByIdCommandHandler(
        IApplicationDbContext dbContext,
        ILogger<DeleteProductByIdCommandHandler> logger,
        IFileStorage fileStorage)
    {
        _dbContext = dbContext;
        _logger = logger;
        _fileStorage = fileStorage;
    }

    public async Task<Result<DeleteProductByIdCommandResponse>> Handle(DeleteProductByIdCommand request, CancellationToken cancellationToken)
    {
        var product = await _dbContext.Products.FindAsync(request.Id, cancellationToken);
        if (product == null)
        {
            return Result<DeleteProductByIdCommandResponse>.NotFound($"Product with Id {request.Id} not found.");
        }

        _dbContext.Products.Remove(product);
        await _dbContext.SaveChangesAsync(cancellationToken);

        return Result<DeleteProductByIdCommandResponse>.Success(new DeleteProductByIdCommandResponse
        {
            Id = product.Id,
        }, "Product deleted successfully.");

    }
}

