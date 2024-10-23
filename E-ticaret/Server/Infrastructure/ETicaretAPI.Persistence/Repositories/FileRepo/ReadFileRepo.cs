using ETicaretAPI.Application.Repositories.FileRepo;
using ETicaretAPI.Persistence.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ETicaretAPI.Persistence.Repositories.FileRepo
{
    public class ReadFileRepo : ReadRepository<Domain.Entities.Common.File>, IReadFileRepo
    {
        public ReadFileRepo(AppDbContext context) : base(context)
        {
        }
    }
}
