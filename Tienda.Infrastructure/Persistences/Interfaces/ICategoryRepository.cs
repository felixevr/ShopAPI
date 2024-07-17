using Tienda.Domain.Entities;
using Tienda.Infrastructure.Commons.Bases.Request;
using Tienda.Infrastructure.Commons.Bases.Response;

namespace Tienda.Infrastructure.Persistences.Interfaces
{
    public interface ICategoryRepository : IGenericRepository<Category>
    {
        Task<BaseEntityResponse<Category>> ListCategories(BaseFiltersRequest filters);
        Task<BaseEntityResponse<Category>> ListCategoriesWithProducts();
    }
}
