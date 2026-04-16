using vertexERP.Domain.Common;
using vertexERP.Domain.Modules.Inventory.ValueObjects;

namespace vertexERP.Domain.Modules.Inventory.Entities
{
    public class Product : BaseEntity
    {
        public string Name { get; set; } = string.Empty;
        public SKU SKU { get; set; } = default!;
        public int CategoryId { get; set; }
        public Category Category { get; set; } = default!;
        public bool IsAvailable { get; set; } = true;
        public ICollection<Stock> Stocks { get; set; } = new List<Stock>();
    }
}
