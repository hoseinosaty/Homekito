using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Barayand.OutModels.Models
{
    public class PublicForms
    {
        public int Id { get; set; }
        public int F_Type { get; set; } = 1;//1=suggestion 2=Consultation request 3=Investment Request
        public int MsgType { get; set; } = 1;//if F_Type == 1 --> 1=Criticism 2=Proposal 3=Complaint 4=Tracking
        public string User { get; set; }
        public string UserPhone { get; set; }
        public string UserEmail { get; set; }
        public string F_Msg { get; set; }
        public bool Status { get; set; } = false;
        public string Response { get; set; }
        public int MsgTopic { get; set; } = 1;//if F_Type == 2 // this selection show the message is graphic design or motion graphic
        public int MsgSubTopic { get; set; } = 1;
    }
}
