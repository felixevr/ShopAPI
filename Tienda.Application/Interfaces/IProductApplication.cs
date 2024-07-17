using Tienda.Application.Commons.Bases;
using Tienda.Application.Dtos.Request;
using Tienda.Application.Dtos.Response;
using Tienda.Infrastructure.Commons.Bases.Request;
using Tienda.Infrastructure.Commons.Bases.Response;

namespace Tienda.Application.Interfaces
{
    public interface IProductApplication
    {
        Task<BaseResponse<BaseEntityResponse<ProductResponseDto>>> ListProducts(BaseFiltersRequest filters);
        Task<BaseResponse<IEnumerable<ProductSelectResponseDto>>> ListSelectProducts();
        Task<BaseResponse<ProductSelectResponseDto>> ProductById(int UserId);
        Task<BaseResponse<bool>> RegisterProduct(ProductRequestDto requestDto);
        Task<BaseResponse<bool>> EditProduct(int UserId, ProductRequestDto requestDto);
        Task<BaseResponse<bool>> RemoveProduct(int UserId);
    }
}
