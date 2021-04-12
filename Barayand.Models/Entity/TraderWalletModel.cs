using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;
using Barayand.Models.Extra;
namespace Barayand.Models.Entity
{
    public class TraderWalletModel :BaseModel
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int TW_Id { get; set; }
        public int TW_CsmId { get; set; }
        public int TW_DPId { get; set; }
        public double TW_Amount { get; set; }
    }
}