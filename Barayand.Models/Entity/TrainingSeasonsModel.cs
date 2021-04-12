using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;
using Barayand.Models.Extra;

namespace Barayand.Models.Entity
{
    public class TrainingSeasonsModel:BaseModel
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int S_Id { get; set; }
        public int S_TId { get; set; }
        public string S_Title { get; set; }
        public string S_Description { get; set; }
        public string S_Time { get; set; }
        public decimal S_Cost { get; set; } = 0;
        public int S_Sort { get; set; } = 1;
        public string S_VideoUrl { get; set; }
        [NotMapped]
        public bool AllowDownload { get; set; } = false;
    }
}
