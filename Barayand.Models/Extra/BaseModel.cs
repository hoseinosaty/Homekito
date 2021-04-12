using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Barayand.Models.Extra
{
    public class BaseModel
    {
        public DateTime? Created_At { get; set; } = DateTime.Now;
        public DateTime? Updated_At { get; set; } = DateTime.Now;
        public DateTime? Deleted_At { get; set; } = DateTime.Now;
    }
}
