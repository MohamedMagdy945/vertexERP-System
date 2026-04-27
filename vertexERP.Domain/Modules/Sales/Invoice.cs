namespace VertexERP.Domain.Modules.Sales
{
    public class Invoice
    {
        public int SalesOrderId { get; set; }
        public int CustomerId { get; set; }
        public DateTime InvoiceDate { get; set; }
        public decimal TotalAmount { get; set; }
        public string Status { get; set; } = "Unpaid";
        public ICollection<Payment> Payments { get; set; } = new List<Payment>();
    }
}
