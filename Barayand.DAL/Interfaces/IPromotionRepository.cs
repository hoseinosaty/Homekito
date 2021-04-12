using Barayand.Models.Entity;
using Barayand.OutModels.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Barayand.DAL.Interfaces
{
    public interface IPromotionRepository:IPublicMethodRepsoitory<PromotionBoxModel>
    {
        Task<List<PromotionBoxModel>> GetByType(int type);
    }
}
