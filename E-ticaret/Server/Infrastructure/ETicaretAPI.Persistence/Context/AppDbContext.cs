﻿using ETicaretAPI.Domain.Entities.Common;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ETicaretAPI.Persistence.Context
{
    public sealed class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions options) : base(options)
        { }

        public DbSet<Product> Products { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<Customer> Customers { get; set; }
        public DbSet<Domain.Entities.Common.File> Files { get; set; }
        public DbSet<ProductImageFile> ProductImageFiles { get; set; }
        public DbSet<InvoiceFile> InvoiceFiles { get; set; }

        public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            // Change tracker entityler üzerinden yapılan değişikliklerin ya da yeni eklenen verinin yakalanmasını sağlayan özelliktir. Update operasyonlarında track edilen verileri yakalayıp elde etmemizi sağlar
            var datas = ChangeTracker.Entries<BaseEntity>();

            foreach (var data in datas)
            {
                _ = data.State switch
                {
                    EntityState.Added => data.Entity.CreatedDate = DateTime.UtcNow.AddHours(3),
                    EntityState.Modified => data.Entity.UpdatedDate = DateTime.UtcNow.AddHours(3),
                    _ => DateTime.UtcNow.AddHours(3)
                };
            }

            return await base.SaveChangesAsync(cancellationToken);
        }

    }
}
