namespace Tienda.Domain.Entities
{
    public partial class InvoiceDetail : BaseEntity
    {
        public int InvoiceDetailId { get; set; }

        public int? InvoiceId { get; set; }

        public int? ProductId { get; set; }

        public int Quantity { get; set; }

        public decimal UnitPrice { get; set; }

        public decimal? Subtotal { get; set; }     

        public virtual InvoiceHeader? Invoice { get; set; }

        public virtual Product? Product { get; set; }
    }
}
