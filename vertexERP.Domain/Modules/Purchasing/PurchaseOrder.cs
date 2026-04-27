using VertexERP.Domain.Common;

namespace VertexERP.Domain.Modules.Purchasing
{
    public class PurchaseOrder : BaseEntity
    {
        public int SupplierId { get; set; }
        public Supplier Supplier { get; set; } = null!;

        public DateTime OrderDate { get; set; }

        public string Status { get; set; } = "Draft";
        // Draft, Approved, Sent, Received, Cancelled

        public ICollection<PurchaseOrderItem> Items { get; set; } = new List<PurchaseOrderItem>();

    }
}
