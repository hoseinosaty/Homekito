using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Barayand.OutModels.Models
{
    public class ManufactureCuntry
    {
        public int M_Id { get; set; }
        public string M_Title { get; set; }
        public int M_Sort { get; set; } = 1;
        public bool M_Status { get; set; } = true;
        public bool M_IsDeleted { get; set; } = false;
    }
}
