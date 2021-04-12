using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Barayand.OutModels.Miscellaneous
{
    public class IndexSectionModel
    {
        public string image { get; set; }
        public string title { get; set; }
        public string link { get; set; }
        public string description { get; set; }
        public bool status { get; set; } = false;
    }
}
