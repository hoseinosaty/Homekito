using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Barayand.Models.RuntimeModels
{
    public class PaginationModel<T> where T : notnull
    {
        public PaginationModel()
        {

        }
            
        public List<T> Entities { get; set; } = null;
        public int ItemsPerPage { get; set; } = 10;
        public int PageCount()
        {
            if (Entities == null || Entities.Count() < 1)
                return 0;
            if (ItemsPerPage < 1)
                ItemsPerPage = 10;

            return (int)Math.Ceiling((double)Entities.Count() / ItemsPerPage);

        }
        public int CurrentPage { get; set; }
    }
}
