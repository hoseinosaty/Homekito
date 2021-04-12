using Barayand.Models.Extra;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Barayand.Models.Entity
{
    public class CatAttrRelationModel : BaseModel
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int X_Id { get; set; }
        public int X_CatId { get; set; }
        public int X_AttrId { get; set; }
        public bool X_Status { get; set; } = true;
        public bool X_IsDeleted { get; set; } = false;

        [NotMapped]
        public int[] X_AnswersIds { get; set; } = new int[] { };
        [NotMapped]
        public int X_Type { get; set; } = 1;//1=>ComboBox 2=>TextBox
        [NotMapped]
        public string X_Answers { get; set; }

    }
}
