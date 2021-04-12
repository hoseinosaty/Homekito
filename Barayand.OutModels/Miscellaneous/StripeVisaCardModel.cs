using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Barayand.OutModels.Miscellaneous
{
    public class StripeVisaCardModel
    {
        public string OwnerName { get; set; }
        public string OwnerFamily { get; set; }
        public string CardNumber { get; set; }
        public long ExpirationYear { get; set; }
        public long ExpirationMonth { get; set; }
        public string Cvv2 { get; set; }
    }
}
