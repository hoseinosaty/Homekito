using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Barayand.OutModels.Models
{
    public class NoticesGategory
    {
        public int NC_Id { get; set; }
        public int NC_Type { get; set; } = 1;//1=news 2=articles
        public string NC_Title { get; set; }
        public int NC_SortField { get; set; } = 0;
        public string NC_Seo { get; set; }
        public string NC_SeoUrl { get; set; }
        public string NC_Image { get; set; } = "noimage.png";
        public bool NC_Status { get; set; } = true;
        public bool NC_IsDeleted { get; set; } = false;
        public string Lang { get; set; } = "fa";
    }
}
