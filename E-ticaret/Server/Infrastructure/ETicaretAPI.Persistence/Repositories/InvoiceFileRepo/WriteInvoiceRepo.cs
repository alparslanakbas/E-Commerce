using ETicaretAPI.Application.Repositories.InvoiceFileRepo;
using ETicaretAPI.Domain.Entities.Common;
using ETicaretAPI.Persistence.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ETicaretAPI.Persistence.Repositories.InvoiceFileRepo
{
    public class WriteInvoiceRepo : WriteRepository<InvoiceFile>, IWriteInvoiceRepo
    {
        public WriteInvoiceRepo(AppDbContext context) : base(context)
        {
        }
    }
}
