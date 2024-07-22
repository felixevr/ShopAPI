using Microsoft.EntityFrameworkCore;
using Tienda.Domain.Entities;
using Tienda.Infrastructure.Commons.Bases.Response;
using Tienda.Infrastructure.Persistences.Interfaces;
using Tienda.Utilities.Static;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Tienda.Infrastructure.Persistences.Repositories
{
    public class CartRepository : GenericRepository<Cart>, ICartRepository
    {
        public CartRepository(TiendaDbContext context) : base(context) { }


        public async Task<BaseEntityResponse<Cart>> ListCartsWithProducts(int userId)
        {
            var response = new BaseEntityResponse<Cart>();

            var carts = GetEntityQuery(x => x.AuditDeleteUser == null && x.AuditDeleteDate == null && x.State == (int)StateTypes.Active && x.UserID == userId)
                            .Include(x => x.CartItems.Where(x => x.CartID.Equals(x.CartID) && x.State == (int)StateTypes.Active))
                               .ThenInclude(ci => ci.Product);

            if (carts is not null)
            {
                response.TotalRecords = await carts.CountAsync();
                response.Items = await carts.ToListAsync();
            }

            return response;
        }

        public async Task<BaseEntityResponse<Cart>> GetCartByIdWithProducts(int cartId)
        {
            var response = new BaseEntityResponse<Cart>();
            var cart =  GetEntityQuery(x => x.AuditDeleteUser == null && x.AuditDeleteDate == null && x.State == (int)StateTypes.Active && x.Id.Equals(cartId))
                            .Include(x => x.CartItems.Where(ci => ci.State.Equals((int)StateTypes.Active)))
                                .ThenInclude(ci => ci.Product);


            if (cart is not null)
            {
                response.TotalRecords = await cart.CountAsync();
                response.Items = await cart.ToListAsync();
            }

            return response;
        }
    }
}
