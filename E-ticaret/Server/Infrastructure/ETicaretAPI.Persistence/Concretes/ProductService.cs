using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ETicaretAPI.Application.Abstractions;
using ETicaretAPI.Domain.Entities.Common;

namespace ETicaretAPI.Persistence.Concretes
{
    public class ProductService : IProductService
    {
        public List<Product> GetProducts() => new()
        {
            new() 
            {
                Id = Guid.NewGuid(),
                Name = "Lenovo Legion RFC27 Monitor",
                Stock = 100,
                Price = 5500,
                CreatedDate = DateTime.Now
            },
            new() 
            {
                Id = Guid.NewGuid(),
                Name = "AMD Ryzen 5 5500 İşlemci",
                Stock = 100,
                Price = 3000,
                CreatedDate = DateTime.Now
            },
            new() 
            {
                Id = Guid.NewGuid(),
                Name = "Palid GTX 1660TI Ekran Kartı",
                Stock = 100,
                Price = 8500,
                CreatedDate = DateTime.Now
            },
            new() 
            {
                Id = Guid.NewGuid(),
                Name = "B450MK-I Ana Kart",
                Stock = 100,
                Price = 2999,
                CreatedDate = DateTime.Now
            }
        };
    }
}