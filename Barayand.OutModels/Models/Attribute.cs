using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Barayand.OutModels.Models
{
    public class Attribute
    {
        public int A_Id { get; set; }
        public string A_Title { get; set; }
        public int A_Type { get; set; } = 1;//1=>ComboBox 2=>TextBox
        public int A_SortField { get; set; } = 0;
        public bool A_UseInSearch { get; set; } = false;
        public bool A_IsDeleted { get; set; } = false;
        public bool A_Status { get; set; } = false;
        public bool A_ShowInDetail { get; set; } = false;//if true show this attribute in product detail box

        public string Lang { get; set; } = "fa";
    }
}
