using ETicaretAPI.Application.Repositories;
using ETicaretAPI.Application.RequestParameters;
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

        // Veritabanındaki toplam kayıt sayısını hesaplar
        public async Task<int> CountAsync(Expression<Func<T, bool>>? filter = null, CancellationToken cancellationToken = default)
        {
            return filter is not null
                ? await Table.CountAsync(filter, cancellationToken)
                : await Table.CountAsync(cancellationToken);
        }

        // Belirli bir şartla veritabanından verileri asenkron olarak döner
        public async Task<List<T>> FindAsync(Expression<Func<T, bool>> predicate, CancellationToken cancellationToken = default)
        {
            return await Table
                .Where(predicate)
                .ToListAsync(cancellationToken);
        }

        // Veritabanındaki tüm verileri getirir. Büyük veri setleri için dikkatli kullanılmalı veya alternatif olarak pagination metodu kullanılmalı.
        public async Task<List<T>> GetAllAsync(CancellationToken cancellationToken = default)
        {
            return await Table.ToListAsync(cancellationToken);
        }

        // Verilen predicate'e göre tek bir kaydı döner
        public async Task<T?> GetByFilterAsync(Expression<Func<T, bool>> predicate, CancellationToken cancellationToken = default)
        {
            return await Table.FirstOrDefaultAsync(predicate, cancellationToken);
        }

        // Id ye göre veritabanından bir kayıt getir
        public async Task<T> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
        {
            return await Table.FirstOrDefaultAsync(x => x.Id == id, cancellationToken);
        }

        public async Task<PagedResult<T>> GetPagedAsync
            (
                Pagination pagination,
                Guid? id = null,
                string? name = null,
                string? stock = null,
                string? price = null,
                string? createdDate = null,
                string? updatedDate = null,
                string? sortColumn = null,
                bool ascending = true,
                CancellationToken cancellationToken = default
)
            {

            IQueryable<T> query = Table;

            // ID filtresi
            if (id.HasValue && id != Guid.Empty)
            {
                query = query.Where(e => EF.Property<Guid>(e, "Id") == id);
            }

            // Name filtresi
            if (!string.IsNullOrEmpty(name))
            {
                query = query.Where(e => EF.Functions.Like(EF.Property<string>(e, "Name"), $"%{name}%"));
            }

            // Stock filtresi
            if (!string.IsNullOrEmpty(stock))
            {
                query = query.Where(e => EF.Functions.Like(EF.Property<string>(e, "Stock"), $"%{stock}%"));
            }

            // Price filtresi
            if (!string.IsNullOrEmpty(price))
            {
                query = query.Where(e => EF.Functions.Like(EF.Property<string>(e, "Price"), $"%{price}%"));
            }

            // CreatedDate filtresi
            if (!string.IsNullOrEmpty(createdDate))
            {
                query = query.Where(e => EF.Property<DateTime>(e, "CreatedDate").ToString().Contains(createdDate));
            }

            // UpdatedDate filtresi
            if (!string.IsNullOrEmpty(updatedDate))
            {
                query = query.Where(e => EF.Property<DateTime>(e, "UpdatedDate").ToString().Contains(updatedDate));
            }

            // Sıralama
            if (!string.IsNullOrEmpty(sortColumn))
            {
                if (sortColumn == "CreatedDate")
                {
                    query = ascending
                        ? query.OrderBy(e => EF.Property<DateTime>(e, sortColumn))
                        : query.OrderByDescending(e => EF.Property<DateTime>(e, sortColumn));
                }
                else if (sortColumn == "Price")
                {
                    query = ascending
                        ? query.OrderBy(e => EF.Property<decimal>(e, sortColumn))
                        : query.OrderByDescending(e => EF.Property<decimal>(e, sortColumn));
                }
                else
                {
                    query = ascending
                        ? query.OrderBy(e => EF.Property<string>(e, sortColumn))
                        : query.OrderByDescending(e => EF.Property<string>(e, sortColumn));
                }
            }
            else
            {
                query = query.OrderByDescending(e => EF.Property<DateTime>(e, "CreatedDate"));
            }

            // Toplam sayıyı al
            int totalItems = await query.CountAsync();

            // Sayfalama işlemi
            var items = await query
                .Skip((pagination.Page - 1) * pagination.Size)
                .Take(pagination.Size)
                .ToListAsync(cancellationToken);

            return new PagedResult<T>(items, totalItems, pagination.Page, pagination.Size);
        }


    }

}
