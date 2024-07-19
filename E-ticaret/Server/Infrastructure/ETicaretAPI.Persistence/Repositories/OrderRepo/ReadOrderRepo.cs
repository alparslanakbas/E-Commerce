using ETicaretAPI.Application.Repositories.CustomerRepo;
using ETicaretAPI.Application.Repositories.OrderRepo;
using ETicaretAPI.Domain.Entities.Common;
using ETicaretAPI.Persistence.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ETicaretAPI.Persistence.Repositories.OrderRepo
{
    public class ReadOrderRepo : ReadRepository<Order>, IReadOrderRepo
    {
        public ReadOrderRepo(AppDbContext context) : base(context)
        {
        }
    }
}
