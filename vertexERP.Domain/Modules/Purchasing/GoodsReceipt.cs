namespace VertexERP.Domain.Modules.Purchasing
{
    public class GoodsReceipt
    {
        public int Id { get; set; }

        public int PurchaseOrderId { get; set; }

        public DateTime ReceivedDate { get; set; }

        public ICollection<GoodsReceiptItem> Items { get; set; } = new List<GoodsReceiptItem>();
    }
}
