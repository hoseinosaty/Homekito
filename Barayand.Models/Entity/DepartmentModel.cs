using Barayand.Models.Extra;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Barayand.Models.Entity
{
    public class DepartmentModel:BaseModel
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int D_Id { get; set; }
        public string D_Title { get; set; }
        public int D_SortField { get; set; } = 1;
        public string D_Email { get; set; }
        public string D_Tel { get; set; }
        public bool D_Status { get; set; } = true;
        public string Lang { get; set; } = "fa";
    }
}
