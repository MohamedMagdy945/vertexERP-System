using VertexERP.Domain.Common;

namespace VertexERP.Domain.Module.Inventory.Entities;

public class Warehouse : BaseEntity
{
    public string Name { get; set; } = string.Empty;
    public string Code { get; set; } = string.Empty;
    public string Location { get; set; } = string.Empty;
    public ICollection<Stock> Stocks { get; set; } = new List<Stock>();

}

