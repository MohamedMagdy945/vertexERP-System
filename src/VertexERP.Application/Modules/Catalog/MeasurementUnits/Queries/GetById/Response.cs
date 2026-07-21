namespace VertexERP.Application.Modules.Catalog.Units.Queries.GetById;

public sealed record Response(
    Guid Id,
    string Name,
    string? Description);