using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ETicaretAPI.Application.Abstractions;
using ETicaretAPI.Persistence.Concretes;
using ETicaretAPI.Persistence.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace ETicaretAPI.Persistence
{
    public static class ServiceRegistration
    {
        public static void AddPersistenceServices(this IServiceCollection services, IConfigurationSection configuration)
        {

            //services.AddSingleton<IProductService, ProductService>();
            services.AddDbContext<AppDbContext>(options => options
            .UseNpgsql(configuration
                .GetConnectionString("PostgreSql"))
            );
        }
    }
}
