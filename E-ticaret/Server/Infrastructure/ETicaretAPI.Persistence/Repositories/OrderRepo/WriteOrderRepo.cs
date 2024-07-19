﻿using ETicaretAPI.Application.Repositories.OrderRepo;
using ETicaretAPI.Domain.Entities.Common;
using ETicaretAPI.Persistence.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ETicaretAPI.Persistence.Repositories.OrderRepo
{
    public class WriteOrderRepo : WriteRepository<Order>, IWriteOrderRepo
    {
        public WriteOrderRepo(AppDbContext context) : base(context)
        {
        }
    }
}
