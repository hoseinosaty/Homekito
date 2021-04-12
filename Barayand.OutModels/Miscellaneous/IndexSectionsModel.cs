using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Barayand.OutModels.Miscellaneous
{
    public class IndexSectionsModel
    {
        public int Id { get; set; } = 1;
        public string Title { get; set; }
        public string Icon { get; set; }
        public int Sort { get; set; } = 1;
        public int Section { get; set; } = 1;
        public List<int> Products { get; set; } = new List<int>();
        public string Lang { get; set; } = "fa";
    }
}
