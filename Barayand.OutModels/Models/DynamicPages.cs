using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Barayand.OutModels.Models
{
    public class DynamicPages
    {
        public int D_Id { get; set; }
        public string PageName { get; set; }
        public string PageContent { get; set; }
        public string PageId { get; set; }
        public string PageSeo { get; set; }
        public string PageOtherSetting { get; set; }
        public string Lang { get; set; } = "fa";
    }
}
