using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;
using Barayand.Models.Extra;
using Barayand.Models.KeyValueModel;

namespace Barayand.Models.Entity
{
    public class ProductModel:BaseModel
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int P_Id { get; set; }
        public string P_Code { get; set; }
        public int P_Type { get; set; } = 1;//1=>physical 2=>digital 3=>train 4=>train season
        public int P_MainCatId { get; set; } = 0;//first level of category id MAIN PARENT
        public int P_EndLevelCatId { get; set; } = 0;//Last level of category id 
        public int P_BrandId { get; set; } = 0;//Brand List Id -> in gbook this is Publisher ID
        public int P_MCuntryId { get; set; } = 0;//manufacturing cuntry code
        public int P_EnergyId { get; set; } = 0;
        public string P_Title { get; set; }
        public string P_EnTitle { get; set; }
        public int ManualRate { get; set; } = 0;//admin set manual rate for product
        public string P_DetailsTitle { get; set; }
        public string P_DetailsSubTitle { get; set; }
        public decimal P_Weight { get; set; } = 0;
        public string P_Image { get; set; } = "noimage.png";
        public bool P_Status { get; set; } = true;
        public bool P_CompanyWarranty { get; set; } = false;//Homekito warranty
        public bool P_ImmediateSend { get; set; } = false;//ارسال فوری
        public string P_Seo { get; set; }
        public int P_Sort { get; set; } = 1;
        public string P_Url { get; set; }//Seo Url of Product
        [MaxLength(2500)]
        public string P_ImgGallery { get; set; } = null;
        [MaxLength(5000)]
        public string P_Video { get; set; } = "noimage.png";//Aparat link in homekito
        [MaxLength(5000)]
        public string P_Description { get; set; }
        public string P_TechnicalInfo { get; set; }
        public string P_Manuals { get; set; }//دفترچه راهنما
        public bool P_IsDeleted { get; set; } = false;
        public bool P_BestOffer { get; set; } = false; //بهترین فرصت خرید


        /**********************HomeKito useless fields (فیلد های بی کاربرد در هومکیتو)**********************/
        public int P_AvailableCount { get; set; } = 0;
        public int P_SaleCount { get; set; } = 0;
        public string P_Isbn { get; set; }//if product is book set isbn to this field
        public string P_Audio { get; set; } = null;//if type == 1;Physical product demo file
        public string P_DownloadAbleAudio { get; set; } = null;//if type == 1;Physical product Main file
        /**********Main Price Data***************/
        public decimal P_Price { get; set; } = 0;
        public int P_PriceType { get; set; } = 1;//if type =>1 user was pay by rials 2=>user is out of iran and pay by points(points calculated by devide final price by 1000)
        public int P_PriceFormulaId { get; set; } = 0;
        /******************************************/
        /*****************Product Main Discount***********************/
        public decimal P_Discount { get; set; } = 0;
        public int P_DiscountType { get; set; } = 1;//Type of discount 1=>percentage 2=>price after reduced percentage from main price enter user
        /**************************************************/
        public decimal P_ExternalPrice { get; set; } = 0;//price of product in foreign websites such as Amazon - price is dollar base
       
        public decimal P_PageCount { get; set; } = 0;
        public int P_GiftBin { get; set; } = 0;//gift of buy by user
        public int P_BinPrice { get; set; } = 0;//user bin required for buy by bin

        /*************Print Able Price Data****************/
        public bool P_PrintAbleVersion { get; set; } = false;
        public decimal P_PrintAbleVerPrice { get; set; } = 0;
        public int P_PrintAbleVerPriceType { get; set; } = 0;//0=Is Rials 1=>Calculate By Formula
        public int P_PrintAbleVerFormulaId { get; set; } = 0;
        /***********************************/
        /*************Product Period Time Price Data****************/
        public int P_DiscountPeriodTime { get; set; } = 0;//after this time price of product will automacillay calculated from P_PeriodDiscountPrice or P_PriodDiscountFormulaId
        public int P_PeriodDiscountPriceType { get; set; } = 0;//0=Is Rials 1=>Calculate By Formula
        public decimal P_PeriodDiscountPrice { get; set; } = 0;
        public int P_PriodDiscountFormulaId { get; set; } = 0;
        public int P_PeriodPrintablePriceType { get; set; } = 0;//0=>rials 1=>formulaid
        public decimal P_PeriodPrintablePrice { get; set; } = 0;
        public int P_PeriodPrintableFomrulaId { get; set; } = 0;

        /***************************/
        public string P_DownloadLink { get; set; }
        public string Lang { get; set; } = "fa";
        public string P_PublishDate { get; set; }
        public int P_Author { get; set; } = 0;//Registered User Id
        /********************************************************************************/
        [NotMapped]
        public dynamic P_DedicatedField { get; set; }//this field not affected on database only recieved data from user and in repository insert/update function will be use
        [NotMapped]
        public int[] P_LabelIds { get; set; } = new int[] { };
        [NotMapped]
        public string P_CatTitle { get; set; }
        [NotMapped]
        public string P_BrandTitle { get; set; }
        [NotMapped]
        public List<ProductCategoryModel> P_ParentCategories { get; set; } = new List<ProductCategoryModel>();
        [NotMapped]
        public List<ProductModel> P_Relations { get; set; } = new List<ProductModel>();
        [NotMapped]
        public List<ProductLabelModel> P_Labels { get; set; } = new List<ProductLabelModel>();
        [NotMapped]
        public object P_AttributeStructures { get; set; } = new List<dynamic>();
        [NotMapped]
        public List<CommentModel> Comments { get; set; } = null;
        [NotMapped]
        public int Rate { get; set; } = 0;
        [NotMapped]
        public dynamic PriceModel { get; set; }
        [NotMapped]
        public bool IsAvailable { get; set; } = false;
        [NotMapped]
        public List<Warranty> Warranties { get; set; } = new List<Warranty>();
        [NotMapped]
        public List<Color> Colors { get; set; } = new List<Color>();
        [NotMapped]
        public ProductCombineModel DefaultProductCombine { get; set; } = null;
        [NotMapped]
        public ManufacturContryModel Cuntry { get; set; } = new ManufacturContryModel();
        [NotMapped]
        public List<ProductModel> RelatedProducts { get; set; } = new List<ProductModel>();
        [NotMapped]
        public List<ProductModel> CompletelyProducts { get; set; } = new List<ProductModel>();
        [NotMapped]
        public List<ProductModel> SetProducts { get; set; } = new List<ProductModel>();
        [NotMapped]
        public ProductModel Gift { get; set; } = null;
        [NotMapped]
        public int VisitCount { get; set; } = 0;
        [NotMapped]
        public bool AllowDownload { get; set; } = false;
        public decimal FinalPrice(decimal couponDiscount = 0,decimal shippingCos = 0,decimal price = 0,bool onlyPercentag = false)
        {
            try
            {
                decimal PRICE = (price == 0)? P_Price:price;
                decimal FinalPrice = 0;
                if(P_DiscountType == 1)
                {
                    FinalPrice = (PRICE - ((PRICE * P_Discount) / 100));
                }
                else
                {
                    FinalPrice = (onlyPercentag)? PRICE : P_Discount;
                }
                FinalPrice = FinalPrice - ((FinalPrice * couponDiscount) / 100);
                FinalPrice = FinalPrice + shippingCos;
                return FinalPrice;
            }
            catch(Exception ex)
            {
                return 0;
            }
        }
        
       
    }
}