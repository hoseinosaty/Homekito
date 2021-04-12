using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Barayand.OutModels.Models
{
    public class Notices
    {
        public int N_Id { get; set; }
        public int N_Type { get; set; } = 1;//1 => News 2 => Article
        public int N_CId { get; set; }
        public string N_Title { get; set; }
        public string N_Sup { get; set; }
        public DateTime N_Date { get; set; } = DateTime.Now;
        public string N_ShamsiDate { get; set; } 
        public int N_Sort { get; set; } = 0;
        public bool N_Status { get; set; } = true;
        public string N_Image { get; set; }
        public string N_Seo { get; set; }
        public string N_Media { get; set; }
        public string N_BannerImage { get; set; } = "noimage.png";
        public int N_IsSlideShow { get; set; } = 1;//1=>normal 2=>type 2 3=>is slider

        public int N_MediaType { get; set; } = 1;//1 => Video 2 => ImageGallery
        public string N_Summary { get; set; }
        public string N_Content { get; set; }
        public bool N_IsDeleted { get; set; } = false;
        public string N_Url { get; set; }
        public string N_Attachment { get; set; }// if file type is Articles this field will be filled as article attachment file
        public int VisitCount { get; set; } = 0;
        public double PopularPerc { get; set; } = 0;
        public int CommentCount { get; set; } = 0;
        public string N_CatTitle { get; set; }
        public string Lang { get; set; } = "fa";
    }
}
