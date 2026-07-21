namespace VertexERP.Application.Modules.Catalog.Categorys.Queries.Get;

public sealed record Response(
    Guid Id,
    string Name,
    string? Description);