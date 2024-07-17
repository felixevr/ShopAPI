using Microsoft.EntityFrameworkCore;
using Tienda.Domain.Entities;
using Tienda.Infrastructure.Commons.Bases.Request;
using Tienda.Infrastructure.Commons.Bases.Response;
using Tienda.Infrastructure.Persistences.Interfaces;
using Tienda.Utilities.Static;

namespace Tienda.Infrastructure.Persistences.Repositories
{
    public class CategoryRepository : GenericRepository<Category>, ICategoryRepository
    {
        public CategoryRepository(TiendaDbContext context) : base(context) { }

        public async Task<BaseEntityResponse<Category>> ListCategories(BaseFiltersRequest filters)
        {
            var response = new BaseEntityResponse<Category>();
            var categories = GetEntityQuery(x => x.AuditDeleteUser == null && x.AuditDeleteDate == null);

            if (filters.NumFilter is not null && !string.IsNullOrEmpty(filters.TextFilter))
            {
                switch (filters.NumFilter)
                {
                    case 1:
                        categories = categories.Where(x => x.Name!.Contains(filters.TextFilter));
                        break;
                    case 2:
                        categories = categories.Where(x => x.Description!.Contains(filters.TextFilter));
                        break;
                }
            }

            if (filters.StateFilter is not null)
            {
                categories = categories.Where(x => x.State.Equals(filters.StateFilter));
            }

            if (!string.IsNullOrEmpty(filters.StartDate) && !string.IsNullOrEmpty(filters.EndDate))
            {
                categories = categories.Where(x => x.AuditCreateDate >= Convert.ToDateTime(filters.StartDate) && x.AuditCreateDate <= Convert.ToDateTime(filters.EndDate).AddDays(1));
            }

            if (filters.Sort is null) filters.Sort = "Id";

            response.TotalRecords = await categories.CountAsync();
            bool download = filters.Download ?? false; 
            response.Items = await Ordering(filters, categories, !download).ToListAsync();
            
            return response;
        }

        public async Task<BaseEntityResponse<Category>> ListCategoriesWithProducts()
        {
            var response = new BaseEntityResponse<Category>();
            var categories = GetEntityQuery(x => x.AuditDeleteUser == null && x.AuditDeleteDate == null && x.State == (int)StateTypes.Active)
                .Include(x => x.Products.Where(p => p.State == (int)StateTypes.Active));

            response.TotalRecords = await categories.CountAsync();
            response.Items = await categories.ToListAsync();

            return response;
        }
    }
}
