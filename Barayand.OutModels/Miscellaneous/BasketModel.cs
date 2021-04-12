using Barayand.Models.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Barayand.OutModels.Miscellaneous
{
    public class BasketModel
    {
        public List<BasketItem> CartItems { get; set; } = new List<BasketItem>();
        public List<CopponModel> Coppon { get; set; } = new List<CopponModel>();
        public ReciptientInfoModel RecipientInfo { get; set; } 
        public decimal ShippingCost { get; set; } = 0m;
        public DateTime OrderDate { get; set; } = DateTime.Now;
        public decimal BasketTotalProductPrice(string lang = "fa",int cvrt = 1000)
        {
            decimal totalPrice = 0;
            foreach (var item in CartItems)
            {
                totalPrice += (item.PrintAble == false) ? (((PriceModel)item.Product.PriceModel).PdfPrice * item.Quantity) : (((PriceModel)item.Product.PriceModel).HcopyPrice * item.Quantity);
            }
            return (lang == "fa")? totalPrice : totalPrice / cvrt;
        }
        public decimal BasketProductPrintablePrice(int pid , string lang = "fa",bool withDiscount = true,bool containQuantity = true,int cvrt = 1000)
        {
            var product = CartItems.FirstOrDefault(x=>x.Product.P_Id == pid && x.PrintAble);
            if(product == null)
            {
                return 0;
            }
            int quantity = (containQuantity) ? product.Quantity : 1;
            decimal result = 0;
            if(withDiscount)
            {
                result = (((PriceModel)product.Product.PriceModel).HcopyPrice * quantity);
            }
            else
            {
                result = (((PriceModel)product.Product.PriceModel).HcopyPriceWithOutDiscount) * quantity;
            }
            return (lang == "fa")? result : result / cvrt;
        }
        public decimal BasketProductPrice(int pid, string lang = "fa", bool withDiscount = true, bool containQuantity = true,int cvrt = 1000)
        {
            var product = CartItems.FirstOrDefault(x => x.Product.P_Id == pid && x.PrintAble == false);
            if (product == null)
            {
                return 0;
            }
            int quantity = (containQuantity) ? product.Quantity : 1;
            decimal result = 0;
            if (withDiscount)
            {
                result = (((PriceModel)product.Product.PriceModel).PdfPrice * quantity);
            }
            else
            {
                result = (((PriceModel)product.Product.PriceModel).PdfPriceWithOutDiscount) * quantity;
            }
            return (lang == "fa") ? result : result / cvrt;
        }
        public int BasketProductBinPriceTotal(int binperc = 1)
        {
            try
            {
                int sum = (int)BasketTotalAmount();
                int result = (sum * int.Parse(binperc.ToString()) / 100);
                return result;
            }
            catch(Exception ex)
            {
                return 0;
            }
        }
        public decimal BasketTotalAmount(string lang = "fa",int cvrt = 1000)
        {
            decimal totalPrice = 0;
            foreach (var item in CartItems)
            {
                if(item.PrintAble)
                {
                    totalPrice += BasketProductPrintablePrice(item.Product.P_Id,lang,true,true,cvrt);
                }
                else
                {
                    totalPrice += BasketProductPrice(item.Product.P_Id, lang, true, true,cvrt);
                }
            }
            if (Coppon != null)
            {
                totalPrice = totalPrice - ((totalPrice * SumDiscount()) / 100);
            }
            totalPrice += ShippingCost;
           
            return totalPrice;
        }
        public decimal SumDiscount(string lang = "fa",int cvrt = 1000)
        {
            return (lang == "fa")? Coppon.Sum(x => x.CP_Discount) : Coppon.Sum(x => x.CP_Discount) / cvrt;
        }
        public int TotalQuantity()
        {
            return CartItems.Sum(x=>x.Quantity);
        }
    }
}
