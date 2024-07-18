using Tienda.Application.Commons.Bases;
using Tienda.Application.Dtos.Response;
using Tienda.Infrastructure.Commons.Bases.Request;
using Tienda.Infrastructure.Commons.Bases.Response;

namespace Tienda.Application.Interfaces
{
    public interface ICartApplication
    {
        Task<BaseResponse<BaseEntityResponse<CartResponseDto>>> ListCartsWithProducts();

    }
}
