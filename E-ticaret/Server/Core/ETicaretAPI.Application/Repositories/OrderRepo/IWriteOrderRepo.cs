﻿using ETicaretAPI.Domain.Entities.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ETicaretAPI.Application.Repositories.OrderRepo
{
    public interface IWriteOrderRepo : IWriteRepository<Order>
    {
    }
}