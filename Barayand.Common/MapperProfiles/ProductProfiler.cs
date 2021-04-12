using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Barayand.Models;
using Barayand.OutModels;
using Barayand.Models.Entity;
using Barayand.OutModels.Models;
using Barayand.OutModels.Miscellaneous;


namespace Barayand.Common.MapperProfiles
{
    public class ProductProfiler : Profile
    {
        public ProductProfiler()
        {
            CreateMap<ProductModel, OutModels.Models.Product>().IncludeAllDerived().ReverseMap();
            CreateMap<ProductModel, OutModels.Miscellaneous.ProductBasketModel>().IncludeAllDerived().ReverseMap();
        }
    }
}
