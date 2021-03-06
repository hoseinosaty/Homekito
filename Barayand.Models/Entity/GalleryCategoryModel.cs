
using Barayand.Models.Extra;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Barayand.Models.Entity
{
    public class GalleryCategoryModel : BaseModel
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int GC_Id { get; set; }
        public string GC_Titles { get; set; }
        public int GC_SortField { get; set; } = 0;
        public string GC_Seo { get; set; }
        public string GC_Icon { get; set; }
        public string GC_Description { get; set; }
        public bool GC_Status { get; set; } = true;
        public bool GC_IsDeleted { get; set; } = false;
        public int GC_Type { get; set; } = 0;//image = 1 , video = 2 , UNKNOWN = 0
        public string Lang { get; set; } = "fa";
    }
}