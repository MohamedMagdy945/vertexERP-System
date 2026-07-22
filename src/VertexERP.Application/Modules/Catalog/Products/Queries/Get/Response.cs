namespace VertexERP.Application.Modules.Catalog.Products.Queries.Get;

public sealed record Response(Guid Id, string Name, string Code, string? Barcode, string? Description,
    decimal CostPrice, decimal SellingPrice, Guid CategoryId, Guid UnitId, bool IsAvailable,
    IReadOnlyList<string> Images);