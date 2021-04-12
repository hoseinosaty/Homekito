using Barayand.Models.Entity;
using Barayand.OutModels.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Barayand.DAL.Interfaces
{
    public interface IPCRepository : IPublicMethodRepsoitory<ProductCategoryModel>
    {
        //Task<ResponseStructure> GetAllInside();
        Task<ResponseStructure> GetAllByParentIdOneLevel(int parent);
        List<ProductCategoryModel> GetAllChild(int id, List<ProductCategoryModel> newLst);
        Task<ResponseStructure> UpdateRange(List<ProductCategoryModel> entities);
        Task<int> GetCategoryLevel(int cid);
        Task<List<ProductCategoryModel>> GetCategoryParents(int cid);
        Task<ResponseStructure> GetAllMenu();
    }
}
