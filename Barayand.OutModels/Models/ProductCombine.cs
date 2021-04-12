using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Barayand.OutModels.Models
{
    public class ProductCombine
    {
        public int X_Id { get; set; }
        public int X_ProductId { get; set; } = 0;
        public int X_WarrantyId { get; set; } = 0;
        public int X_ColorId { get; set; } = 0;
        public int X_Sort { get; set; } = 1;
        public int X_AvailableCount { get; set; } = 0;
        public decimal X_Price { get; set; } = 0;
        public decimal X_Discount { get; set; } = 0;
        public int X_DiscountType { get; set; } = 1;//1=>Percentage 2=>(Price - Discount)
        public bool X_Default { get; set; } = false;
        public bool X_Status { get; set; } = true;
        public bool X_IsDeleted { get; set; } = false;
    }
}
