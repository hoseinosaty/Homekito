using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;
using Barayand.Models.Extra;
namespace Barayand.Models.Entity
{
    public class TicketResponseModel:BaseModel
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int TR_Id{get;set;}
        public int TR_Tid { get; set; }
        public int TR_Userid { get; set; }//responser
        public string TR_Body { get; set; }
    }
}