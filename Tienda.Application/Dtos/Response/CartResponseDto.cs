namespace Tienda.Application.Dtos.Response
{
    public class CartResponseDto
    {
        public int CartId { get; set; }
        public DateTime AuditCreateDate { get; set; }
        public int State { get; set; }
        public string? StateCart { get; set; }
        public int UserId { get; set; }

        // Cre que de alguna manera aquí debo mapear el User (ya que lo tengo definido en el Cart)
        public List<CartItemResponseDto> CartItems { get; set; } = new List<CartItemResponseDto>(); // agregado para lograr traer los productos
    }
}
