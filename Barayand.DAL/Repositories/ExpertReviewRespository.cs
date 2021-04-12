using Barayand.DAL.Context;
using Barayand.DAL.Interfaces;
using Barayand.Models.Entity;
using Barayand.OutModels.Models;
using Barayand.OutModels.Response;
using System;
using System.Collections.Generic;

using System.Linq;
using System.Threading.Tasks;

namespace Barayand.DAL.Repositories
{
    public class ExpertReviewRespository : GenericRepository<ExpertReviewModel>, IPublicMethodRepsoitory<ExpertReviewModel>, IExpertReviewRepository
    {
        private readonly BarayandContext _context;
        public ExpertReviewRespository(BarayandContext context) : base(context)
        {
            _context = context;
        }

        public async Task<List<ExpertReviewModel>> GetByProduct(int pid)
        {
            try
            {
                var all = ((List<ExpertReviewModel>)(await GetAll()).Data);
                return all.Where(x=>x.E_ProductId == pid && !x.E_IsDeleted).ToList();
            }
            catch(Exception ex)
            {
                return null;
            }
        }

        public async Task<ResponseStructure> LogicalAvailable(object id, bool newState)
        {
            try
            {
                var e = await GetById(id);
                if(e == null)
                {
                    return ResponseModel.Error("مورد یافت نشد");
                }
                e.E_Status = newState;
                return await Update(e);
            }
            catch(Exception ex)
            {
                return ResponseModel.ServerInternalError(data:ex);
            }
        }

        public async Task<ResponseStructure> LogicalDelete(object id)
        {
            try
            {
                var e = await GetById(id);
                if (e == null)
                {
                    return ResponseModel.Error("مورد یافت نشد");
                }
                e.E_IsDeleted = true;
                return await Update(e);
            }
            catch (Exception ex)
            {
                return ResponseModel.ServerInternalError(data: ex);
            }
        }
    }
}
