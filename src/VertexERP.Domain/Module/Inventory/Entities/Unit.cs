using VertexERP.Domain.Common;

namespace VertexERP.Domain.Module.Inventory.Entities;

public class Unit : BaseEntity
{
    public string Name { get; set; } = string.Empty;
    public string Symbol { get; set; } = string.Empty;
}

