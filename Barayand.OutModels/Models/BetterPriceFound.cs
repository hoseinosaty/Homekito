using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Barayand.OutModels.Models
{
    public class BetterPriceFound
    {
        public int B_Id { get; set; }
        public int B_ProductId { get; set; } = 0;
        public string B_ProductTitle { get; set; }
        public string B_Price { get; set; } = "0";
        public string B_StoreWebAddress { get; set; }
        public string B_StoreName { get; set; }
        public string B_StoreOwnCity { get; set; }
        public int B_StoreType { get; set; } = 1;//1=>Website(fill sotre webaddress) 2=>Physical store(fill store name and stor own city)
        public string B_StoreTypeStr
        {
            get
            {
                switch (B_StoreType)
                {
                    case 2:
                        return "فروشگاه فیزیکی";
                    default:
                        return "فروشگاه اینترنتی";
                }
            }
        }
    }
}
