using MediatR;
using VertexERP.Shared.Results;

namespace VertexERP.Application.Modules.Inventory.Products.Commands.DeleteProductImage;

public record DeleteProductImageCommand(
    int ImageId
) : IRequest<Result<DeleteProductImageCommandResponse>>;