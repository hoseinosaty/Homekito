using Barayand.Models.Extra;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Barayand.Models.Entity
{
    public class EnergyUsageModel:BaseModel
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int E_Id { get; set; }
        public string E_Title { get; set; }
        public bool E_Status { get; set; } = true;
        public string E_Image { get; set; } = "noimage.png";
        public int E_Sort { get; set; } = 1;
        public int E_Type { get; set; } = 1;//1=>EnergyUsage 2=>BoxGiftWrapper 3=>FooterLogo
        public bool E_IsDeleted { get; set; } = false;
        public string E_Link { get; set; }//if type == FooterLogo filled this field
    }
}
