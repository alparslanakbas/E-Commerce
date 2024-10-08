using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ETicaretAPI.Application.RequestParameters
{
    public class PagedResult<T>
    {
        public PagedResult(List<T> data, int totalItems, int page, int size)
        {
            Data = data;
            TotalItems = totalItems;
            Page = page;
            Size = size;
        }

        public List<T> Data { get; set; }
        public int TotalItems { get; set; }
        public int Page { get; set; }
        public int Size { get; set; }
    }
}
