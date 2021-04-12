using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;
using Barayand.Models.Extra;

namespace Barayand.Models.Entity
{
    public class NoticesModel :BaseModel
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int N_Id { get; set; }
        public int N_Type { get; set; } = 1;//1 => News 2 => Article
        public int N_CId { get; set; }
        [MaxLength(500)]
        public string N_Title { get; set; }
        [MaxLength(500)]
        public string N_Sup { get; set; }
        public DateTime N_Date { get; set; } = DateTime.Now;
        public string N_ShamsiDate { get; set; } 
        public int N_Sort { get; set; } = 0;
        public bool N_Status { get; set; } = true;
        [MaxLength(1000)]
        public string N_Image { get; set; }
        [MaxLength(4000)]
        public string N_Seo { get; set; }
        [MaxLength(4000)]
        public string N_Media { get; set; }
        public int N_MediaType { get; set; } = 1;//1 => Video 2 => ImageGallery
        [MaxLength(2500)]
        public string N_Summary { get; set; }
        [MaxLength(4000)]
        public string N_Content { get; set; }
        public string N_BannerImage { get; set; } = "noimage.png";

        public int N_IsSlideShow { get; set; } = 1;//1=>normal 2=>type 2 3=>is slider
        public bool N_IsDeleted { get; set; } = false;
        public string N_Url { get; set; }
        public string N_Attachment { get; set; }// if file type is Articles this field will be filled as article attachment file
        public string Lang { get; set; } = "fa";
        [NotMapped]
        public int VisitCount { get; set; } = 0;
        [NotMapped]
        public double PopularPerc { get; set; } = 0;
        [NotMapped]
        public int CommentCount { get; set; } = 0;
        [NotMapped]
        public string N_CatTitle { get; set; }
        [NotMapped]
        public int Rate { get; set; } = 0;
        [NotMapped]
        public List<CommentModel> Comments { get; set; } = new List<CommentModel>();
        
    }
}