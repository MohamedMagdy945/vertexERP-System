using MediatR;
using Microsoft.Extensions.Logging;
using VertexERP.Application.Abstractions.Persistence;
using VertexERP.Application.Abstractions.Storage;
using VertexERP.Shared.Results;

namespace VertexERP.Application.Modules.Inventory.Products.Commands.DeleteProductImage;


public class DeleteProductImageCommandHandler
    : IRequestHandler<DeleteProductImageCommand, Result<DeleteProductImageCommandResponse>>
{
    private readonly IApplicationDbContext _context;
    private readonly ILogger<DeleteProductImageCommandHandler> _logger;
    private readonly IFileStorage _fileStorage;
    public DeleteProductImageCommandHandler(
        IApplicationDbContext dbContext,
        ILogger<DeleteProductImageCommandHandler> logger,
        IFileStorage fileStorage)
    {
        _context = dbContext;
        _logger = logger;
        _fileStorage = fileStorage;
    }

    public async Task<Result<DeleteProductImageCommandResponse>> Handle(DeleteProductImageCommand request, CancellationToken cancellationToken)
    {
        var result = Result<DeleteProductImageCommandResponse>.Create();

        var image = await _context.ProductImages.FindAsync(request.ImageId, cancellationToken);

        if (image == null)
            return result.NotFound();

        await _fileStorage.DeleteAsync(image.Url);

        _context.ProductImages.Remove(image);
        await _context.SaveChangesAsync(cancellationToken);

        return result.Success(new DeleteProductImageCommandResponse { ImageId = image.Id, });

    }
}

