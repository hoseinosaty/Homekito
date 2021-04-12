using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Barayand.Models.RuntimeModels
{
    public class ProductCombinePriceModel
    {
        public decimal Price { get; set; } = 0;
        public decimal DiscountedPrice { get; set; } = 0;
        public decimal Discount { get; set; } = 0;
        public bool HasDiscount { get; set; } = false;
        public string Timer { get; set; } = null;
        public int Avl { get; set; } = 0;
    }
}
