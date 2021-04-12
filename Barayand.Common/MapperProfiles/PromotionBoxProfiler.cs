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
    public class PromotionBoxProfiler : Profile
    {
        public PromotionBoxProfiler()
        {
            CreateMap<PromotionBoxModel, OutModels.Models.PromotionBox>().IncludeAllDerived().ReverseMap();
        }
    }
}
