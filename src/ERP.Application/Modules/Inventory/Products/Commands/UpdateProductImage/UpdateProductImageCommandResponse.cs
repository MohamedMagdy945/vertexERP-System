namespace VertexERP.Application.Modules.Inventory.Products.Commands.UpdateProductImage;

public record UpdateProductImageCommandResponse
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Code { get; set; } = string.Empty;
    public string? ImageUrl { get; set; }
    public decimal SellingPrice { get; set; }
}