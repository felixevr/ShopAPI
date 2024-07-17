using Tienda.Domain.Entities;
using Tienda.Infrastructure.Commons.Bases.Request;
using Tienda.Infrastructure.Commons.Bases.Response;

namespace Tienda.Infrastructure.Persistences.Interfaces
{
    public interface IUserRepository : IGenericRepository<User>
    {
        Task<BaseEntityResponse<User>> ListUsers(BaseFiltersRequest filters);
    }
}
