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
    public class ReadInvoiceFileRepo : ReadRepository<InvoiceFile>, IReadInvoiceRepo
    {
        public ReadInvoiceFileRepo(AppDbContext context) : base(context)
        {
        }
    }
}
