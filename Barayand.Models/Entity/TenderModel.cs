using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;
using Barayand.Models.Extra;
namespace Barayand.Models.Entity
{
    public class TenderModel:BaseModel
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int T_Id { get; set; }
        public int T_SellerUser { get; set; }
        public double T_SellerCal { get; set; } = 0;
        public double T_BuyerCal { get; set; } = 0;
        public int T_BuyerUser { get; set; }
        public int T_Status { get; set; } = 1;//1=new/waiting 2=tendered
        public int T_SourceCurrency { get; set; }
        public decimal T_Amount { get; set; }
        public decimal T_Price { get; set; }
        public int T_Type { get; set; } = 1;//1 => sale 2=>buy
        public DateTime? T_RequestTime { get; set; }
        public DateTime? T_TransactionTime { get; set; }
        
    }
}