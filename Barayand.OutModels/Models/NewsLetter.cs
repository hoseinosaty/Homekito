using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Barayand.OutModels.Models
{
    public class NewsLetter
    {
        public int Id { get; set; }
        public string Entity { get; set; }
        public int Type { get; set; } = 1;//0 => phone number 1=>Email ...
    }
}
