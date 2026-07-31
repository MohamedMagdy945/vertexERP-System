namespace VertexERP.Application.Modules.Inventory.Warehouses.GetById;

public sealed class Response
{
    public Guid Id { get; init; }
    public string Name { get; init; } = default!;
    public string? Description { get; init; }
}