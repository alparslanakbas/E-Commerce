using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ETicaretAPI.Domain.Entities.Common
{
    public class Customer : BaseEntity
    {
        public required string FirstName { get; init; }
        public required string LastName { get; init; }
        public required ICollection<Order> Orders { get; init; }
    }
}
