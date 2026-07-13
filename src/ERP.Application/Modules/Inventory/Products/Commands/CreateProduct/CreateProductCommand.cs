using MediatR;
using Microsoft.AspNetCore.Http;
using VertexERP.Shared.Results;

namespace VertexERP.Application.Modules.Inventory.Products.Commands.CreateProduct;

public record CreateProductCommand(
    string Name,
    IReadOnlyList<IFormFile>? Images,
    string? Barcode,
    string? Description,
    string Code,
    decimal CostPrice,
    decimal SellingPrice,
    int Unit,
    int CategoryId
) : IRequest<Result<CreateProductCommandResponse>>;