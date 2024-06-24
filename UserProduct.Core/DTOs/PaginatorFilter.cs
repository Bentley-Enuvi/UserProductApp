using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UserProduct.Core.DTOs
{
    public class PaginationFilter
    {
        public PaginationFilter(int pageNumber, int pageSize)
        {
            PageNumber = pageNumber < 1 ? 1 : pageNumber;
            PageSize = pageSize is > 5 or < 1 ? 5 : pageSize;
        }

        public PaginationFilter()
        {
            PageNumber = 1;
            PageSize = 5;
        }

        public int PageNumber { get; }
        public int PageSize { get; }
    }
}
