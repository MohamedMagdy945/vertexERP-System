using MediatR;
using VertexERP.Application.Modules.Inventory.Products.Commands.DeleteProductById;
using VertexERP.Shared.Results;

namespace VertexERP.Application.Modules.Inventory.Warehouses.Commands.DeleteWarehourseById;

public record DeleteProductByIdCommand(
    int Id
) : IRequest<Result<DeleteProductByIdCommandResponse>>;