using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;
using Barayand.Models.Extra;
using Newtonsoft.Json;

namespace Barayand.Models.Entity
{
    public class RateModel:BaseModel
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int R_Id { get; set; }
        [JsonProperty("Rate")]
        public int R_Rate { get; set; } = 0;
        [JsonProperty("EntityId")]
        public int R_EntityId { get; set; } = 0;
        public string R_Ip { get; set; }
        [JsonProperty("EntityType")]
        public int R_Type { get; set; } = 1;//cat id -> 1 = product 2=> digital product 3=>training curses 4=> video 5=>Article/News
    }
}