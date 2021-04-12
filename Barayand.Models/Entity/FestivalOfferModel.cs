using Barayand.Models.Extra;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Barayand.Models.Entity
{
    public class FestivalOfferModel:BaseModel
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int F_Id { get; set; }
        public string F_Title { get; set; }
        public decimal F_Discount { get; set; } = 0;
        public int F_Type { get; set; } = 1;//1=>Apply to all products 2=>Apply to all products by group
        public int F_EndLevelCategoryId { get; set; } = 0;//if F_Type == 1
        public bool F_IsDeleted { get; set; } = false;
        public bool F_Status { get; set; } = true;

    }
}
