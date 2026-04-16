using vertexERP.Domain.Common;
using vertexERP.Domain.Common.ValueObjects;

namespace vertexERP.Domain.Modules.Inventory.Entities
{
    public class Stock : BaseEntity
    {
        public int ProductId { get; set; }
        public Product Product { get; set; } = null!;

        public int WarehouseId { get; set; }
        public Warehouse Warehouse { get; set; } = null!;

        public Quantity Quantity { get; set; } = default!;

        public ICollection<StockMovement> Movements { get; set; } = new List<StockMovement>();

    }
}
