namespace VertexERP.Application.Modules.Inventory.Warehouses.Create;

public sealed record Request(
    string Name,
    string Code,
    string Location);