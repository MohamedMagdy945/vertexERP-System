using Microsoft.AspNetCore.Mvc;
using VertexERP.Application.Modules.Inventory.Products.Commands.CreateProduct;
using VertexERP.Application.Modules.Inventory.Products.Commands.DeleteProductById;
using VertexERP.Application.Modules.Inventory.Products.Commands.UpdateProduct;
using VertexERP.Application.Modules.Inventory.Products.Queries.GetProductById;
using VertexERP.Application.Modules.Inventory.Products.Queries.GetProducts;
using VertexERP.Shared.Pagination;
using VertexERP.Shared.Results;

namespace VertexERP.API.Controllers.Inventory;


[Tags("Inventory")]
public class ProductsController : AppControllerBase
{
    [HttpGet("GetProductById/{id}")]
    [ProducesResponseType(typeof(Result<GetProductByIdCommandResponse>), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetProductById(int id)
    {
        var response = await Mediator.Send(new GetProductByIdCommand(id));

        return ApiResponse(response);
    }

    [HttpGet("GetProducts")]
    [ProducesResponseType(typeof(Result<Page<GetProductsQueryResponse>>), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetProducts([FromQuery] GetProductsQuery query)
    {
        var response = await Mediator.Send(query);

        return ApiResponse(response);
    }

    [HttpPost("CreateProduct")]
    [ProducesResponseType(typeof(Result<CreateProductCommandResponse>), StatusCodes.Status200OK)]
    public async Task<IActionResult> CreateProduct([FromForm] CreateProductCommand command)
    {
        var response = await Mediator.Send(command);

        return ApiResponse(response);
    }
    [HttpPost("UpdateProduct")]
    [ProducesResponseType(typeof(Result<UpdateProductCommandResponse>), StatusCodes.Status200OK)]
    public async Task<IActionResult> UpdateProduct(UpdateProductCommand command)
    {
        var response = await Mediator.Send(command);

        return ApiResponse(response);
    }

    [HttpPost("DeleteProductById/{id}")]
    [ProducesResponseType(typeof(Result<DeleteProductByIdCommandResponse>), StatusCodes.Status200OK)]
    public async Task<IActionResult> DeleteProductById(int id)
    {
        var response = await Mediator.Send(new DeleteProductByIdCommand(id));

        return ApiResponse(response);
    }


}
