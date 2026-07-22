namespace VertexERP.Application.Modules.Catalog.Categories.Queries.GetById;

public sealed record Response(
    Guid Id,
    string Name,
    string? Description);