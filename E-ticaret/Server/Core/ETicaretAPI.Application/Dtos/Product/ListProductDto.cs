﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ETicaretAPI.Application.Dtos.Product
{
    public record ListProductDto(Guid Id, string Name, int Stock, float Price, DateTime CreatedDate, DateTime UpdatedDate);
}
