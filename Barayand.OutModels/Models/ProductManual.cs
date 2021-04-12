using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Barayand.OutModels.Models
{
    public class ProductManual
    {
        public int M_Id { get; set; }
        public int M_ProductId { get; set; }
        public decimal M_Price { get; set; } = 0;
        public string M_Title { get; set; }
        public string M_FileName { get; set; }
        public bool M_Status { get; set; } = false;
    }
}
