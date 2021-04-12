using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;
using Barayand.Models.Extra;
namespace Barayand.Models.Entity
{
    public class NoticesCategoryModel :BaseModel
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int NC_Id { get; set; }
        public int NC_Type { get; set; } = 1;//1=news 2=articles
        [Required]
        [MaxLength(500)]
        public string NC_Title { get; set; }
        public int NC_SortField { get; set; } = 0;
        public string NC_Seo { get; set; }
        [MaxLength(1500)]
        public string NC_SeoUrl { get; set; }
        public string NC_Image { get; set; } = "noimage.png";
        public bool NC_Status { get; set; } = true;
        public bool NC_IsDeleted { get; set; } = false;
        public string Lang { get; set; } = "fa";
    }
}