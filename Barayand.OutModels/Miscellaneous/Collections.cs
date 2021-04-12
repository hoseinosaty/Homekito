using Barayand.Models.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Barayand.OutModels.Miscellaneous
{
    public class Collections
    {
    }
    public class ProductListPage
    {
        public List<ProductModel> Products { get; set; } = new List<ProductModel>();
        public List<BrandModel> Publishers { get; set; } = new List<BrandModel>();
        public List<ProductCategoryModel> Categories { get; set; } = new List<ProductCategoryModel>();
        public List<string> PublishYears { get; set; } = new List<string>();
    }
}
