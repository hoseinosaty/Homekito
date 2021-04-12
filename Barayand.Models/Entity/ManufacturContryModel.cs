using Barayand.Models.Extra;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Barayand.Models.Entity
{
    public class ManufacturContryModel:BaseModel
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int M_Id { get; set; }
        public string M_Title { get; set; }
        public int M_Sort { get; set; } = 1;
        public bool M_Status { get; set; } = true;
        public bool M_IsDeleted { get; set; } = false;
    }
}
