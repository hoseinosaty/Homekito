using Barayand.Models.Extra;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Barayand.Models.Entity
{
    public class CommentModel:BaseModel
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int C_Id { get; set; }
        public int C_ParentId { get; set; } = 0;
        [JsonProperty("User")]
        public string C_UserName { get; set; }
        [JsonProperty("Email")]
        public string C_UserEmail { get; set; }
        public int C_Rate { get; set; } = 0;
        [JsonProperty("EntityType")]
        public int C_Type { get; set; } = 1; //1=>product 2=>digital product 3=>training curse 4=> video 5=>news
        [JsonProperty("EntityId")]
        public int C_EntityId { get; set; } = 0;
        public int C_Status { get; set; } = 1; /// 1=>new 2=> accepted
        [JsonProperty("Msg")]
        public string C_Body { get; set; }
    }
}