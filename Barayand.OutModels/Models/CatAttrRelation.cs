using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Barayand.OutModels.Models
{
    public class CatAttrRelation
    {
        public int X_Id { get; set; }
        public int X_CatId { get; set; }
        public int X_AttrId { get; set; }
        public string X_Answers { get; set; }
        public int X_Type { get; set; } = 1;//1=>ComboBox 2=>TextBox
        public bool X_Status { get; set; } = true;
        public bool X_IsDeleted { get; set; } = false;
        public int[] X_AnswersIds { get; set; } = new int[] { };
    }
}
