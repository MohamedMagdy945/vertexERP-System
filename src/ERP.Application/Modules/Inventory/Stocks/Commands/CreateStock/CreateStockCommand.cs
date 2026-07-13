using MediatR;
using VertexERP.Shared.Results;

namespace VertexERP.Application.Modules.Inventory.Stocks.Commands.CreateStock;

public record CreateStockCommand(
    int ProductId,
    int WarehouseId,
    int Quantity
) : IRequest<Result<CreateStockCommandResponse>>;

