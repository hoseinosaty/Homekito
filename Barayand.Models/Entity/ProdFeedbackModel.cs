using Barayand.Models.Extra;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;


namespace Barayand.Models.Entity
{
    public class ProdFeedbackModel:BaseModel
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int F_Id { get; set; }
        public int F_Topic { get; set; } = 1;
        public string F_DuplicateUrl { get; set; }//if topic == duplicate url , filled this field to duplicated product url
        public int F_ProductId { get; set; } = 0;
        public string F_Description { get; set; }

    }
}
