using Barayand.Models.Extra;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Barayand.Models.Entity
{
    public class ImageGalleryModel:BaseModel
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int IG_Id { get; set; }
        public int IG_CatId { get; set; }
        [Required]
        [MaxLength(500)]
        public string IG_Title { get; set; }
        public int IG_SortField { get; set; } = 0;
        public bool IG_Status { get; set; } = false;
        public string Lang { get; set; }
        [Required]
        [MaxLength(1000)]
        public string IG_ImageUrl { get; set; }
        [NotMapped]
        public string IG_CatTitle { get; set; }

    }
}