using Barayand.Models.Extra;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Barayand.Models.Entity
{
    public class ExpertReviewModel:BaseModel
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int E_Id{ get; set; }
        public int E_ProductId { get; set; }
        public string E_Title { get; set; }
        public string E_Description { get; set; }
        public int E_Sort { get; set; } = 1;
        public string E_Image { get; set; } = "noimage.png";
        public bool E_Status { get; set; } = true;
        public bool E_IsDeleted { get; set; } = false;
    }
}
