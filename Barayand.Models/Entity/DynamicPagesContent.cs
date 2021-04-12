using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;
using Barayand.Models.Extra;
namespace Barayand.Models.Entity
{
    public class DynamicPagesContent :BaseModel
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int D_Id { get; set; }
        [MaxLength(50)]
        public string PageName { get; set; }
        public string PageContent { get; set; }
        public string PageId { get; set; }
        public string PageSeo { get; set; }
        public string PageOtherSetting { get; set; }
        public string Lang { get; set; } = "fa";
    }
}