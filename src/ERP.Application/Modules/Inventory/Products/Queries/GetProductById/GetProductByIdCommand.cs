using MediatR;
using VertexERP.Shared.Results;

namespace VertexERP.Application.Modules.Inventory.Products.Queries.GetProductById;

public record GetProductByIdCommand(
    int Id
) : IRequest<Result<GetProductByIdCommandResponse>>;