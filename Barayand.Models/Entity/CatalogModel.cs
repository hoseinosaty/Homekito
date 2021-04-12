using Barayand.Models.Extra;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;


namespace Barayand.Models.Entity
{
    public class CatalogModel : BaseModel
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int C_Id { get; set; }
        public int C_CatId { get; set; } = 0;
        [NotMapped]
        public string C_CatTitle { get; set; }
        public string C_Title { get; set; }
        public int C_SortField { get; set; } = 1;
        public bool C_Status { get; set; } = true;
        public string Lang { get; set; } = "fa";
        public string C_ImageUrl { get; set; } = "noimage.png";
        public string C_FileUrl { get; set; } = "noimage.png";
    }
}
