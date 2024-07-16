using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ETicaretAPI.Domain.Entities.Common
{
    public class Order : BaseEntity
    {
        public string? Description { get; init; }
        public required string Address { get; init; }
        public required ICollection<Product> Products { get; init; }
        public required Customer Customer { get; init; } 
    }
}
