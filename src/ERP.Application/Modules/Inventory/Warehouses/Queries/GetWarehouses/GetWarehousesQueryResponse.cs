namespace VertexERP.Application.Modules.Inventory.Warehouses.Queries.GetWarehouses;

public record GetWarehousesQueryResponse
{
    public int Id { get; set; }
    public string Name { get; set; } = default!;
    public string Description { get; set; } = default!;
    public string ImageUrl { get; set; } = default!;
    public string Code { get; set; } = default!;
    public decimal SellingPrice { get; set; }
}