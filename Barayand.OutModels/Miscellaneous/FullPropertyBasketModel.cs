using Barayand.Models.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Barayand.OutModels.Miscellaneous
{
    public class FullPropertyBasketModel
    {
        public List<FullPropertyBasketItem> CartItems { get; set; } = new List<FullPropertyBasketItem>();
        public List<CopponModel> Coppon { get; set; } = new List<CopponModel>();
        public ReciptientInfoModel RecipientInfo { get; set; }
        public int GiftBoxWrapperId { get; set; } = 0;
        public DateTime OrderDate { get; set; } = DateTime.Now;
        public decimal Total { get; set; } = 0;
    }
}