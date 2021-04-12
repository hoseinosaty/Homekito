using Barayand.Models.Extra;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
namespace Barayand.Models.Entity
{
    public class BetterPriceFoundModel : BaseModel
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int B_Id { get; set; }
        public int B_ProductId { get; set; } = 0;
        public string B_Price { get; set; } = "0";
        public string B_StoreWebAddress { get; set; }
        public string B_StoreName { get; set; }
        public string B_StoreOwnCity { get; set; }
        public int B_StoreType { get; set; } = 1;//1=>Website(fill sotre webaddress) 2=>Physical store(fill store name and stor own city)
    }
}
