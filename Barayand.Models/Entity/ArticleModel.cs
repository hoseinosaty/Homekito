
using Barayand.Models.Extra;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Barayand.Models.Entity
{
    public class ArticleModel :BaseModel
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int A_Id { get; set; }
        public int A_CId { get; set; }
        [MaxLength(500)]
        public string A_Title { get; set; }
        [MaxLength(500)]
        public string A_Sup { get; set; }
        public DateTime? A_Date { get; set; }
        public int? A_Sort { get; set; }
        public bool A_Status { get; set; }
        [MaxLength(4000)]
        public string A_SeoSetting { get; set; }
        [MaxLength(1000)]
        public string A_Image { get; set; }
        [MaxLength(1000)]
        public string A_AttachFile { get; set; }
        [MaxLength(2500)]
        public string A_Summary { get; set; }
        [MaxLength(4000)]
        public string A_Content { get; set; }
        [MaxLength(4000)]
        public string A_Media { get; set; }

    }
}