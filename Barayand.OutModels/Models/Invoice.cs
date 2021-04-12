using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Barayand.OutModels.Models
{
    public class Invoice
    {
        public int ID { get; set; }
        public string I_Id { get; set; }
        public int I_UserId { get; set; } = 0;
        public int I_PaymentType { get; set; } = 1;//1=>Online 2->Wallet 3->Bon 4->Point(only for Non-Persian users)
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
        public int I_Status { get; set; } = 1;//1=>new/pending 2=>accepted 3=>Declined
        public string I_Reason { get; set; }
        public bool I_RequestedPOS { get; set; } = false;//user requested POS to Pay in Local
    }
}
