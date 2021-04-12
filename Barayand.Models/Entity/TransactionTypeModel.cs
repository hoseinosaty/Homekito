using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;
using Barayand.Models.Extra;
namespace Barayand.Models.Entity
{
    public class TransactionTypeModel:BaseModel
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int TT_Id { get; set; }
        public int? TT_CashId { get; set; }
        public int? TT_ArrivalId { get; set; }
        public int TT_Type { get; set; } = 1;//1=exchange 2= trader
        public decimal TT_MinAmount { get; set; }//min amount to whitdraw if TT_Type == 2
       
    }
}