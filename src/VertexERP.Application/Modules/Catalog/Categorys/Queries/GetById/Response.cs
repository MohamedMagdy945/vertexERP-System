namespace VertexERP.Application.Modules.Catalog.Categorys.Queries.GetById;

public sealed record Response(
    Guid Id,
    string Name,
    string? Description);