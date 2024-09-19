using ETicaretAPI.Application.Repositories;
using ETicaretAPI.Domain.Entities.Common;
using ETicaretAPI.Persistence.Context;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace ETicaretAPI.Persistence.Repositories
{
    public class ReadRepository<T> : IReadRepository<T> where T : BaseEntity
    {
        private readonly AppDbContext _context;

        public ReadRepository(AppDbContext context)
        {
            _context = context;
        }

        public DbSet<T> Table => _context.Set<T>();

        public async Task <bool> ExistInDatabaseAsync(Expression<Func<T, bool>> predicate)
        {
             return await _context.Set<T>().AnyAsync(predicate);
        }

        public IQueryable<T> GetAll(bool tracking = false) 
        {
            var query = _context.Set<T>();

            if (!tracking)
                query.AsNoTracking();  

            return query;
        }

        public async Task<T> GetByIdAsync(string id, bool tracking = false)
        {
            var query = Table.AsQueryable();
            if (!tracking)
                query = Table.AsNoTracking();

            return await query.FirstOrDefaultAsync(x => x.Id == Guid.Parse(id));
        }

        public async Task<T> GetSingleAsync(Expression<Func<T, bool>> method, bool tracking = false)
        {
            var query = Table.AsQueryable();
            if (!tracking)
                query = Table.AsNoTracking();

            return await query.FirstOrDefaultAsync(method);
        } 

        public IQueryable<T> GetWhere(Expression<Func<T, bool>> method, bool tracking = false)
        {
            var query = Table.Where(method);

            if (!tracking)
                query.AsNoTracking();

            return query;
        }
    }
}
