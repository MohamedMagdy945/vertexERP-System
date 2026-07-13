namespace VertexERP.Application.Modules.Inventory.Products.Commands.AddProductImage;

public record AddProductImageCommandResponse
{
    public int ImageId { get; set; }
    public string ImageUrl { get; set; } = string.Empty;
}