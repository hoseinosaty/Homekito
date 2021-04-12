using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;
using Barayand.Models.Extra;
namespace Barayand.Models.Entity
{
    public class SocialMediaModel:BaseModel
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int SMU_Id { get; set; }
        public int SMU_CatId { get; set; }
        [MaxLength(500)]
        public string SMU_Title { get; set; }
        [MaxLength(500)]
        public string SMU_UId { get; set; }
    }
}