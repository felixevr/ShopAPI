using Tienda.Domain.Entities;
using Tienda.Infrastructure.Commons.Bases.Response;
using Tienda.Infrastructure.Persistences.Interfaces;
using Tienda.Utilities.Static;
using Microsoft.EntityFrameworkCore;

namespace Tienda.Infrastructure.Persistences.Repositories
{
    public class CartItemRepository : GenericRepository<CartItem>, ICartItemRepository
    {
        public CartItemRepository(TiendaDbContext context) : base(context) { }

        //public async Task<BaseEntityResponse<CartItem>> ListCartItems()
        //{
        //    var response = new BaseEntityResponse<CartItem>();
        //    var carts = GetEntityQuery(x => x.AuditDeleteUser == null && x.AuditDeleteDate == null && x.State == (int)StateTypes.Active)
        //        .Include(x => x.CartID.Equals(x => x.).Where(p => p.State == (int)StateTypes.Active));

        //    response.TotalRecords = await carts.CountAsync();
        //    response.Items = await carts.ToListAsync();

        //    return response;
        //}
    }
}
