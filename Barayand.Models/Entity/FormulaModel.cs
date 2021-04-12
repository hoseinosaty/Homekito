using Barayand.Models.Extra;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;
namespace Barayand.Models.Entity
{
    public class FormulaModel:BaseModel
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int F_Id { get; set; }
        public string F_Title { get; set; }
        public string F_Formula { get; set; }
        public bool F_Status { get; set; } = true;
        public bool F_IsDeleted { get; set; } = false;
        public int F_CatId { get; set; } = 1;
    }
}
