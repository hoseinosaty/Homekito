using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;
using Barayand.Models.Extra;
namespace Barayand.Models.Entity
{
    public class OrderModel : BaseModel
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int O_Id { get; set; }
        public string O_ReciptId { get; set; }
        public int O_ProductId { get; set; } = 0;
        public int O_WarrantyId { get; set; } = 0;
        public int O_ColorId { get; set; } = 0;
        public int O_ProductManuualId { get; set; } = 0;//if this field filled user ordered only product manual
        public int O_ProductType { get; set; } = 1;//1=>product combine 2=>product manual
        public int O_GiftId { get; set; } = 0;//if 0 then current order dont has a gift
        public decimal O_Price { get; set; } = 0;
        public decimal O_Discount { get; set; } = 0;
        public int O_DiscountType { get; set; } = 1;// 1=> percentage 2=>price after reduce discount
        public int O_Quantity { get; set; } = 1;
        public int O_Version { get; set; } = 1;//if 1=> ordered product is pdf version 2=>ordered product is Hard copy
        public string Lang { get; set; }
    }
}