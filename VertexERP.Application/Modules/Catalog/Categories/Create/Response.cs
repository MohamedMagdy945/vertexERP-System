namespace VertexERP.Application.Modules.Catalog.Categories.Create;

public sealed record Response(Guid Id,
    string Name,
    string? Description);