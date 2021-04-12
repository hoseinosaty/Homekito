using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;
using Barayand.Models.Extra;
namespace Barayand.Models.Entity
{
    public class VisitsModel:BaseModel
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int V_Id { get; set; }
        public int V_Entity { get; set; }
        public int V_Count { get; set; }
        public int V_Type { get; set; }//1=>news
        public DateTime V_LastVisied { get; set; }
        public string V_Ip { get; set; }
    }
}