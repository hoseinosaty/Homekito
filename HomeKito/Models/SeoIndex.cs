using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HomeKito.Models
{
    public class SeoIndex
    {
        public string icon { get; set; }
        public string phone { get; set; }
        public string headerphone { get; set; }
        public string email { get; set; }

        public string address { get; set; }
        public string serviceBoxTitle { get; set; }
        public Newsletter Newsletter { get; set; }
    }
}
