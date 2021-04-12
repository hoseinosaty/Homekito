using Barayand.Models.Extra;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Barayand.Models.Entity
{
    public class ExWithDrawHistoryModel: BaseModel
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int WD_Id { get; set; }
        public int WD_User { get; set; }
        public int WD_Type { get; set; }//1=>bank pay 2=>coin pay
        public bool WD_ToIrr { get; set; } = false;//if true user want to convert to rials and widthrow as IRR
        public int WD_SourceCurrency { get; set; }
        public decimal WD_Amount { get; set; }
        public decimal WD_Price { get; set; }
        public string WD_IBAN { get; set; } = null;//Bank account Sheba Number
        public string WD_BankName { get; set; } = null;//Bank Branch Name
        public string WD_PublicKey { get; set; } = null;//if user want to cashe out to crypto currency user enter self public key to transfer
        public int WD_RequestState { get; set; } = 1;// 1=>pending 2=>accepted(transfered) 3=>failed
        public string WD_Message { get; set; } = null;//if request failed admin write an reason
        public DateTime? WD_AdminAccept { get; set; }//the admin operation date time
        public DateTime? WD_RequestDate { get; set; }


    }
}