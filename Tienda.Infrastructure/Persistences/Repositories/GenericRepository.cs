using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using System.Linq.Dynamic.Core;
using Tienda.Domain.Entities;
using Tienda.Infrastructure.Commons.Bases.Request;
using Tienda.Infrastructure.Helpers;
using Tienda.Infrastructure.Persistences.Interfaces;
using Tienda.Utilities.Static;

namespace Tienda.Infrastructure.Persistences.Repositories
{
    public class GenericRepository<T> : IGenericRepository<T> where T : BaseEntity
    {
        private readonly TiendaDbContext _context;
        private readonly DbSet<T> _entity;

        public GenericRepository(TiendaDbContext dbContext)
        {
            _context = dbContext;
            _entity = _context.Set<T>();
        }

        public async Task<IEnumerable<T>> GetAllAsync()
        {
            var getAll = await _entity
                .Where(x => x.State.Equals((int)StateTypes.Active) && x.AuditDeleteUser == null && x.AuditDeleteDate == null).AsNoTracking().ToListAsync();

            return getAll;
        }

        public async Task<T> GetByIdAsync(int id)
        {
            var getById = await _entity.AsNoTracking().FirstOrDefaultAsync(x => x.Id.Equals(id));

            return getById!;
        }

        public async Task<bool> RegisterAsync(T entity)
        {
            entity.AuditCreateUser = 1;
            entity.AuditCreateDate = DateTime.Now;

            await _context.AddAsync(entity);

            var recordsAffected = await _context.SaveChangesAsync();
            return recordsAffected > 0;
        }
        public async Task<bool> EditAsync(T entity)
        {
            entity.AuditUpdateUser = 1;
            entity.AuditUpdateDate = DateTime.Now;

            _context.Update(entity);
            _context.Entry(entity).Property(x => x.AuditCreateUser).IsModified = false;
            _context.Entry(entity).Property(x => x.AuditCreateDate).IsModified = false;

            var recordsAffected = await _context.SaveChangesAsync();
            return recordsAffected > 0;
        }

        public async Task<bool> RemoveAsync(int id)
        {
            T entity = await GetByIdAsync(id);

            entity.AuditDeleteUser = 1;
            entity.AuditDeleteDate = DateTime.Now;

            _context.Update(entity);

            var recordsAffected = await _context.SaveChangesAsync();
            return recordsAffected > 0;
        }

        public IQueryable<T> GetEntityQuery(Expression<Func<T, bool>>? filter = null)
        {
            IQueryable<T> query = _entity; // Conversión implícita de DBSet a IQueryable, ya que DBSet implementa IQueryable

            if (filter != null) query = query.Where(filter); // Trae los 7 registros completos

            return query;
        }

        public IQueryable<TDTO> Ordering<TDTO>(BasePaginationRequest request, IQueryable<TDTO> queryable, bool pagination = false) where TDTO : class
        {
            IQueryable<TDTO> queryDto = request.Order == "desc" ? queryable.OrderBy($"{request.Sort} descending") : queryable.OrderBy($"{request.Sort} ascending");
            if (pagination) queryDto = queryDto.Paginate(request);
            return queryDto;
        }
    }
}
