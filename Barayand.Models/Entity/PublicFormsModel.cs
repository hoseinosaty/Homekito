using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;
using Barayand.Models.Extra;
namespace Barayand.Models.Entity
{
    public class PublicFormsModel :BaseModel
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int F_Id { get; set; }
        public int F_Type { get; set; } = 1;//1=suggestion 2=Consultation request 3=Investment Request
        public int F_MsgType { get; set; } = 1;//if F_Type == 1 --> 1=Criticism 2=Proposal 3=Complaint 4=Tracking
        public int F_MsgTopic { get; set; } = 1;//if F_Type == 2 // this selection show the message is graphic design or motion graphic
        public int F_MsgSubTopic { get; set; } = 1;
        [MaxLength(500)]
        public string F_User { get; set; }
        [MaxLength(500)]
        public string F_UserPhone { get; set; }
        [MaxLength(500)]
        public string F_UserEmail { get; set; }
        [MaxLength(4000)]
        public string F_Msg { get; set; }
        public bool F_Status { get; set; } = false;
        [MaxLength(4000)]
        public string F_Response { get; set; }
       
    }
}