using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ETicaretAPI.Application.Dtos.Product
{
    public record UpdateProductDto(string Name, int Stock, float Price, DateTime UpdatedDate);
}
