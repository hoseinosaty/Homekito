using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Barayand.OutModels.Models
{
    public class FaqCategory
    {
        public int F_Id { get; set; }
        public string F_Title { get; set; }
        public int F_SortField { get; set; } = 1;
        public string F_Logo { get; set; }
        public bool F_Status { get; set; } = true;
    }
}
