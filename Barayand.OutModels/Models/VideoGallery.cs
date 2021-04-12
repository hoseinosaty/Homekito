using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Barayand.OutModels.Models
{
    public class VideoGallery
    {
        public int VG_Id { get; set; }
        public int VG_CatId { get; set; }
        public string VG_Title { get; set; }
        public bool VG_Status { get; set; } = true;
        public int VG_SortField { get; set; } = 0;
        public int VG_UrlType { get; set; } = 0;//0->Local file and uploaded by user  1->an url from another host
        public string VG_VideoUrl { get; set; }
        public string VG_VideoImage { get; set; }
        public string VG_Keywords { get; set; }
        public string VG_Seo { get; set; }
        public string VG_CatTitle { get; set; }
        public string VG_Description { get; set; }
        public bool VG_IsDeleted { get; set; } = false;
        public string Lang { get; set; } = "fa";
    }
}
