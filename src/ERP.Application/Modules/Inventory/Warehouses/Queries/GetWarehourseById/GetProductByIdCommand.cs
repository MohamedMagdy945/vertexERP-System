using MediatR;
using VertexERP.Application.Modules.Inventory.Products.Queries.GetProductById;
using VertexERP.Shared.Results;

namespace VertexERP.Application.Modules.Inventory.Warehouses.Queries.GetWarehourseById;

public record GetProductByIdCommand(
    int Id
) : IRequest<Result<GetProductByIdCommandResponse>>;