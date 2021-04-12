using Barayand.Models.Extra;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Barayand.Models.Entity
{
    public class AttrAnswerModel:BaseModel
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int X_Id { get; set; }
        public int X_CatAttrId { get; set; }//CatAttrRelation X_Id
        public string X_Answer { get; set; }
        public int X_Sort { get; set; } = 0;
        public bool X_Status { get; set; } = true;
        public bool X_IsDeleted { get; set; } = false;

        [NotMapped]
        public int[] X_AnswersIds { get; set; } = new int[] { };

    }
}
