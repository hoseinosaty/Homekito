using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Barayand.OutModels.Models
{
    public class Faq
    {
        public int FA_Id { get; set; }
        public int FA_CatId { get; set; } = 0;
        public string FA_Title { get; set; }
        public int FA_SortField { get; set; } = 1;
        public bool FA_Status { get; set; } = true;
        public string FA_Answer { get; set; }
        public bool FA_IsDeleted { get; set; } = false;
    }
}
