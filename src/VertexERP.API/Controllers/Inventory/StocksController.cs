using Microsoft.AspNetCore.Mvc;
using VertexERP.Application.Modules.Inventory.Stocks.Commands.CreateStock;
using VertexERP.Application.Modules.Inventory.Stocks.Commands.DeleteStock;
using VertexERP.Shared.Results;

namespace VertexERP.API.Controllers.Inventory;


[Tags("Inventory")]
public class StocksController : AppControllerBase
{

    [HttpPost()]
    [ProducesResponseType(typeof(Result<CreateStockCommandResponse>), StatusCodes.Status200OK)]
    public async Task<IActionResult> Create(CreateStockCommand command)
    {
        var result = await Mediator.Send(command);

        return ApiResponse(result);
    }

    [HttpDelete]
    [ProducesResponseType(typeof(Result<DeleteStockCommandResponse>), StatusCodes.Status200OK)]
    public async Task<IActionResult> Delete([FromQuery] DeleteStockCommand command)
    {
        var result = await Mediator.Send(command);

        return ApiResponse(result);
    }

}

