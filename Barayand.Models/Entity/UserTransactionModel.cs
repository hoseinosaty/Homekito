using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;
using Barayand.Models.Extra;
namespace Barayand.Models.Entity
{
    public class UserTransactionModel:BaseModel
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int UT_Id { get; set; }
        public int UT_Uid { get; set; }
        public decimal UT_Amount { get; set; }
        public int UT_Type { get; set; }//1-> Buy 2-> Sale 3=>Trader(Convert)
        public int UT_Entity { get; set; }//id of sale/buy product
    }
}