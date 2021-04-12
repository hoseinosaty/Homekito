using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Barayand.OutModels.Miscellaneous
{
    public class OfflineRequestChangeState
    {
        public int id { get; set; }
        public int state { get; set; }
        public decimal price { get; set; } = 0;
        public string deliver { get; set; }
        public string reason { get; set; }
    }
}
