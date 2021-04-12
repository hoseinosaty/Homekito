using Barayand.Models.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Barayand.OutModels.Models
{
    public class Trainings
    {
        public int T_Id { get; set; }
        public string T_Code { get; set; }
        public int T_MainCatId { get; set; } = 0;//first level of category id MAIN PARENT
        public int T_EndLevelCatId { get; set; } = 0;//Last level of category id 
        public int T_Level { get; set; } = 1;//Level of training course 1=>Beginner 2=>Intermediate  3=>Advanced
        public string T_Title { get; set; }
        public string T_Time { get; set; }
        public string T_Image { get; set; } = "noimage.png";
        public string T_Video { get; set; } = "noimage.png";
        public bool T_Status { get; set; } = true;
        public string T_Seo { get; set; }
        public string T_Url { get; set; }//Seo Url of Product
        public string T_ImgGallery { get; set; } = null;
        public string T_Description { get; set; }
        public string T_Summary { get; set; }
        public double T_Cost { get; set; } = 0;
        public double T_Discount { get; set; } = 0;
        public int T_DiscountType { get; set; } = 1;//Type of discount 1=>percentage 2=>price after reduced percentage from main price enter user
        public int T_SaleCount { get; set; } = 0;

        public bool T_IsDeleted { get; set; } = false;

        public string T_DedicatedField { get; set; }//this field not affected on database only recieved data from user and in repository insert/update function will be use
        public string T_Seasons { get; set; }
        public string T_CatTitle { get; set; }
        public List<ProductCategoryModel> T_ParentCategories { get; set; } = new List<ProductCategoryModel>();
    }
}
