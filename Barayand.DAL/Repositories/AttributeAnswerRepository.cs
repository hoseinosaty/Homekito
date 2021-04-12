using Barayand.Models.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Barayand.DAL.Interfaces;
using Barayand.DAL.Context;
using Barayand.OutModels.Response;

namespace Barayand.DAL.Repositories
{
    public class AttributeAnswerRepository:GenericRepository<AttrAnswerModel> ,IAttributeAnswerRepository
    {
        private BarayandContext _context;
        public AttributeAnswerRepository(BarayandContext context):base(context)
        {
            _context = context;
        }

        public async Task<ResponseStructure> AddAnswer(AttrAnswerModel enityt)
        {
            try
            {
                var getAll = ((List<AttrAnswerModel>)(await this.GetAll()).Data).ToList();
                if(getAll.Count(x=>x.X_CatAttrId == enityt.X_CatAttrId && x.X_Answer == enityt.X_Answer && x.X_IsDeleted != true) > 0)
                {
                    return ResponseModel.Error("پاسخ وارد شده قبلا تعریف شده است");
                }
                return await this.Insert(enityt);
            }
            catch(Exception ex)
            {
                return ResponseModel.ServerInternalError(data:ex);
            }
        }

        public async Task<ResponseStructure> LogicalAvailable(object id, bool newState)
        {
            try
            {
                var check =await this.GetById(id);
                if (check == null)
                {
                    return ResponseModel.Error("پاسخ مورد یافت نشد");
                }
                check.X_Status = newState;
                return await this.Update(check);
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
                var check = await this.GetById(id);
                if (check == null)
                {
                    return ResponseModel.Error("پاسخ مورد یافت نشد");
                }
                check.X_IsDeleted = true;
                return await this.Update(check);
            }
            catch (Exception ex)
            {
                return ResponseModel.ServerInternalError(data: ex);
            }
        }

        public async Task<ResponseStructure> UpdateAnswer(AttrAnswerModel enityt)
        {
            try
            {
                var check = await this.GetById(enityt.X_Id);
                if (check == null)
                {
                    return ResponseModel.Error("پاسخ مورد یافت نشد");
                }
                check.X_Answer = enityt.X_Answer;
                return await this.Update(check);
            }
            catch (Exception ex)
            {
                return ResponseModel.ServerInternalError(data: ex);
            }
        }
    }
}
