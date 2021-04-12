using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Barayand.OutModels.Models
{
    public class Brand
    {
        public int B_Id { get; set; }
        public string B_Title { get; set; }
        public string B_EnTitle { get; set; }
        public string B_Logo { get; set; }
        public int B_SortField { get; set; } = 0;
        public string B_Description { get; set; }
        public string B_Seo { get; set; }
        public bool B_Status { get; set; } = false;
        public bool B_IsDeleted { get; set; } = false;
        public bool B_ShowInIndex { get; set; } = true;
        public string B_SiteLink { get; set; } = "https://homekito.com";
        public string Lang { get; set; } = "fa";
    }
}
