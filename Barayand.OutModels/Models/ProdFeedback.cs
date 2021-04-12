using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Barayand.OutModels.Models
{
    public class ProdFeedback
    {
        public int F_Id { get; set; }
        public int F_Topic { get; set; } = 1;
        public int[] F_Topics { get; set; } = new int[] { };
        public string F_TopicStr
        {
            get
            {
                switch (F_Topic)
                {
                    case 2:
                        return "عکس‌های کالا مناسب نیست";
                    case 3:
                        return "مشخصات فنی کالا صحیح نیست";
                    case 4:
                        return "توضیحات کالا صحیح نیست";
                    case 5:
                        return "این کالا غیراصل است";
                    case 6:
                        return "کالا تکراری است";
                    default:
                        return "نام کالا صحیح نیست";

                }
            }
        }
        public string F_DuplicateUrl { get; set; }//if topic == duplicate url , filled this field to duplicated product url
        public int F_ProductId { get; set; } = 0;
        public string F_ProductTitle { get; set; }
        public string F_Description { get; set; }
    }
}
