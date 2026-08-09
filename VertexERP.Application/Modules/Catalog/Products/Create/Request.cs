using Microsoft.AspNetCore.Http;

namespace VertexERP.Application.Modules.Catalog.Products.Create;

public sealed record Request(
        string Name,
        string Code,
        decimal CostPrice,
        decimal SellingPrice,
        Guid CategoryId,
        Guid UnitId,
        string? Barcode,
        string? Description,
        IReadOnlyList<IFormFile>? Images);
