namespace Tienda.Domain.Entities;

public partial class InvoiceHeader : BaseEntity
{
    public int? UserId { get; set; }

    public DateTime Date { get; set; }

    public decimal Total { get; set; }

    public virtual User? User { get; set; }

    public virtual ICollection<InvoiceDetail> InvoiceDetails { get; set; } = new List<InvoiceDetail>();

    public virtual ICollection<InvoicePayment> InvoicePayments { get; set; } = new List<InvoicePayment>();

}
