using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ETicaretAPI.Application.Repositories.FileRepo
{
    public interface IWriteFileRepo : IWriteRepository<Domain.Entities.Common.File>
    {
    }
}
