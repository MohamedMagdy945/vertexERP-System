namespace VertexERP.Application.Modules.Identity.Users.Queries.GetCategories;

public record GetCategoriesQueryResponse
{
    public int Id { get; init; }
    public string Name { get; init; } = default!;
    public string Description { get; init; } = default!;
}