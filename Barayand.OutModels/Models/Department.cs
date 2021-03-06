using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Barayand.OutModels.Models
{
    public class Department
    {
        public int D_Id { get; set; }
        public string D_Title { get; set; }
        public string D_Email { get; set; }
        public string D_Tel { get; set; }
        public int D_SortField { get; set; } = 1;
        public bool D_Status { get; set; } = true;
        public string Lang { get; set; } = "fa";
    }
}
