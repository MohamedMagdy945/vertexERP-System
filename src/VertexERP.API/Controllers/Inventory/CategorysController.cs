using Microsoft.AspNetCore.Mvc;
using VertexERP.Application.Modules.Identity.Users.Queries.GetCategories;
using VertexERP.Application.Modules.Inventory.Categories.Commands.CreateCategory;
using VertexERP.Shared.Pagination;
using VertexERP.Shared.Results;

namespace VertexERP.API.Controllers.Inventory;


[Tags("Inventory")]
public class CategorysController : AppControllerBase
{
    [HttpPost("CreateCategory")]
    [ProducesResponseType(typeof(Result<CreateCategoryCommandResponse>), StatusCodes.Status200OK)]
    public async Task<IActionResult> CreateCategory(CreateCategoryCommand command)
    {
        var response = await Mediator.Send(command);

        return ApiResponse(response);
    }

    [HttpGet("GetCategories")]
    [ProducesResponseType(typeof(Result<Page<GetCategoriesQueryResponse>>), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetCategories([FromQuery] GetCategoriesQuery query)
    {
        var response = await Mediator.Send(query);

        return ApiResponse(response);
    }
}
