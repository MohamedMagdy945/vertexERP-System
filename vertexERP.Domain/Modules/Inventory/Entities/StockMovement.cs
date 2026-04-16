using vertexERP.Domain.Common;
using vertexERP.Domain.Modules.Inventory.Enums;

namespace vertexERP.Domain.Modules.Inventory.Entities
{
    public class StockMovement : BaseEntity
    {

        public int StockId { get; set; }
        public Stock Stock { get; set; } = null!;

        public int QuantityChange { get; set; }

        public StockMovementType Type { get; set; }

        public string? Reference { get; set; }

    }
}
