using Barayand.Models.Entity;
using Barayand.OutModels.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;


namespace Barayand.DAL.Interfaces
{
    public interface IExpertReviewRepository : IPublicMethodRepsoitory<ExpertReviewModel>
    {
        Task<List<ExpertReviewModel>> GetByProduct(int pid);

    }
}
