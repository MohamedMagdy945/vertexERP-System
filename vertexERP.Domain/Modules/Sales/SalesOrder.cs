using vertexERP.Domain.Common;

namespace vertexERP.Domain.Modules.Sales
{
    public class SalesOrder : BaseEntity
    {
        public int CustomerId { get; set; }
        public Customer Customer { get; set; } = default!;

        public DateTime OrderDate { get; set; }

        public SalesOrderStatus status { get; set; }
        public ICollection<SalesOrderItem> items { get; set; } = new List<SalesOrderItem>();
    }
}
