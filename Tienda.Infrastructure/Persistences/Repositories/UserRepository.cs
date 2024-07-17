using Microsoft.EntityFrameworkCore;
using Tienda.Domain.Entities;
using Tienda.Infrastructure.Commons.Bases.Request;
using Tienda.Infrastructure.Commons.Bases.Response;
using Tienda.Infrastructure.Persistences.Interfaces;

namespace Tienda.Infrastructure.Persistences.Repositories
{
    public class UserRepository : GenericRepository<User>, IUserRepository
    {
        public UserRepository(TiendaDbContext context) : base(context) { }

        public async Task<BaseEntityResponse<User>> ListUsers(BaseFiltersRequest filters)
        {
            var response = new BaseEntityResponse<User>();
            var users = GetEntityQuery(x => x.AuditDeleteUser == null && x.AuditDeleteDate == null);

            if (filters.NumFilter is not null && !string.IsNullOrEmpty(filters.TextFilter))
            {
                switch (filters.NumFilter)
                {
                    case 1:
                        users = users.Where(x => x.Name!.Contains(filters.TextFilter));
                        break;
                    case 2:
                        users = users.Where(x => x.Email!.Contains(filters.TextFilter));
                        break;
                }
            }

            if (filters.StateFilter is not null)
            {
                users = users.Where(x => x.State.Equals(filters.StateFilter));
            }

            if (!string.IsNullOrEmpty(filters.StartDate) && !string.IsNullOrEmpty(filters.EndDate))
            {
                users = users.Where(x => x.AuditCreateDate >= Convert.ToDateTime(filters.StartDate) && x.AuditCreateDate <= Convert.ToDateTime(filters.EndDate).AddDays(1));
            }

            if (filters.Sort is null) filters.Sort = "Id";

            response.TotalRecords = await users.CountAsync();
            bool download = filters.Download ?? false; 
            response.Items = await Ordering(filters, users, !download).ToListAsync();

            return response;
        }
    }
}
