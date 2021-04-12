using Barayand.Models.Extra;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Barayand.Models.Entity
{
    public class StoreModel:BaseModel
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int S_Id { get; set; }
        public string S_Title { get; set; }
        public int S_KeeperId { get; set; }
        public int S_ProvinceId { get; set; }
        public int S_CityId { get; set; }
        public bool S_Status { get; set; }
    }
}
