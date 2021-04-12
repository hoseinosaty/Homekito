using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Barayand.OutModels.Models
{
    public class Catalog
    {
        public int C_Id { get; set; }
        public int C_CatId { get; set; } = 0;
        public string C_CatTitle { get; set; }
        public string C_Title { get; set; }
        public int C_SortField { get; set; } = 1;
        public bool C_Status { get; set; } = true;
        public string Lang { get; set; } = "fa";
        public string C_ImageUrl { get; set; } = "moimage.png";
        public string C_FileUrl { get; set; } = "noimage.png";

    }
}
