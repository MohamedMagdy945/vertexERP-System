using MediatR;
using VertexERP.Shared.Results;

namespace VertexERP.Application.Modules.Inventory.Warehouses.Commands.UpdateWarehouse;

public record UpdateWarehouseCommand(
    int Id,
    string Name,
    string Code,
    string Location
) : IRequest<Result<UpdateWarehouseCommandResponse>>;