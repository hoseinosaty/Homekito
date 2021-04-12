using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Barayand.OutModels.Miscellaneous
{
    public class IncomeProdFieldSetModel
    {
        public int id { get; set; } = 0;
        public string title { get; set; }
        public dynamic value { get; set; }
        public int type { get; set; } = 1;//1 = checkbox , 2 = text
        public int productid { get; set; } = 0;
        public object Answers { get; set; } = new { };
    }
}
