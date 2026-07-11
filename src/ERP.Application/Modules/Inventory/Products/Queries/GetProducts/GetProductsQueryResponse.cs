namespace VertexERP.Application.Modules.Inventory.Products.Queries.GetProducts;

public record GetProductsQueryResponse
{
    public int Id { get; init; }
    public string Name { get; init; } = default!;
    public string Description { get; init; } = default!;
}