using MediatR;
using VertexERP.Shared.Results;

namespace VertexERP.Application.Modules.Inventory.Stocks.Commands.DeleteStock;

public record DeleteStockCommand(int ProductId, int WarehouseId) :
    IRequest<Result<DeleteStockCommandResponse>>;

