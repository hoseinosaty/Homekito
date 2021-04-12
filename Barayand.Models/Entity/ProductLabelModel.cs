using Barayand.Models.Extra;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Barayand.Models.Entity
{
    public class ProductLabelModel : BaseModel
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int L_Id { get; set; }
        public string L_Title { get; set; }
        public string L_Logo { get; set; }
        public string L_Seo { get; set; }
        public string L_HexCode { get; set; } = "#ffffffff";
        public bool L_DisplayOnProduct { get; set; } = false;
        public bool L_Status { get; set; } = false;
        public bool L_IsDeleted { get; set; } = false;
        public string Lang { get; set; } = "fa";

    }
}
