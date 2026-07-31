namespace VertexERP.Application.Modules.Catalog.Products.Get;

public sealed class Response
{
    public Guid Id { get; init; }
    public string Name { get; init; } = string.Empty;
    public string Code { get; init; } = string.Empty;
    public string? Description { get; init; }
    public decimal SellingPrice { get; init; }
    public string UnitSymbol { get; init; } = string.Empty;
    public bool IsAvailable { get; init; }
    public IReadOnlyList<string> Images { get; init; } = [];
}