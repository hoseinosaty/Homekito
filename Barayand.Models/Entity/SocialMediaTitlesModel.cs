using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;
using Barayand.Models.Extra;
namespace Barayand.Models.Entity
{
    public class SocialMediaTitlesModel:BaseModel
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int SM_Id { get; set; }
        [Required]
        [MaxLength(500)]
        public string SM_Title { get; set; }
        [Required]
        [MaxLength(1000)]
        public string SM_Url { get; set; }
        public string SM_Icon { get; set; }

    }
}