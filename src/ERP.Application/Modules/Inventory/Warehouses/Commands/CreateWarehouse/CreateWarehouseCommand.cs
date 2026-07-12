using MediatR;
using VertexERP.Shared.Results;

namespace VertexERP.Application.Modules.Inventory.Warehouses.Commands.CreateWarehouse;

public record CreateWarehouseCommand(
    string Name,
    string Code,
    string Location
) : IRequest<Result<CreateWarehouseCommandResponse>>;