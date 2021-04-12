using Barayand.Models.Extra;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Barayand.Models.Entity
{
    public class ServiceModel:BaseModel
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int S_Id { get; set; }
        public string S_Title { get; set; }
        public string S_Image { get; set; } = "noimage.png";
        public int S_Sort { get; set; } = 1;
        public bool S_Status { get; set; } = true;
        public string S_Seo { get; set; }
        public string S_Content { get; set; }
        public string S_Icon { get; set; } = "noimage.png";
        public string S_ImageGallery { get; set; }
        public int S_Type { get; set; } = 1;//1=>Service 2=>Profits(مزیت)

    }
}
