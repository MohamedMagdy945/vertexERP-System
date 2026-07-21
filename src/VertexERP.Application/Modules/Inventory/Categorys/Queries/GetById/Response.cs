namespace VertexERP.Application.Modules.Inventory.Categorys.Queries.GetById;

public sealed record Response(
    Guid Id,
    string Name,
    string? Description);