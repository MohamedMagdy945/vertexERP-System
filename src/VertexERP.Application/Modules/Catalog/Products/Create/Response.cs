namespace VertexERP.Application.Modules.Catalog.Products.Create;


public sealed record Image(Guid Id, string Url);
public sealed record Response(Guid Id, string Name, string Code, string? Barcode,
    decimal CostPrice, decimal SellingPrice, Guid CategoryId, Guid UnitId, bool IsAvailable,
     IReadOnlyList<Image> Images);