using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;
using Barayand.Models.Extra;
namespace Barayand.Models.Entity
{
    public class TraderWIthDrawHistoryModel:BaseModel
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int TWD_Id { get; set; }
        public int TWD_User { get; set; }
        public int TWD_Type { get; set; }//1=>bank pay 2=>coin pay
        public int TWD_SourceCurrency { get; set; }
        public decimal TWD_Amount { get; set; }
        public decimal TWD_Price { get; set; }
        public string TWD_IBAN { get; set; } = null;//Bank account Sheba Number
        public string TWD_BankName { get; set; } = null;//Bank Branch Name
        public string TWD_PublicKey { get; set; } = null;//if user want to cashe out to crypto currency user enter self public key to transfer
        public int TWD_RequestState { get; set; } = 0;//0=registerd 1=>pending 2=>accepted 3=>failed 4=>transfered
        public string TWD_Message { get; set; } = null;//if request failed admin write an reason
        public DateTime? TWD_AdminAccept { get; set; }//the admin operation date time
        public DateTime? TWD_RequestDate { get; set; }
    }
}