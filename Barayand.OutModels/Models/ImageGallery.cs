using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Barayand.OutModels.Models
{
    public class ImageGallery
    {
        public int IG_Id { get; set; }
        public int IG_CatId { get; set; } = 0;
        public string IG_CatTitle { get; set; }
        public string IG_Title { get; set; }
        public int IG_SortField { get; set; } = 0;
        public bool IG_Status { get; set; } = false;
        public string IG_ImageUrl { get; set; }
        public string Lang { get; set; }
    }
}
