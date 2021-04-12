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
    public class BrandProfiler:Profile
    {
        public BrandProfiler()
        {
            CreateMap<BrandModel, OutModels.Models.Brand>().IncludeAllDerived().ReverseMap();
            CreateMap<BrandModel, ComboItems.Brand>().IncludeAllDerived().ReverseMap();
        }
    }
}
