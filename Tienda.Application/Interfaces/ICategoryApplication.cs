using Tienda.Application.Commons.Bases;
using Tienda.Application.Dtos.Request;
using Tienda.Application.Dtos.Response;
using Tienda.Domain.Entities;
using Tienda.Infrastructure.Commons.Bases.Request;
using Tienda.Infrastructure.Commons.Bases.Response;

namespace Tienda.Application.Interfaces
{
    public interface ICategoryApplication
    {
        Task<BaseResponse<BaseEntityResponse<CategoryResponseDto>>> ListCategories(BaseFiltersRequest filters);
        Task<BaseResponse<BaseEntityResponse<CategoryResponseDto>>> ListCategoriesWithProducts();
        Task<BaseResponse<IEnumerable<CategorySelectResponseDto>>> ListSelectCategories();
        Task<BaseResponse<CategorySelectResponseDto>> CategoryById(int categoryId);
        Task<BaseResponse<bool>> RegisterCategory(CategoryRequestDto requestDto);
        Task<BaseResponse<bool>> EditCategory(int categoryId, CategoryRequestDto requestDto);
        Task<BaseResponse<bool>> RemoveCategory(int categoryId);
    }
}
