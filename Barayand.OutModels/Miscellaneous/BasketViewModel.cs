using Barayand.Models.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Barayand.OutModels.Miscellaneous
{
    public class BasketViewModel
    {
        public List<ProductList> Products { get; set; } = new List<ProductList>();
        public ReciptientInfoModel ReciptientInfo { get; set; } = new ReciptientInfoModel();
        public Coupon CouponInfo { get; set; } = null;
        public decimal Total { get; set; } = 0;
    }
    public class ProductList
    {
        public int ProductCombineId { get; set; } = 0;
        public string ProductImage { get; set; }
        public string ProductTitle { get; set; }
        public string WarrantyTitle { get; set; }
        public string ColorTitle { get; set; }
        public decimal Price { get; set; } = 0;
        public decimal DiscountedPrice { get; set; } = 0;
        public int Quantity { get; set; } = 1;
        public decimal Total { get; set; } = 0;
        public bool IsAvailable { get; set; } = true;
        public ProductModel GiftProduct { get; set; } = null;
    }
    public class Coupon
    {
        public string CouponId { get; set; }
        public decimal CouponDiscount { get; set; } = 0;
        public decimal CouponAmount { get; set; } = 0;
    }
}