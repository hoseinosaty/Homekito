using Barayand.Models.Extra;
using Barayand.Models.RuntimeModels;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Barayand.Models.Entity
{
    public class ProductCombineModel : BaseModel
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int X_Id{ get; set; }
        public int X_ProductId { get; set; } = 0;
        public int X_WarrantyId { get; set; } = 0;
        public int X_ColorId { get; set; } = 0;
        public int X_Sort { get; set; } = 1;
        public decimal X_Price { get; set; } = 0;
        public decimal X_Discount { get; set; } = 0;
        public int X_DiscountType { get; set; } = 1;//1=>Percentage 2=>(Price - Discount)
        public bool X_Default { get; set; } = false;
        public bool X_Status { get; set; } = true;
        public int X_AvailableCount { get; set; } = 0; //موجودی
        public bool X_IsDeleted { get; set; } = false;
        [NotMapped]
        public ColorModel ColorDetail { get; set; } = null;
        [NotMapped]
        public WarrantyModel WarrantyModel { get; set; } = null;
        public decimal CalculatedPrice()
        {
            try
            {
                if (this.X_DiscountType == 1)
                {
                    var res = (X_Price - ((X_Price * X_Discount) / 100));
                    return res;
                }
                else
                {
                    return X_Discount;
                }
            }
            catch(Exception ex)
            {
                return 0;
            }
        }
        [NotMapped]
        public ProductCombinePriceModel PriceModel { get; set; } = new ProductCombinePriceModel();
    }
}
