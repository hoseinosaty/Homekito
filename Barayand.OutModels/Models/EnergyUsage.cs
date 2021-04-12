using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Barayand.OutModels.Models
{
    public class EnergyUsage
    {
        public int E_Id { get; set; }
        public string E_Title { get; set; }
        public bool E_Status { get; set; } = true;
        public string E_Image { get; set; } = "noimage.png";
        public int E_Sort { get; set; } = 1;
        public int E_Type { get; set; } = 1;//1=>EnergyUsage 2=>BoxGiftWrapper 3=>FooterLogo
        public string E_Link { get; set; }//if type == FooterLogo filled this field
    }
}
