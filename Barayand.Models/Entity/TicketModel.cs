using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;
using Barayand.Models.Extra;
namespace Barayand.Models.Entity
{
    public class TicketModel:BaseModel
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int T_Id { get; set; }
        public int T_ParentId { get; set; } = 0;
        public decimal T_Cid { get; set; }
        public int T_Userid { get; set; } = 0;
        public string T_Body { get; set; }
        public int T_Status { get; set; } = 1; /// 1= new  2= responsed
    }
}