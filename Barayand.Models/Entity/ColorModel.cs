using Barayand.Models.Extra;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Barayand.Models.Entity
{
    public class ColorModel:BaseModel
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int C_Id { get; set; }
        public string C_Title { get; set; }
        public string C_HexColor { get; set; } = "#ffffff";
        public bool C_IsDeleted { get; set; } = false;
        public bool C_Status { get; set; } = false;

    }
}
