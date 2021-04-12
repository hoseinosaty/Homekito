using Barayand.Models.Extra;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Barayand.Models.Entity
{
    public class FavoriteModel:BaseModel
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int F_Id { get; set; }
        public int F_UserId { get; set; }
        public int F_EntityId { get; set; }
        public int F_EntityType { get; set; } = 1;//Entity Type 1=>Physical 2=>Digital 3=>Training

    }
}
