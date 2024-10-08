using ETicaretAPI.Application.Repositories;
using ETicaretAPI.Domain.Entities.Common;
using ETicaretAPI.Persistence.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace ETicaretAPI.Persistence.Repositories
{
    public class WriteRepository<T> : IWriteRepository<T> where T : BaseEntity
    {
        private readonly AppDbContext _context;

        public WriteRepository(AppDbContext context)
        {
            _context = context;
        }

        public DbSet<T> Table => _context.Set<T>();

        // Tek ekleme 
        public async Task AddAsync(T entity, CancellationToken cancellationToken = default)
        {
            await Table.AddAsync(entity, cancellationToken);
        }

        // Çoklu ekleme
        public async Task<bool> AddRangeAsync(List<T> datas)
        {
            await Table.AddRangeAsync(datas);
            return true;
        }

        // silme
        public async Task DeleteAsync(T entity, CancellationToken cancellationToken = default)
        {
            var existingEntity = await Table.FirstOrDefaultAsync(x => x.Id == entity.Id, cancellationToken);
            if (existingEntity is not null)
            {
                Table.Remove(existingEntity);
            }
        }

        // Veriyi kaydet
        public async Task<int> SaveAsync()
        {
            return await _context.SaveChangesAsync(); 
        }

        // Güncelleme
        public async Task UpdateAsync(T entity, CancellationToken cancellationToken = default)
        {
            var existingEntity = await Table
                .AsNoTracking()
                .FirstOrDefaultAsync(x => x.Id == entity.Id, cancellationToken);

            if (existingEntity is not null)
            {
                Table.Update(entity); 
            }
        }

        public async Task<bool> ExistInDatabaseAsync(Expression<Func<T, bool>> predicate)
        {
            return await Table.AnyAsync(predicate);
        }
    }

}
