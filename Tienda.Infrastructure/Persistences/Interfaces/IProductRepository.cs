using Tienda.Domain.Entities;
using Tienda.Infrastructure.Commons.Bases.Request;
using Tienda.Infrastructure.Commons.Bases.Response;

namespace Tienda.Infrastructure.Persistences.Interfaces
{
    public interface IProductRepository : IGenericRepository<Product>
    {
        public Task<BaseEntityResponse<Product>> ListProducts(BaseFiltersRequest filters);
    }
}