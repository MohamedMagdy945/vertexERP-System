namespace VertexERP.Application.Modules.Catalog.Categories.Update;

public sealed record Response(
    Guid Id,
    string Name,
    string? Description,
    string? ImageUrl);