namespace VertexERP.Application.Modules.Inventory.Warehouses.Queries.GetWarehouseById;


public record GetWarehouseByIdCommandResponse
{
    public int Id { get; set; }
    public string Name { get; set; } = default!;
    public string Description { get; set; } = default!;
    public string Code { get; set; } = default!;
}