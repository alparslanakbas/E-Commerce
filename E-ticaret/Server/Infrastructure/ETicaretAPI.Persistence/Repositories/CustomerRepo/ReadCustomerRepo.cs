using ETicaretAPI.Application.Repositories.CustomerRepo;
using ETicaretAPI.Domain.Entities.Common;
using ETicaretAPI.Persistence.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ETicaretAPI.Persistence.Repositories.CustomerRepo
{
    public class ReadCustomerRepo : ReadRepository<Customer>, IReadCustomerRepo
    {
        public ReadCustomerRepo(AppDbContext context) : base(context)
        {
        }
    }
}
