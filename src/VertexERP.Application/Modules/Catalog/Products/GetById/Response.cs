namespace VertexERP.Application.Modules.Catalog.Products.GetById;

public sealed record ImageResponse(Guid Id, string Url);
public sealed class Response
{
    public Guid Id { get; init; }
    public string Name { get; init; } = string.Empty;
    public string Code { get; init; } = string.Empty;
    public string? Description { get; init; }
    public decimal SellingPrice { get; init; }
    public string CategoryName { get; init; } = string.Empty;
    public string UnitName { get; init; } = string.Empty;
    public bool IsAvailable { get; init; }
    public IReadOnlyList<ImageResponse> Images { get; init; } = [];
}