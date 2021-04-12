using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Barayand.OutModels.Models
{
    public class ExpertReview
    {
        public int E_Id { get; set; }
        public int E_ProductId { get; set; }
        public string E_Title { get; set; }
        public string E_Description { get; set; }
        public int E_Sort { get; set; } = 1;
        public string E_Image { get; set; } = "noimage.png";
        public bool E_Status { get; set; } = true;
        public bool E_IsDeleted { get; set; } = false;
    }
}
