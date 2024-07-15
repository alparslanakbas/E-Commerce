using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ETicaretAPI.Domain.Entities.Common
{
    public sealed class Product : BaseEntity
    {
        public string Name { get; init; } = null!;
        public int Stock { get; init; }
        public long Price { get; init; }
    }
}