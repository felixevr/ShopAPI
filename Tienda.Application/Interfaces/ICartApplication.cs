using Tienda.Application.Commons.Bases;
using Tienda.Application.Dtos.Request;
using Tienda.Application.Dtos.Response;
using Tienda.Infrastructure.Commons.Bases.Response;

namespace Tienda.Application.Interfaces
{
    public interface ICartApplication
    {
        Task<BaseResponse<BaseEntityResponse<CartResponseDto>>> ListCartsWithProducts(int userId);
        Task<BaseResponse<bool>> RegisterCart(CartRequestDto requestDto);
        Task<BaseResponse<CartResponseDto>> GetCartById(int cardId);
        Task<BaseResponse<BaseEntityResponse<CartResponseDto>>> GetCartByIdWithProducts(int cartId);
    }
}
