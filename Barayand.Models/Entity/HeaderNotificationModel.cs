using Barayand.Models.Extra;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Barayand.Models.Entity
{
    public class HeaderNotificationModel:BaseModel
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int H_Id { get; set; }
        public string H_Title { get; set; }
        public string H_Link { get; set; }
        public string H_BgColor { get; set; } = "#ffffff";
        public string H_BtnColor { get; set; } = "#ffffff";
        public bool H_Status { get; set; } = true;
    }
}
