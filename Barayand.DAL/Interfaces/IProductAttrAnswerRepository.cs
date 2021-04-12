using Barayand.Models.Entity;
using Barayand.OutModels.Miscellaneous;
using Barayand.OutModels.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Barayand.DAL.Interfaces
{
    public interface IProductAttrAnswerRepository : IPublicMethodRepsoitory<ProductAttributeModel>
    {
        Task<List<IncomeProdFieldSetModel>> GetProductAttributes(int pid);
        Task<ResponseStructure> SetProductAttributes(List<IncomeProdFieldSetModel> attrs);
        Task<List<int>> GetProductsByAnswer(int ansid);
    }
}
