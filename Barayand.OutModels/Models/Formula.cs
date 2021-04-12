using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Barayand.OutModels.Models
{
    public class Formula
    {
        public int F_Id { get; set; }
        public string F_Title { get; set; }
        public string F_Formula { get; set; }
        public bool F_Status { get; set; } = true;
        public bool F_IsDeleted { get; set; } = false;
        public int F_CatId { get; set; } = 1;
    }
}
