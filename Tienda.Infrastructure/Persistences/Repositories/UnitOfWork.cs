using Tienda.Domain.Entities;
using Tienda.Infrastructure.Persistences.Interfaces;

namespace Tienda.Infrastructure.Persistences.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly TiendaDbContext _context;
        public ICategoryRepository Category { get; private set; }
        public IUserRepository User { get; private set; }
        public IProductRepository Product { get; private set; }
        public ICartRepository Cart { get; private set; }
        public ICartItemRepository CartItem { get; private set; }



        public UnitOfWork(TiendaDbContext context)
        {
            _context = context;
            Category = new CategoryRepository(_context);
            User = new UserRepository(_context);
            Product = new ProductRepository(_context);
            Cart = new CartRepository(_context);
            CartItem = new CartItemRepository(_context);



        }
        public void Dispose()
        {
            _context.Dispose();
        }

        public void SaveChanges()
        {
            _context.SaveChanges();
        }

        public async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
        }
    }
}
