namespace VertexERP.Application.Modules.Inventory.Warehouses.Commands.UpdateWarehouse;

public record UpdateWarehouseCommandResponse
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Code { get; set; } = string.Empty;
    public string Location { get; set; } = string.Empty;
}