namespace Tienda.Domain.Entities
{
    public class InvoicePayment : BaseEntity
    {
        public int? InvoiceId { get; set; }

        public DateTime PaymentDate { get; set; }

        public decimal Amount { get; set; }

        public string PaymentMethod { get; set; } = null!;

        public virtual InvoiceHeader? Invoice { get; set; }
    }
}
