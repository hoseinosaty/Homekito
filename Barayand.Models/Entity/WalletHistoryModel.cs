using Barayand.Models.Extra;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Barayand.Models.Entity
{
    public class WalletHistoryModel: BaseModel
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int WH_id { get; set; }
        public string WH_SessionId { get; set; }
        public int WH_CustomerId { get; set; }
        public int WH_AdderId { get; set; } = 0;//0 mean user her-charched self wallet or if charged by other user , user id saved in this field
        public int WH_TransactionType { get; set; } = 1;//the transaction type 1 is Deposit and 2 is withdraw
        public decimal WH_Amount { get; set; }
        public int WH_PayType { get; set; }//1=>stripe payment
        public string WH_PayBankRecip { get; set; }
    }
}