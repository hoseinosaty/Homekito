using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;
using Barayand.Models.Extra;
namespace Barayand.Models.Entity
{
    public class VideoGalleryModel:BaseModel
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int VG_Id { get; set; }
        public int VG_CatId { get; set; }
        [Required]
        [MaxLength(500)]
        public string VG_Title { get; set; }
        public bool VG_Status { get; set; } = true;
        public int VG_SortField { get; set; } = 0;
        public int VG_UrlType { get; set; } = 0;//0->Local file and uploaded by user  1->an url from another host
        [MaxLength(4000)]
        public string VG_VideoUrl { get; set; }
        [MaxLength(4000)]
        public string VG_VideoImage { get; set; }
        [MaxLength(1000)]
        public string VG_Keywords { get; set; }
        [MaxLength(4000)]
        public string VG_Seo { get; set; }
        public string VG_Description { get; set; }
        public string Lang { get; set; } = "fa";
        public bool VG_IsDeleted { get; set; } = false;
        [NotMapped]
        public string VG_CatTitle { get; set; }
        [NotMapped]
        public int Rate { get; set; }
        [NotMapped]
        public List<CommentModel> Comments { get; set; } = null;
    }
}