using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Barayand.OutModels.Miscellaneous
{
    public class ReciptientInfoModel
    {
        public string Name { get; set; }
        public string Phone { get; set; }
        public int Province { get; set; } = 0;
        public int State { get; set; } = 0;
        public string Address { get; set; }
        public int AddressId { get; set; } = 0;
        public string Tel { get; set; }
        public string ZipCode { get; set; }
        public string Description { get; set; }
        public string DeliveryDate { get; set; }//format yyyy/MM/dd HH:mm:ss
    }
}
