using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Barayand.OutModels.Models
{
    public class Service
    {
        public int S_Id { get; set; }
        public string S_Title { get; set; }
        public string S_Image { get; set; } = "noimage.png";
        public int S_Sort { get; set; } = 1;
        public bool S_Status { get; set; } = true;
        public string S_Seo { get; set; }
        public string S_Content { get; set; }
        public string S_Icon { get; set; } = "noimage.png";
        public string S_ImageGallery { get; set; }
    }
}
