using vertexERP.Domain.Common;

namespace vertexERP.Domain.Modules.Inventory
{
    public class Stock : BaseEntity
    {
        public int ProductId { get; set; }
        public Product Product { get; set; } = null!;

        public int WarehouseId { get; set; }
        public Warehouse Warehouse { get; set; } = null!;

        public int Quantity { get; set; }

        public ICollection<StockMovement> Movements { get; set; } = new List<StockMovement>();

    }
}
