using VertexERP.Domain.Common;

namespace VertexERP.Domain.Modules.Sales
{
    public class Payment : BaseEntity
    {
        public int Id { get; set; }
        public int InvoiceId { get; set; }
        public Invoice Invoice { get; set; } = null!;

        public decimal Amount { get; set; }

        public DateTime PaymentDate { get; set; }

        public string Method { get; set; } = string.Empty;
    }
}
