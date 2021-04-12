using Barayand.Models.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Barayand.OutModels.Miscellaneous
{
    public class InvoiceEmailFormat
    {
        public string id { get; set; }
        public string date { get; set; }
        public string total { get; set; }
        public string customerEmail { get; set; }
        public List<BasketItem> Products { get; set; }

    }
}
