using vertexERP.Domain.Common;

namespace vertexERP.Domain.Modules.Sales
{
    public class SalesOrderItem : BaseEntity
    {
        public int SalesOrderId { get; set; }
        public SalesOrder SalesOrder { get; set; } = null!;
        public int ProductId { get; set; }
        public int Quantity { get; set; }
        public decimal UnitPrice { get; set; }
        public decimal Total => Quantity * UnitPrice;
    }
}
