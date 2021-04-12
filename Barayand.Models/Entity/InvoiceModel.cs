using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;
using Barayand.Models.Extra;
namespace Barayand.Models.Entity
{
    public class InvoiceModel : BaseModel
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ID { get; set; }
        public string I_Id { get; set; }
        public int I_UserId { get; set; } = 0;
        public int I_PaymentType { get; set; } = 1;//1=> Online payment 2=> local payment
        public string I_RecipientInfo { get; set; }
        public int I_CopponId { get; set; } = 0;
        public decimal I_CopponDiscount { get; set; } = 0;
        public decimal I_ShippingCost { get; set; } = 0;
        public int I_ShippingMethod { get; set; } = 0;
        public string I_PaymentInfo { get; set; }//returned payment gateway information was saved as JSON format in thid field
        public DateTime I_PaymentDate { get; set; } = DateTime.Now;
        public decimal I_TotalAmount { get; set; } = 0;//Invoice Totla Amount
        public decimal I_TotalProductAmount { get; set; } = 0;//sum cast of products only
        public string I_DeliveryDate { get; set; }
        public int I_Status { get; set; } = 1;//1=>new/pending 2=>paied 3=>delivered
        public int I_BoxWrapperId { get; set; } = 0;//id of gift box wrapper 
        public string I_Reason { get; set; }
        public bool I_RequestedPOS { get; set; } = false;//user requested POS to Pay in Local
    }
}