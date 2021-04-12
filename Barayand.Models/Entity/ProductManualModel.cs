using Barayand.Models.Extra;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Barayand.Models.Entity
{
    public class ProductManualModel : BaseModel
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int M_Id { get; set; }
        public int M_ProductId { get; set; }
        public decimal M_Price { get; set; } = 0;
        public string M_Title { get; set; }
        public string M_FileName { get; set; }
        public bool M_Status { get; set; } = false;
    }
}
