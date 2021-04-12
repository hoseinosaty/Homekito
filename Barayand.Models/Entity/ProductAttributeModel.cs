using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Barayand.Models.Entity
{
    public class ProductAttributeModel
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int X_Id { get; set; }
        public int X_PId { get; set; } = 0;//Product Id
        public int X_AId { get; set; } = 0;//Attribute Id
        public int X_AnswerId { get; set; } = 0;//Answer Id -> if Attribute type == Combobox
        public string X_AnswerTitle { get; set; }//Answer Title -> if Attribute type == TextBox 
        public int X_EntityType { get; set; } = 1;//1=>Products 2=>Trainings
    }
}
