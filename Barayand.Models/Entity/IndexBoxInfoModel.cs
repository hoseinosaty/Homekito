using Barayand.Models.Extra;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Barayand.Models.Entity
{
    public class IndexBoxInfoModel : BaseModel
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int I_Id { get; set; }
        public int I_SecId { get; set; }
        public string I_Title { get; set; }
        public string I_Icon { get; set; }
        public int I_Sort { get; set; } = 1;
        public string Lang { get; set; } = "fa";
    }
}
