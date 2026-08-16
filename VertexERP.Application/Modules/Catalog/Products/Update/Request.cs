namespace VertexERP.Application.Modules.Catalog.Products.Update;

public sealed record Request(
    string Name,
    string Code,
    decimal CostPrice,
    decimal SellingPrice,
    Guid CategoryId,
    Guid UnitId,
    string Barcode,
    string Description);