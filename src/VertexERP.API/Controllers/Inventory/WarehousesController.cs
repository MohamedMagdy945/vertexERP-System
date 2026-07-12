using Microsoft.AspNetCore.Mvc;
using VertexERP.Application.Modules.Inventory.Warehouses.Commands.CreateWarehouse;
using VertexERP.Application.Modules.Inventory.Warehouses.Queries.GetWarehouses;
using VertexERP.Shared.Pagination;
using VertexERP.Shared.Results;

namespace VertexERP.API.Controllers.Inventory;


[Tags("Inventory")]
public class WarehousesController : AppControllerBase
{
    //[HttpGet("GetWarehouseById/{id}")]
    //[ProducesResponseType(typeof(Result<DeleteProductByIdCommandRe sponse>), StatusCodes.Status200OK)]
    //public async Task<IActionResult> GetWarehouseById(int id)
    //{
    //    var response = await Mediator.Send(new GetWarehouseByIdCommand(id));

    //    return ApiResponse(response);
    //}

    [HttpGet("GetWarehouses")]
    [ProducesResponseType(typeof(Result<PagedResult<GetWarehousesQueryResponse>>), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetWarehouses([FromQuery] GetWarehousesQuery query)
    {
        var response = await Mediator.Send(query);

        return ApiResponse(response);
    }

    [HttpPost("CreateWarehouse")]
    [ProducesResponseType(typeof(Result<CreateWarehouseCommandResponse>), StatusCodes.Status200OK)]
    public async Task<IActionResult> CreateWarehouse(CreateWarehouseCommand command)
    {
        var response = await Mediator.Send(command);

        return ApiResponse(response);
    }

    //[HttpPost("UpdateWarehouse")]
    //[ProducesResponseType(typeof(Result<UpdateWarehouseCommandResponse>), StatusCodes.Status200OK)]
    //public async Task<IActionResult> UpdateWarehouse([FromForm] UpdateWarehouseCommand command)
    //{
    //    var response = await Mediator.Send(command);

    //    return ApiResponse(response);
    //}

    //[HttpPost("DeleteWarehouseById/{id}")]
    //[ProducesResponseType(typeof(Result<DeleteWarehouseByIdCommandResponse>), StatusCodes.Status200OK)]
    //public async Task<IActionResult> DeleteWarehouseById(int id)
    //{
    //    var response = await Mediator.Send(new DeleteWarehouseByIdCommand(id));

    //    return ApiResponse(response);
}

