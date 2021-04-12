
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;
using Barayand.Models.Extra;
namespace Barayand.Models.Entity
{
    public class ProductCategoryModel :BaseModel
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int PC_Id { get; set; }
        public int PC_ParentId { get; set; } = 0;
        [Required]
        [MaxLength(1000)]
        public string PC_Title { get; set; }
        public string PC_ImgTitle { get; set; }
        public string PC_PrefixCode { get; set; }
        public string PC_Description { get; set; }//safe buy guid
        public int PC_OrderField { get; set; } = 0;
        public bool PC_Status { get; set; } = false;
        public bool PC_IsDeleted { get; set; } = false;
        public string PC_Logo { get; set; }
        public string PC_Seo { get; set; }
        public int PC_Type { get; set; } = 1;//1=>Physical 2=>Digital
        public string Lang { get; set; } = "fa";
        [NotMapped]
        public string PC_ParentTitle { get; set; }
        [NotMapped]
        public int PC_Level { get; set; } = 1;
        [NotMapped]
        public bool PC_AttrAble { get; set; } = false;

    }
}