using Barayand.Models.Extra;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Barayand.Models.Entity
{
    public class IndexBoxProductRelModel : BaseModel
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int I_Id { get; set; }
        public int I_SecId { get; set; }
        public int I_Pid { get; set; }
        public string Lang { get; set; } = "fa";
    }
}
