using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Barayand.OutModels.Miscellaneous
{
    public class FullPropertyBasketItem
    {
        public int ProductCombineId { get; set; } = 0;//id of combined of Warranty and color and product 
        public int GiftProductCombineId { get; set; } = 0;//id of combined of Warranty and color and product of gifted product
        public int ProductManualId { get; set; } = 0; //0=> product not have an manual

        public int Quantity { get; set; } = 1;//count of order
        public string CopponCode { get; set; }
        public int ProductType { get; set; } = 1;//1 = > product combine 2 => product manual only

    }
}
