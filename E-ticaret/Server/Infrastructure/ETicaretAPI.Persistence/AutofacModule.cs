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

namespace ETicaretAPI.Persistence
{
    public class AutofacModule : Module
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
        }
    }
}
