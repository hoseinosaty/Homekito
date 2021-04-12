using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Barayand.OutModels.Models
{
    public class Order
    {
        public int O_Id { get; set; }
        public string O_ReciptId { get; set; }
        public int O_ProductId { get; set; } = 0;
        public decimal O_Price { get; set; } = 0;
        public decimal O_Discount { get; set; } = 0;
        public int O_DiscountType { get; set; } = 1;// 1=> percentage 2=>price after reduce discount
        public int O_Quantity { get; set; }
        public int O_Version { get; set; }//if 1=> ordered product is pdf version 2=>ordered product is Hard copy
        public string Lang { get; set; }
    }
}
