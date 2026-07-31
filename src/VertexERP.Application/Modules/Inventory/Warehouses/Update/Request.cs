namespace VertexERP.Application.Modules.Inventory.Warehouses.Update;

public sealed record Request(
    Guid Id,
    string Name,
    string Code,
    string Location);