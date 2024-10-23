using ETicaretAPI.Application.Repositories.ProductRepo.ProductImageFileRepo;
using ETicaretAPI.Domain.Entities.Common;
using ETicaretAPI.Persistence.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ETicaretAPI.Persistence.Repositories.ProductRepo.ProductImageFileRepo
{
    public class WriteProductImageFileRepo : WriteRepository<ProductImageFile>, IWriteProductImageFileRepo
    {
        public WriteProductImageFileRepo(AppDbContext context) : base(context)
        {
        }
    }
}
