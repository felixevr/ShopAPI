namespace Tienda.Domain.Entities;

public partial class User : BaseEntity
{
    public string Name { get; set; } = null!;

    public string Email { get; set; } = null!;

    public string Password { get; set; } = null!;

    // Un usuario tiene un carrito (no aceptada sugerencia) - 3 Doritos después si tuve que hacerlo por lista
    public virtual ICollection<Cart> Carts { get; set; } = new List<Cart>(); // Me queda la espina aquí de no poder hacer una relación 1 a 1

    public virtual ICollection<InvoiceHeader> InvoiceHeaders { get; set; } = new List<InvoiceHeader>();
}
