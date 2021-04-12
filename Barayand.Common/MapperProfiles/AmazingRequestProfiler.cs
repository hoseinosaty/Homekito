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
    public class AmazingRequestProfiler:Profile
    {
        public AmazingRequestProfiler()
        {
            CreateMap<AmazingRequestModel, OutModels.Models.AmazingRequest>().IncludeAllDerived().ReverseMap();
        }
    }
}
