using Barayand.Models.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Barayand.OutModels.Miscellaneous
{
    public class BasketItem
    {
        public ProductBasketModel Product { get; set; } = new ProductBasketModel();
        public int ProductId { get; set; } = 0;
        public bool PrintAble { get; set; } = false;
        public int Quantity { get; set; } = 1;
        public string CopponCode { get; set; }
        public ReciptientInfoModel ReciptientInfo { get; set; }
    }
}
