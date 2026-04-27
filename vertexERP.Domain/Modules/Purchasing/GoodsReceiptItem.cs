namespace VertexERP.Domain.Modules.Purchasing
{
    public class GoodsReceiptItem
    {
        public int Id { get; set; }

        public int GoodsReceiptId { get; set; }

        public int ProductId { get; set; }

        public int QuantityReceived { get; set; }
    }
}
