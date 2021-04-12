using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Barayand.OutModels.Miscellaneous
{
    public class FestivalCreationModel
    {
        public int[] Categories { get; set; } = new int[] { };
        public string Title { get; set; }
        public decimal Discount { get; set; } = 0;
        public int Type { get; set; } = 1;//1=>Apply to all products 2=>Apply to all products by group
    }
}
