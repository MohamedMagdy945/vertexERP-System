using MediatR;
using VertexERP.Shared.Results;

namespace VertexERP.Application.Modules.Inventory.Warehouses.Queries.GetWarehouseById;

public record GetWarehouseByIdCommand(
    int Id
) : IRequest<Result<GetWarehouseByIdCommandResponse>>;