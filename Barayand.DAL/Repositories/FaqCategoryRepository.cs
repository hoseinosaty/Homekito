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
    public class FaqCategoryRepository : GenericRepository<FaqCategoryModel>, IPublicMethodRepsoitory<FaqCategoryModel>
    {
        private readonly BarayandContext _context;
        public FaqCategoryRepository(BarayandContext context) : base(context)
        {
            this._context = context;
        }

        public async Task<ResponseStructure> LogicalAvailable(object id, bool newState)
        {
            try
            {
                var fc = await GetById(id);
                if(fc == null)
                {
                    return ResponseModel.Error("رکورد مورد نظر یافت نشد");
                }
                fc.F_Status = newState;
                return await Update(fc);
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
                var fc = await GetById(id);
                if (fc == null)
                {
                    return ResponseModel.Error("رکورد مورد نظر یافت نشد");
                }
                fc.F_IsDeleted = true;
                return await Update(fc);
            }
            catch (Exception ex)
            {
                return ResponseModel.ServerInternalError(data: ex);
            }
        }
    }
}
