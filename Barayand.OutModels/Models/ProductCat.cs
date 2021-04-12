using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Barayand.OutModels.Models
{
    public class ProductCat
    {
        public int PC_Id { get; set; }
        public int? PC_ParentId { get; set; }
        public string PC_Title { get; set; }
        public string PC_ImgTitle { get; set; }
        public string PC_PrefixCode { get; set; }
        public string PC_Description { get; set; }//safe buy guid
        public int PC_OrderField { get; set; } = 0;
        public bool PC_Status { get; set; } = false;
        public bool PC_IsDeleted { get; set; } = false;
        public bool PC_AttrAble { get; set; } = false; // this field show current cat can have attributes or not
        public string PC_Logo { get; set; }
        public string PC_Seo { get; set; }
        public string PC_ParentTitle { get; set; }
        public int PC_Level { get; set; } = 1;
        public int PC_Type { get; set; } = 1;//1=>Physical 2=>Digital
        public string Lang { get; set; } = "fa";
    }
}
