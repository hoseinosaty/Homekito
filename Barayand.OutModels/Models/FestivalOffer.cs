using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Barayand.OutModels.Models
{
    public class FestivalOffer
    {
        public int F_Id { get; set; }
        public string F_Title { get; set; }
        public decimal F_Discount { get; set; } = 0;
        public int F_Type { get; set; } = 1;//1=>Apply to all products 2=>Apply to all products by group
        public int F_EndLevelCategoryId { get; set; } = 0;//if F_Type == 1
    }
}
