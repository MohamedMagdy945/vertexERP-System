using vertexERP.Domain.Common;

namespace vertexERP.Domain.Modules.Inventory
{
    public class Product : BaseEntity
    {
        public string Name { get; set; } = string.Empty;
        public string SKU { get; set; } = string.Empty;
        public int CategoryId { get; set; }
        public Category Category { get; set; } = default!;
        public bool IsAvailable { get; set; } = true;
        public ICollection<Stock> Stocks { get; set; } = new List<Stock>();
    }
}
