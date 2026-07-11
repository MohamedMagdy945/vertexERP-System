namespace VertexERP.Application.Modules.Inventory.Categories.Commands.CreateCategory;

public record CreateCategoryCommandResponse
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
}