namespace VertexERP.Application.Modules.Inventory.Warehouses.Update;

public sealed record Response(
    Guid Id,
    string Name,
    string? Description,
    string? ImageUrl);