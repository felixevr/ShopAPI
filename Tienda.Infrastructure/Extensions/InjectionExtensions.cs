using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Tienda.Domain.Entities;
using Tienda.Infrastructure.Persistences.Interfaces;
using Tienda.Infrastructure.Persistences.Repositories;

namespace Tienda.Infrastructure.Extensions
{
    public static class InjectionExtensions
    {
        public static IServiceCollection AddInjectionInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
            var assembly = typeof(TiendaDbContext).Assembly.FullName;

            services.AddDbContext<TiendaDbContext>(
                options => options.UseSqlServer(
                    configuration.GetConnectionString("TiendaDBConnection"), b => b.MigrationsAssembly(assembly)), ServiceLifetime.Transient);

            services.AddTransient<IUnitOfWork, UnitOfWork>();
            services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));

            return services;
        }
    }
}

