using MediatR;
using VertexERP.Shared.Results;

namespace VertexERP.Application.Modules.Inventory.Products.Commands.DeleteProductById;

public record DeleteProductByIdCommand(
    int Id
) : IRequest<Result<DeleteProductByIdCommandResponse>>;