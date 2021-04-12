using Barayand.Models.Entity;
using Barayand.OutModels.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
namespace Barayand.DAL.Interfaces
{
    public interface IRateRepository : IPublicMethodRepsoitory<RateModel>
    {
        Task<decimal> CalulateRate(int entity,int type);
    }
}
