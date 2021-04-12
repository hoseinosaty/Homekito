using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Barayand.OutModels.Miscellaneous
{
    public class FavoriteList
    {
        public int Id { get; set; }
        public string Title{get;set;}
        public string Image { get; set; }
        public string Price { get; set; }
        public string Url { get; set; }
        public int Type { get; set; }
    }
}
