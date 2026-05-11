using Asp.Versioning;
using Microsoft.AspNetCore.Mvc;

namespace VertexERP.API.Controllers.V2;

[ApiController]
[ApiVersion("2.0")]
[Route("api/v{version:apiVersion}/[controller]")]
public class ProductsController : ControllerBase
{
    [HttpGet]
    public IActionResult GetAll()
    {
        return Ok(new
        {
            Version = "V2",
            Message = "Products from API Version 2"
        });
    }

    [HttpGet("{id}")]
    public IActionResult GetById(int id)
    {
        return Ok(new
        {
            Version = "V2",
            ProductId = id,
            Message = "Single product from API Version 2"
        });
    }

    [HttpPost]
    public IActionResult Create(ProductCreateDto dto)
    {
        return Ok(new
        {
            Version = "V2",
            Message = "Product created using V2 API",
            Data = dto
        });
    }
}

public class ProductCreateDto
{
    public string Name { get; set; } = default!;

    public decimal Price { get; set; }

    public string? Description { get; set; }
}