using VertexERP.Domain.Common;

namespace VertexERP.Domain.Modules.Inventory.Entities
{
    public class Warehouse : BaseEntity
    {
        public string Name { get; set; } = string.Empty;
        public string Location { get; set; } = string.Empty;
        public ICollection<Stock> Stocks { get; set; } = new List<Stock>();

    }
}
