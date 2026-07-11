using Microsoft.AspNetCore.Mvc;
using VertexERP.Application.Modules.Inventory.Products.Commands.CreateProduct;
using VertexERP.Shared.Results;

namespace VertexERP.API.Controllers.Inventory;


[Tags("Inventory")]
public class ProductsController : AppControllerBase
{
    [HttpPost("CreateProduct")]
    [ProducesResponseType(typeof(Result<CreateProductCommandResponse>), StatusCodes.Status200OK)]
    public async Task<IActionResult> CreateProduct([FromForm] CreateProductCommand command)
    {
        var response = await Mediator.Send(command);

        return ApiResponse(response);
    }

    //[HttpGet("GetProducts")]
    //[ProducesResponseType(typeof(Result<PagedResult<GetProductsQueryResponse>>), StatusCodes.Status200OK)]
    //public async Task<IActionResult> GetProducts([FromQuery] GetProductsQuery query)
    //{
    //    var response = await Mediator.Send(query);

    //    return ApiResponse(response);
    //}
}
