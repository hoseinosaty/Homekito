using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Barayand.Models;
using Barayand.OutModels;
using Barayand.Models.Entity;
using Barayand.OutModels.Models;

namespace Barayand.Common.MapperProfiles
{
    public class ProductCategoryProfiler:Profile
    {
        public ProductCategoryProfiler()
        {
            CreateMap<ProductCategoryModel,ProductCat>().IncludeAllDerived().ReverseMap();
        }
    }
}
