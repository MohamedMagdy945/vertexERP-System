using MediatR;
using VertexERP.Shared.Results;

namespace VertexERP.Application.Modules.Inventory.Warehouses.Commands.DeleteWarehouseById;

public record DeleteWarehouseByIdCommand(
    int Id
) : IRequest<Result<DeleteWarehouseByIdCommandResponse>>;