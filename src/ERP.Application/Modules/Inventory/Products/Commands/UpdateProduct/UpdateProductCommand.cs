using MediatR;
using Microsoft.AspNetCore.Http;
using VertexERP.Domain.Module.Inventory.Enums;
using VertexERP.Shared.Results;

namespace VertexERP.Application.Modules.Inventory.Products.Commands.UpdateProduct;

public record UpdateProductCommand(
    int Id,
    string Name,
    IFormFile? Image,
    string? Barcode,
    string? Description,
    string? Code,
    decimal? CostPrice,
    decimal? SellingPrice,
    int CategoryId,
    UnitType Unit
) : IRequest<Result<UpdateProductCommandResponse>>;