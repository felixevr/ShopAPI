using Microsoft.EntityFrameworkCore;
using Tienda.Domain.Entities;
using Tienda.Infrastructure.Commons.Bases.Request;
using Tienda.Infrastructure.Commons.Bases.Response;
using Tienda.Infrastructure.Persistences.Interfaces;

namespace Tienda.Infrastructure.Persistences.Repositories
{
    internal class ProductRepository : GenericRepository<Product>, IProductRepository
    {
        public ProductRepository(TiendaDbContext context) : base(context) { }

        public async Task<BaseEntityResponse<Product>> ListProducts(BaseFiltersRequest filters)
        {
            var response = new BaseEntityResponse<Product>();
            var products = GetEntityQuery(x => x.AuditDeleteUser == null && x.AuditDeleteDate == null);

            if (filters.NumFilter is not null && !string.IsNullOrEmpty(filters.TextFilter))
            {
                switch (filters.NumFilter)
                {
                    case 1:
                        products = products.Where(x => x.Name!.Contains(filters.TextFilter));
                        break;
                    case 2:
                        products = products.Where(x => x.Description!.Contains(filters.TextFilter));
                        break;
                }
            }

            if (filters.StateFilter is not null)
            {
                products = products.Where(x => x.State.Equals(filters.StateFilter));
            }

            if (!string.IsNullOrEmpty(filters.StartDate) && !string.IsNullOrEmpty(filters.EndDate))
            {
                products = products.Where(x => x.AuditCreateDate >= Convert.ToDateTime(filters.StartDate) && x.AuditCreateDate <= Convert.ToDateTime(filters.EndDate).AddDays(1));
            }

            if (filters.Sort is null) filters.Sort = "Id";

            response.TotalRecords = await products.CountAsync();
            bool download = filters.Download ?? false;
            response.Items = await Ordering(filters, products, !download).ToListAsync();

            return response;
        }
    }
}
