using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Barayand.OutModels.Miscellaneous
{
    public class SearchProductModel
    {
        public int[] Categories { get; set; } = new int[] { };
        public string[] PublishedYear { get; set; } = new string[] { };
        public int[] Publisher { get; set; } = new int[] { };
        public int[] FiterTags { get; set; } = new int[] { };
        public List<AttributeAnswer> CategoryAttributes { get; set; } = null;
        public bool ExcludeStock { get; set; } = false;//Show only available products
        public int Page { get; set; } = 1;
    }
    public class AttributeAnswer
    {
        public int attribute { get; set; }
        public int answer { get; set; }
    }
}
