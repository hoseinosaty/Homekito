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
    public class AttributeProfiler:Profile
    {
        public AttributeProfiler()
        {
            CreateMap<AttributeModel, OutModels.Models.Attribute>().IncludeAllDerived().ReverseMap();
            CreateMap<AttributeModel, ComboItems.Attribute>().IncludeAllDerived().ReverseMap();
            CreateMap<CatAttrRelationModel, OutModels.Models.CatAttrRelation>().IncludeAllDerived().ReverseMap();
        }
    }
}
