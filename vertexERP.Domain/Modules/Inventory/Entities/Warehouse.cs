using vertexERP.Domain.Common;

namespace vertexERP.Domain.Modules.Inventory.Entities
{
    public class Warehouse : BaseEntity
    {
        public string Name { get; set; } = string.Empty;
        public string Location { get; set; } = string.Empty;
        public ICollection<Stock> Stocks { get; set; } = new List<Stock>();

    }
}
