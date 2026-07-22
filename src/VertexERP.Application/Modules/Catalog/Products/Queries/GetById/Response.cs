namespace VertexERP.Application.Modules.Catalog.Products.Queries.GetById;

public sealed record Response(Guid Id, string Name, string Code, string? Barcode, string? Description,
    decimal CostPrice, decimal SellingPrice, Guid CategoryId, Guid UnitId, bool IsAvailable,
    IReadOnlyList<string> Images);