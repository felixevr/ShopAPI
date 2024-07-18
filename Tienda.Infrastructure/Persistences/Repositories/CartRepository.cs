using Microsoft.EntityFrameworkCore;
using Tienda.Domain.Entities;
using Tienda.Infrastructure.Commons.Bases.Response;
using Tienda.Infrastructure.Persistences.Interfaces;
using Tienda.Utilities.Static;

namespace Tienda.Infrastructure.Persistences.Repositories
{
    public class CartRepository : GenericRepository<Cart>, ICartRepository
    {
        public CartRepository(TiendaDbContext context) : base(context) { }


        public async Task<BaseEntityResponse<Cart>> ListCartsWithProducts()
        {
            var response = new BaseEntityResponse<Cart>();
            var carts = GetEntityQuery(x => x.AuditDeleteUser == null && x.AuditDeleteDate == null && x.State == (int)StateTypes.Active)
                .Include(x => x.CartItems.Where(p => p.State == (int)StateTypes.Active));

            response.TotalRecords = await carts.CountAsync();
            response.Items = await carts.ToListAsync();

            return response;
        }
    }
}
