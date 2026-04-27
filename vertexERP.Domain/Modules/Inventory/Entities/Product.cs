using VertexERP.Domain.Common;
using VertexERP.Domain.Modules.Inventory.ValueObjects;

namespace VertexERP.Domain.Modules.Inventory.Entities
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
