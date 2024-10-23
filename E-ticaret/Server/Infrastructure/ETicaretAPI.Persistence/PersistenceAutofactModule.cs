using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ETicaretAPI.Persistence.Repositories;
using ETicaretAPI.Persistence.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using ETicaretAPI.Application.Repositories.CustomerRepo;
using ETicaretAPI.Persistence.Repositories.CustomerRepo;
using ETicaretAPI.Application.Repositories.OrderRepo;
using ETicaretAPI.Persistence.Repositories.OrderRepo;
using ETicaretAPI.Application.Repositories.ProductRepo;
using ETicaretAPI.Persistence.Repositories.ProductRepo;
using Autofac;
using ETicaretAPI.Persistence.Repositories.FileRepo;
using ETicaretAPI.Application.Repositories.FileRepo;
using ETicaretAPI.Persistence.Repositories.ProductRepo.ProductImageFileRepo;
using ETicaretAPI.Application.Repositories.ProductRepo.ProductImageFileRepo;
using ETicaretAPI.Persistence.Repositories.InvoiceFileRepo;
using ETicaretAPI.Application.Repositories.InvoiceFileRepo;

namespace ETicaretAPI.Persistence
{
    public class PersistenceAutofactModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            // Db
            builder.Register(c =>
            {
                var config = c.Resolve<IConfiguration>();
                var optionsBuilder = new DbContextOptionsBuilder<AppDbContext>();
                optionsBuilder.UseNpgsql(config.GetConnectionString("PostgreSql"));
                return new AppDbContext(optionsBuilder.Options);
            }).AsSelf().InstancePerLifetimeScope();


            // Repo
            builder.RegisterType<ReadCustomerRepo>().As<IReadCustomerRepo>().InstancePerLifetimeScope();
            builder.RegisterType<WriteCustomerRepo>().As<IWriteCustomerRepo>().InstancePerLifetimeScope();
            builder.RegisterType<ReadOrderRepo>().As<IReadOrderRepo>().InstancePerLifetimeScope();
            builder.RegisterType<WriteOrderRepo>().As<IWriteOrderRepo>().InstancePerLifetimeScope();
            builder.RegisterType<ReadProductRepo>().As<IReadProductRepo>().InstancePerLifetimeScope();
            builder.RegisterType<WriteProductRepo>().As<IWriteProductRepo>().InstancePerLifetimeScope();
            builder.RegisterType<ReadFileRepo>().As<IReadFileRepo>().InstancePerLifetimeScope();
            builder.RegisterType<WriteFileRepo>().As<IWriteFileRepo>().InstancePerLifetimeScope();
            builder.RegisterType<ReadProductImageFileRepo>().As<IReadProductImageFileRepo>().InstancePerLifetimeScope();
            builder.RegisterType<WriteProductImageFileRepo>().As<IWriteProductImageFileRepo>().InstancePerLifetimeScope();
            builder.RegisterType<ReadInvoiceFileRepo>().As<IReadInvoiceRepo>().InstancePerLifetimeScope();
            builder.RegisterType<WriteInvoiceRepo>().As<IWriteInvoiceRepo>().InstancePerLifetimeScope();
        }
    }
}
