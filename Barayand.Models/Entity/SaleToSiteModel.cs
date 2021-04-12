using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;
using Barayand.Models.Extra;
namespace Barayand.Models.Entity
{
    public class SaleToSiteModel :BaseModel
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int S_Id { get; set; }
        public int S_UserId { get; set; }
        public int S_DpId { get; set; }
        public int S_PayCoin { get; set; } = 0;//if S_Type == 2 -> 0=IRR or Other Currency ID
        public double S_CoinCount { get; set; }
        public double S_CoinPrice { get; set; }
        public string S_CoinSymbol { get; set; }
        public int S_Status { get; set; }//1=>new 2=>accept 3=>rejected-if S_Type = 1
        public int S_Type { get; set; } = 1;//1=>sale 2=>buy
        public string S_AdminReson { get; set; } //Reason Of Reject Sale Request
    }
}