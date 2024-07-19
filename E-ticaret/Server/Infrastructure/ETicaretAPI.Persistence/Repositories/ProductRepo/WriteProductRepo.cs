using ETicaretAPI.Application.Repositories.ProductRepo;
using ETicaretAPI.Domain.Entities.Common;
using ETicaretAPI.Persistence.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ETicaretAPI.Persistence.Repositories.ProductRepo
{
    public class WriteProductRepo : WriteRepository<Product>, IWriteProductRepo
    {
        public WriteProductRepo(AppDbContext context) : base(context)
        {
        }
    }
}
