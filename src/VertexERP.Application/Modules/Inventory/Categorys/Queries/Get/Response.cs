namespace VertexERP.Application.Modules.Inventory.Categorys.Queries.Get;

public sealed record Response(
    Guid Id,
    string Name,
    string? Description);