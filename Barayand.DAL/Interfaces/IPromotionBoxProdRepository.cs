using Barayand.Models.Entity;
using Barayand.OutModels.Response;
using Barayand.OutModels.Miscellaneous;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Barayand.DAL.Interfaces
{
    public interface IPromotionBoxProdRepository
    {
        Task<ResponseStructure> UpdateRelation(List<PromotionBoxProductsModel> data,int secid = 0);
        Task<ResponseStructure> GetAllRelation(Miscellaneous data);
        Task<PromotionBoxProductsModel> CheckProductEixstsInBoxs(int pid);
        Task<PromotionBoxProductsModel> CheckProductCombineExistsInBox(int pid,int wid,int cid);
    }
}
