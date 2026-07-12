using MediatR;
using VertexERP.Shared.Results;

namespace VertexERP.Application.Modules.Inventory.Products.Commands.DeletProduct;

public record DeletProductCommand(
    int Id
) : IRequest<Result<DeletProductCommandResponse>>;