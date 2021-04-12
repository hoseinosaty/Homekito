using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Barayand.OutModels.Miscellaneous
{
    public class PriceModel
    {
        public decimal PdfPrice { get; set; } = 0;
        public string PdfPriceFormated { get; set; } 
        public decimal HcopyPrice { get; set; } = 0;
        public string HcopyPriceFromated { get; set; }
        public string PdfPriceWithOutDiscountStr { get; set; }
        public string HcopyPriceWithOutDiscountStr { get; set; }
        public decimal PdfPriceWithOutDiscount { get; set; }
        public decimal HcopyPriceWithOutDiscount { get; set; }
        public bool Discounted { get; set; } = false;
        public dynamic Discount { get; set; } = 0;
        public int DiscountType { get; set; } = 0;

    }
}
