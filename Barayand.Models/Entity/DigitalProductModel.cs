using Barayand.Models.Extra;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Barayand.Models.Entity
{
    public class DigitalProductModel: BaseModel
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int DDP_Id { get; set; }
        [MaxLength(500)]
        public string DP_Title { get; set; }
        [MaxLength(20)]
        [Required]
        public string DP_Symbol { get; set; }
        public decimal DP_BuyPrice { get; set; } = 0;//Price of Buy from customer
        public bool DP_AllowBuy { get; set; } = false;//this column allow admin to set this dp in Sale To Site List Customer Wallet
        public bool DP_AllowSale { get; set; } = false;
        public double DP_AvailableCount { get; set; } = 0;
        public bool DP_AllowPay { get; set; } = false;//if true user can use this coin as source and buy other currency by this

        public int DP_SortField { get; set; }
        public bool DP_Status { get; set; }
        public bool DP_IsFavourite { get; set; } = false;
        [MaxLength(4000)]
        public string DP_SeoSetting { get; set; }
        [MaxLength(500)]
        public string DP_Icon { get; set; }
        [MaxLength(500)]
        public string DP_Avatar { get; set; }
        [MaxLength(2500)]
        public string DP_ImgGallery { get; set; } = null;
        [MaxLength(500)]
        public string DP_About { get; set; }
        [MaxLength(5000)]
        public string DP_TechnicalInfo { get; set; }
        public dynamic DP_Price { get; set; } = 0;
    }
}