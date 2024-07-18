namespace Tienda.Infrastructure.Persistences.Interfaces
{
    public interface IUnitOfWork : IDisposable
    {
        // Declaración o matricula de nuestras interfaces a nivel de repository
        // Empezamos con el ejempo de ICategoryRepository que creamos de prueba
        ICategoryRepository Category { get; }
        IUserRepository User { get; }
        IProductRepository Product { get; }
        ICartRepository Cart { get; }
        void SaveChanges();
        Task SaveChangesAsync();
    }
}
