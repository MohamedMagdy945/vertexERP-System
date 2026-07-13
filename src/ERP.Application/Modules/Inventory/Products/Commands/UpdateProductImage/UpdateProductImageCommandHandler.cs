using Mapster;
using MediatR;
using Microsoft.Extensions.Logging;
using VertexERP.Application.Abstractions.Persistence;
using VertexERP.Application.Abstractions.Storage;
using VertexERP.Shared.Results;

namespace VertexERP.Application.Modules.Inventory.Products.Commands.UpdateProductImage;


public class UpdateProductImageCommandHandler
    : IRequestHandler<UpdateProductImageCommand, Result<UpdateProductImageCommandResponse>>
{
    private readonly IApplicationDbContext _dbContext;
    private readonly ILogger<UpdateProductImageCommandHandler> _logger;
    private readonly IFileStorage _fileStorage;
    public UpdateProductImageCommandHandler(
        IApplicationDbContext dbContext,
        ILogger<UpdateProductImageCommandHandler> logger,
        IFileStorage fileStorage)
    {
        _dbContext = dbContext;
        _logger = logger;
        _fileStorage = fileStorage;
    }

    public async Task<Result<UpdateProductImageCommandResponse>> Handle(UpdateProductImageCommand request, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();

        var response = Result<UpdateProductImageCommandResponse>.Create();

        var product = await _dbContext.Products.FindAsync(new object[] { request.Id }, cancellationToken);

        if (product == null)
            return response.NotFound();

        //if (request.Image is not null && request.Image.Length > 0)
        //{
        //    if (!string.IsNullOrEmpty(product.ImageUrl))
        //    {
        //        await _fileStorage.DeleteAsync(product.ImageUrl, cancellationToken);
        //    }

        //    using var stream = request.Image.OpenReadStream();

        //    var newImageUrl = await _fileStorage.UploadAsync(stream, request.Image.FileName
        //        , request.Image.ContentType, "products", cancellationToken);

        //    if (string.IsNullOrEmpty(newImageUrl))
        //        return response.Failure("Image upload failed");

        //    product.ImageUrl = newImageUrl;
        //}

        var rowsAffected = await _dbContext.SaveChangesAsync(cancellationToken);

        if (rowsAffected == 0)
            return response.Failure("No changes were saved to the database.");

        return response.Updated(product.Adapt<UpdateProductImageCommandResponse>());
    }
}

