using Barayand.Models.Extra;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Barayand.Models.Entity
{
    public class OfflinRequestModel:BaseModel
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int O_Id { get; set; }
        public int O_User { get; set; }
        public string O_Url { get; set; }
        public string O_Details { get; set; }
        public decimal O_Price { get; set; } = 0;
        public string O_DeliverDate { get; set; }
        public string O_Reason { get; set; }//if state == 4 reason filled by admin
        public int O_Status { get; set; } = 1;//1=>new/pending 2=>waiting payment 3=>paied/downloaded 4=>delined
    }
}
