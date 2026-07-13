using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using VertexERP.Application.Abstractions.Persistence;
using VertexERP.Application.Abstractions.Storage;
using VertexERP.Domain.Module.Inventory.Entities;
using VertexERP.Shared.Results;

namespace VertexERP.Application.Modules.Inventory.Products.Commands.AddProductImage;


public class AddProductImageCommandHandler
    : IRequestHandler<AddProductImageCommand, Result<AddProductImageCommandResponse>>
{
    private readonly IApplicationDbContext _context;
    private readonly ILogger<AddProductImageCommandHandler> _logger;
    private readonly IFileStorage _fileStorage;
    public AddProductImageCommandHandler(
        IApplicationDbContext dbContext,
        ILogger<AddProductImageCommandHandler> logger,
        IFileStorage fileStorage)
    {
        _context = dbContext;
        _logger = logger;
        _fileStorage = fileStorage;
    }

    public async Task<Result<AddProductImageCommandResponse>> Handle(AddProductImageCommand request, CancellationToken cancellationToken)
    {
        var result = Result<AddProductImageCommandResponse>.Create();

        var product = await _context.Products
        .Where(x => x.Id == request.ProductId)
        .Select(x => new
        {
            x.Code,
            ImagesCount = x.Images.Count
        })
        .FirstOrDefaultAsync(cancellationToken);

        if (product is null)
            return result.NotFound();

        if (product.ImagesCount >= 6)
            return result.Failure("A product cannot have more than 6 images.");

        var directory = $"products/{product.Code}";

        var imageUrl = await _fileStorage.UploadAsync(request.Image, directory, cancellationToken);

        var newProductImage = new ProductImage
        {
            ProductId = request.ProductId,
            Url = imageUrl
        };


        await _context.ProductImages.AddAsync(newProductImage, cancellationToken);
        await _context.SaveChangesAsync(cancellationToken);

        var response = new AddProductImageCommandResponse
        {
            ImageId = newProductImage.Id,
            ImageUrl = imageUrl
        };

        return result.Success(response);

    }
}

