namespace VertexERP.Application.Modules.Inventory.Warehouses.Update;

public sealed record Request(
    string Name,
    string Code,
    string Location);