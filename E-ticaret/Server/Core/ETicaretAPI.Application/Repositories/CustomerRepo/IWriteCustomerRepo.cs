﻿using ETicaretAPI.Domain.Entities.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ETicaretAPI.Application.Repositories.CustomerRepo
{
    public interface IWriteCustomerRepo : IWriteRepository<Customer>
    {
    }
}
