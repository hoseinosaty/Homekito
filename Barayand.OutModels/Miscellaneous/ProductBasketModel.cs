using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Barayand.OutModels.Miscellaneous
{
    public class ProductBasketModel
    {
        public int P_Id { get; set; }
        public string P_Code { get; set; }
        public string P_Title { get; set; }
        public string P_Image { get; set; } = "noimage.png";
        public int P_Type { get; set; } = 1;
        /**********Main Price Data***************/
        public decimal P_Price { get; set; } = 0;
        public int P_PriceType { get; set; } = 1;//if type =>1 user was pay by rials 2=>user is out of iran and pay by points(points calculated by devide final price by 1000)
        public int P_PriceFormulaId { get; set; } = 0;
        /******************************************/
        /*****************Product Main Discount***********************/
        public decimal P_Discount { get; set; } = 0;
        public int P_DiscountType { get; set; } = 1;//Type of discount 1=>percentage 2=>price after reduced percentage from main price enter user
        /**************************************************/
        public decimal P_ExternalPrice { get; set; } = 0;//price of product in foreign websites such as Amazon - price is dollar base
        public decimal P_Weight { get; set; } = 0;
        public decimal P_PageCount { get; set; } = 0;
        public int P_GiftBin { get; set; } = 0;//gift of buy by user
        public int P_BinPrice { get; set; } = 0;//user bin required for buy by bin

        /*************Print Able Price Data****************/
        public bool P_PrintAbleVersion { get; set; } = false;
        public decimal P_PrintAbleVerPrice { get; set; } = 0;
        public int P_PrintAbleVerPriceType { get; set; } = 0;//0=Is Rials 1=>Calculate By Formula
        public int P_PrintAbleVerFormulaId { get; set; } = 0;
        /***********************************/
        /*************Product Period Time Price Data****************/
        public int P_DiscountPeriodTime { get; set; } = 0;//after this time price of product will automacillay calculated from P_PeriodDiscountPrice or P_PriodDiscountFormulaId
        public int P_PeriodDiscountPriceType { get; set; } = 0;//0=Is Rials 1=>Calculate By Formula
        public decimal P_PeriodDiscountPrice { get; set; } = 0;
        public int P_PriodDiscountFormulaId { get; set; } = 0;
        public int P_PeriodPrintablePriceType { get; set; } = 0;//0=>rials 1=>formulaid
        public decimal P_PeriodPrintablePrice { get; set; } = 0;
        public int P_PeriodPrintableFomrulaId { get; set; } = 0;
        public dynamic PriceModel { get; set; }
    }
}
