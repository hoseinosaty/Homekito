using Barayand.Models.Entity;
using Barayand.OutModels.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Barayand.DAL.Interfaces
{
    public interface IAttributeAnswerRepository : IGenericRepository<AttrAnswerModel>,IPublicMethodRepsoitory<AttrAnswerModel>
    {
        Task<ResponseStructure> UpdateAnswer(AttrAnswerModel enityt);
        Task<ResponseStructure> AddAnswer(AttrAnswerModel enityt);
    }
}
