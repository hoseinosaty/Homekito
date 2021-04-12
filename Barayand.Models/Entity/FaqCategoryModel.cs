using Barayand.Models.Extra;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Barayand.Models.Entity
{
    public class FaqCategoryModel:BaseModel
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int F_Id { get; set; }
        public string F_Title { get; set; }
        public int F_SortField { get; set; } = 1;
        public string F_Logo { get; set; }
        public bool F_Status { get; set; } = true;
        public bool F_IsDeleted { get; set; } = false;
    }
}
