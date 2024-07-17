namespace Tienda.Domain.Entities
{
    public partial class Cart : BaseEntity
    {
        public int CartID { get; set; }
        public int? UserID { get; set; }

        public User? User { get; set; }
        public virtual ICollection<CartItem> CartItems { get; set; } = new List<CartItem>(); // Relación con CartItems
    }
}