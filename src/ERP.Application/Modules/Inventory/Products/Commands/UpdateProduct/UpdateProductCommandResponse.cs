namespace VertexERP.Application.Modules.Inventory.Products.Commands.UpdateProduct;

public record UpdateProductCommandResponse
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Code { get; set; } = string.Empty;
    public string? ImageUrl { get; set; }
}