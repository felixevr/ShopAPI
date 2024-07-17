namespace Tienda.Domain.Entities;

public partial class Product : BaseEntity
{
    public string Name { get; set; } = null!;

    public string Description { get; set; } = null!;

    public decimal Price { get; set; } = 0;

    public int? CategoryId { get; set; }

    public virtual ICollection<CartItem> CartItems { get; set; } = new List<CartItem>();

    public virtual Category? Category { get; set; }
    
    public string? ImageUrl { get; set; }

    public virtual ICollection<InvoiceDetail> InvoiceDetails { get; set; } = new List<InvoiceDetail>();

}
