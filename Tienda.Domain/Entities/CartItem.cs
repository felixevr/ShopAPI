namespace Tienda.Domain.Entities
{
    public partial class CartItem : BaseEntity
    {
        public int CartID { get; set; }
        public int ProductID { get; set; }
        public int Quantity { get; set; }

        public Cart? Cart { get; set; }
        public Product? Product { get; set; }
    }

}
