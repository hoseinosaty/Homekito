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
    public class FaqRepository : GenericRepository<FaqModel>, IPublicMethodRepsoitory<FaqModel>
    {
        private readonly BarayandContext _context;
        public FaqRepository(BarayandContext context) : base(context)
        {
            this._context = context;
        }
        public async Task<ResponseStructure> GetAll()
        {
            try
            {
                var allFaqs = this._context.Faq.Where(x=>!x.FA_IsDeleted).ToList();
                var allFaqCats = this._context.FaqCategories.ToList();
                foreach(var item in allFaqs)
                {
                    var cat = allFaqCats.FirstOrDefault(x=>x.F_Id == item.FA_CatId);
                    if(cat != null)
                    {
                        item.FA_CatTitle = cat.F_Title;
                    }
                }
                return ResponseModel.Success(data: allFaqs);
            }
            catch (Exception ex)
            {
                return ResponseModel.ServerInternalError(data: ex);
            }
        }
        public async Task<ResponseStructure> LogicalAvailable(object id, bool newState)
        {
            try
            {
                var fc = await GetById(id);
                if (fc == null)
                {
                    return ResponseModel.Error("رکورد مورد نظر یافت نشد");
                }
                fc.FA_Status = newState;
                return await Update(fc);
            }
            catch (Exception ex)
            {
                return ResponseModel.ServerInternalError(data: ex);
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
                fc.FA_IsDeleted = true;
                return await Update(fc);
            }
            catch (Exception ex)
            {
                return ResponseModel.ServerInternalError(data: ex);
            }
        }
    }
}
