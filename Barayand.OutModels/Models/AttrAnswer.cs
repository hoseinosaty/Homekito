using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Barayand.OutModels.Models
{
    public class AttrAnswer
    {
        public int X_Id { get; set; }
        public int X_CatAttrId { get; set; }//CatAttrRelation X_Id
        public string X_Answer { get; set; }
        public bool X_Status { get; set; } = true;
        public bool X_IsDeleted { get; set; } = false;

        public int[] X_AnswersIds { get; set; } = new int[] { };
    }
}
