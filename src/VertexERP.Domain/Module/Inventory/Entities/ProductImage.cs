namespace VertexERP.Domain.Module.Inventory.Entities;

public class ProductImage
{
    public int Id { get; set; }
    public string Url { get; set; } = default!;
    public string? AltText { get; set; }
    public bool IsPrimary { get; set; }
    public int ProductId { get; set; }
    public Product Product { get; set; } = default!;
}