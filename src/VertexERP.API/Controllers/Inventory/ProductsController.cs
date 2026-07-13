using Microsoft.AspNetCore.Mvc;
using VertexERP.Application.Modules.Inventory.Products.Commands.AddProductImage;
using VertexERP.Application.Modules.Inventory.Products.Commands.CreateProduct;
using VertexERP.Application.Modules.Inventory.Products.Commands.DeleteProductById;
using VertexERP.Application.Modules.Inventory.Products.Commands.DeleteProductImage;
using VertexERP.Application.Modules.Inventory.Products.Commands.UpdateProduct;
using VertexERP.Application.Modules.Inventory.Products.Queries.GetProductById;
using VertexERP.Application.Modules.Inventory.Products.Queries.GetProducts;
using VertexERP.Shared.Pagination;
using VertexERP.Shared.Results;

namespace VertexERP.API.Controllers.Inventory;


[Tags("Inventory")]
public class ProductsController : AppControllerBase
{

    [HttpGet("{id}")]
    [ProducesResponseType(typeof(Result<GetProductByIdCommandResponse>), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetById(int id)
    {
        var response = await Mediator.Send(new GetProductByIdCommand(id));

        return ApiResponse(response);
    }

    [HttpGet]
    [ProducesResponseType(typeof(Result<Page<GetProductsQueryResponse>>), StatusCodes.Status200OK)]
    public async Task<IActionResult> Get([FromQuery] GetProductsQuery query)
    {
        var response = await Mediator.Send(query);

        return ApiResponse(response);
    }


    [HttpPost]
    [ProducesResponseType(typeof(Result<CreateProductCommandResponse>), StatusCodes.Status200OK)]
    public async Task<IActionResult> Create([FromForm] CreateProductCommand command)
    {
        var response = await Mediator.Send(command);

        return ApiResponse(response);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Update(int id, [FromForm] UpdateProductCommand command)
    {
        var result = await Mediator.Send(command with { Id = id });

        return ApiResponse(result);
    }

    [HttpDelete("{id}")]
    [ProducesResponseType(typeof(Result<DeleteProductByIdCommandResponse>), StatusCodes.Status200OK)]
    public async Task<IActionResult> Delete(int id)
    {
        var response = await Mediator.Send(new DeleteProductByIdCommand(id));

        return ApiResponse(response);
    }

    [HttpDelete("images/{imageId}")]
    public async Task<IActionResult> DeleteProductImage(int imageId)
    {
        var result = await Mediator.Send(new DeleteProductImageCommand(imageId));

        return ApiResponse(result);

    }

    [HttpPost("{productId}/images")]
    public async Task<IActionResult> AddProductImage(int productId, [FromForm] AddProductImageCommand command)
    {
        command = command with { ProductId = productId };

        var result = await Mediator.Send(command);

        return ApiResponse(result);
    }
}
