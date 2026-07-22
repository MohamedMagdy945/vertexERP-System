namespace VertexERP.Application.Modules.Catalog.Categories.Queries.Get;

public sealed record Response(
    Guid Id,
    string Name,
    string? Description);