using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HomeKito.Models
{
    public class Paging
    {
        public int TotalCount { get; set; }
        public int PageSize { get; set; }
        public int CurrentPage { get; set; }
        public int TotalPages { get; set; }
    }
}
