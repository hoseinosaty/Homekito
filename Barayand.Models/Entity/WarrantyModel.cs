using Barayand.Models.Extra;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Barayand.Models.Entity
{
    public class WarrantyModel:BaseModel
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int W_Id { get; set; }
        public string W_Title { get; set; }
        public string W_Logo { get; set; }
        public int W_SortField { get; set; } = 0;
        public string W_Seo { get; set; }
        public bool W_Status { get; set; } = false;
        public bool W_IsDeleted { get; set; } = false;
    }
}
