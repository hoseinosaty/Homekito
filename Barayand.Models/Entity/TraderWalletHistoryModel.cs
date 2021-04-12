using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;
using Barayand.Models.Extra;
namespace Barayand.Models.Entity
{
    public class TraderWalletHistoryModel:BaseModel
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int TWH_id { get; set; }
        public int TWH_CustomerId { get; set; }
        public int TWH_AdderId { get; set; } = 0;//0 mean user her-charched self wallet or if charged by other user , user id saved in this field
        public int TWH_TransactionType { get; set; } = 1;//the transaction type 1 is Deposit and 2 is withdraw
        public int TWH_DPId { get; set; }//Digital Product Id (id of Currency)
        public decimal TWH_Amount { get; set; }
        public int TWH_PayType { get; set; }//the type of payment 1=Bank gateway 2=CryptoCurrency Schema
        [MaxLength(4000)]
        public string TWH_TrackingCode { get; set; } = null;//Tracking or Schema code of Payment
        public int TWH_AdminAccept { get; set; } = 0;//0=>No Approval required -> for Rials only 1=>Pending 2=>Accepted 3=>Failed
        public string TWH_AdminReason { get; set; } = null;//if TWH_AdminAccept is failed admin write an reason
        public DateTime? TWH_ReviewDate { get; set; }//if currency type not equal to Rials. Date of accept or Fail 
        public string TWH_PayDate { get; set; }
    }
}