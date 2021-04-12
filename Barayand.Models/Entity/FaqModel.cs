using Barayand.Models.Extra;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Barayand.Models.Entity
{
    public class FaqModel:BaseModel
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int FA_Id { get; set; }
        public int FA_CatId { get; set; } = 0;
        [NotMapped]
        public string FA_CatTitle { get; set; }
        public string FA_Title { get; set; }
        public int FA_SortField { get; set; } = 1;
        public bool FA_Status { get; set; } = true;
        public string FA_Answer { get; set; }
        public bool FA_IsDeleted { get; set; } = false;

    }
}
