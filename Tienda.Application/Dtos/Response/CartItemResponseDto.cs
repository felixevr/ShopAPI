using Tienda.Domain.Entities;

namespace Tienda.Application.Dtos.Response
{
    public class CartItemResponseDto
    {
        public int CartItemId { get; set; }
        public int CartID { get; set; }
        public int ProductID { get; set; }
        public int Quantity { get; set; }
        public string? StateCartItem { get; set; }
        public int State {  get; set; }
        public DateTime AuditCreateDate { get; set; }

        //public CartResponseDto? Cart { get; set; }
        //public ProductResponseDto? Product { get; set; }
    }
}
