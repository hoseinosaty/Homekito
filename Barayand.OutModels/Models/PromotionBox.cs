using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Barayand.OutModels.Models
{
    public class PromotionBox
    {
        public int B_Id { get; set; }
        public int B_EntityId { get; set; } = 0;//if box is for category then category id filled here
        public string B_Title { get; set; }
        public string B_Image { get; set; } = "noimage.png";
        public string B_Seo { get; set; }
        public string B_Link { get; set; }
        public int B_SectionId { get; set; } = 1;
        public int B_LoadType { get; set; } = 1;//Type of box type 1=>Product List 2=>Link
        public int B_Type { get; set; } = 1;//1=>Image Box(باکس تصویری) 
        public string B_ColorCode { get; set; } = "#0046bf";
    }
}
