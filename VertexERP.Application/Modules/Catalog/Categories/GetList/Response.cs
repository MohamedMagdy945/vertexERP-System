namespace VertexERP.Application.Modules.Catalog.Categories.GetList;

public sealed class Response
{
    public Guid Id { get; init; }
    public string Name { get; init; } = default!;
    public string? Description { get; init; }
    public string? ImageUrl { get;  set; }
}