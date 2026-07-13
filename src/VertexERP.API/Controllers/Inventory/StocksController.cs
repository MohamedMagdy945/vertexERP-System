using Microsoft.AspNetCore.Mvc;
using VertexERP.Application.Modules.Inventory.Stocks.Commands.CreateStock;
using VertexERP.Shared.Results;

namespace VertexERP.API.Controllers.Inventory;


[Tags("Inventory")]
public class StocksController : AppControllerBase
{

    [HttpPost("CreateStock")]
    [ProducesResponseType(typeof(Result<CreateStockCommandResponse>), StatusCodes.Status200OK)]
    public async Task<IActionResult> CreateWarehouse(CreateStockCommand command)
    {
        var result = await Mediator.Send(command);

        return ApiResponse(result);
    }

}

