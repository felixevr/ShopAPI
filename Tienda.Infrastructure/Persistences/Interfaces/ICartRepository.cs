using Tienda.Domain.Entities;
using Tienda.Infrastructure.Commons.Bases.Request;
using Tienda.Infrastructure.Commons.Bases.Response;

namespace Tienda.Infrastructure.Persistences.Interfaces
{
    public interface ICartRepository : IGenericRepository<Cart>
    {
        Task<BaseEntityResponse<Cart>> ListCartsWithProducts(int userId);
        Task<BaseEntityResponse<Cart>> GetCartByIdWithProducts(int cartId);
    }
}
