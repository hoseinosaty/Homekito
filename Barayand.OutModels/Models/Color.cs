using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Barayand.OutModels.Models
{
    public class Color
    {
        public int C_Id { get; set; }
        public string C_Title { get; set; }
        public string C_HexColor { get; set; } = "#ffffff";
        public bool C_IsDeleted { get; set; } = false;
        public bool C_Status { get; set; } = false;
    }
}
