using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;
using Barayand.Models.Extra;
namespace Barayand.Models.Entity
{
    public class SliderModel :BaseModel
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int S_Id { get; set; }

        public string S_Titles { get; set; }
        public int S_OrderField { get; set; } = 1;
        [MaxLength(500)]
        public string S_Link { get; set; }
        [MaxLength(500)]
        public string S_AltTag { get; set; }
        [MaxLength(500)]
        public string S_TooltipTitle { get; set; }
        [MaxLength(500)]
        public string S_DesktopFileLink { get; set; }
        
        [MaxLength(500)]
        public string S_MobileFileLink { get; set; }

        public bool S_Status { get; set; } = true;
        public bool S_ShowAnimation { get; set; } = true;
        public string Lang { get; set; }

    }
}