namespace VertexERP.Application.Modules.Catalog.Products.Update;


public sealed class Response
{
    public Guid Id { get; init; }
    public string Name { get; init; } = default!;
    public string Code { get; init; } = default!;
    public string? Barcode { get; init; }
    public decimal CostPrice { get; init; }
    public decimal SellingPrice { get; init; }
    public Guid CategoryId { get; init; }
    public Guid UnitId { get; init; }
    public bool IsAvailable { get; init; }
}