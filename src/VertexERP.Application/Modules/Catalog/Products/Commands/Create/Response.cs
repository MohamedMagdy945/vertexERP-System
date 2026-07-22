namespace VertexERP.Application.Modules.Catalog.Products.Commands.Create;

public sealed record Response(Guid Id, string Name, string Code, string? Barcode,
    decimal CostPrice, decimal SellingPrice, Guid CategoryId, Guid UnitId, bool IsAvailable,
    IReadOnlyList<string> Images);