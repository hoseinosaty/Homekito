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
    public class CopponRepository : GenericRepository<CopponModel>, IPublicMethodRepsoitory<CopponModel>
    {
        private readonly BarayandContext _context;

        public CopponRepository(BarayandContext context) : base(context)
        {
            this._context = context;
        }
        public async Task<ResponseStructure> LogicalAvailable(object id, bool newState)
        {
            try
            {
                var item = await this.GetById(id);
                item.CP_Status = newState;
                return await this.Update(item);
            }
            catch (Exception ex)
            {
                return null;
            }
        }
        public async Task<ResponseStructure> LogicalDelete(object id)
        {
            try
            {
                var item = await this.GetById(id);
                item.CP_IsDeleted = true;
                return await this.Update(item);
            }
            catch (Exception ex)
            {
                return null;
            }
        }
        public async Task<ResponseStructure> Insert(CopponModel entity)
        {
            try
            {
                var all = (List<CopponModel>)(await this.GetAll()).Data;
                if (all.Count(x => x.CP_Code == entity.CP_Code) > 0)
                {
                    return ResponseModel.Error("کد تخفیف وارد شده قبلا در سیستم تعریف شده است");
                }
                await this._context.Coppon.AddAsync(entity: entity);
                await this.CommitAllChanges();
                return ResponseModel.Success("operation successfully completed");
            }
            catch (Exception ex)
            {
                return null;
            }
        }
        public async Task<ResponseStructure> Update(CopponModel entity)
        {
            try
            {
                var all = (List<CopponModel>)(await this.GetAll()).Data;
                if (all.Count(x => x.CP_Id != entity.CP_Id && x.CP_Code == entity.CP_Code) > 0)
                {
                    return ResponseModel.Error("کد تخفیف وارد شده قبلا در سیستم تعریف شده است");
                }
                var item = all.FirstOrDefault(x => x.CP_Id == entity.CP_Id);
                entity.Created_At = item.Created_At;
                entity.Updated_At = DateTime.Now;
                this._context.Entry(item).State = Microsoft.EntityFrameworkCore.EntityState.Detached;
                this._context.Coppon.Update(entity);
                await this._context.SaveChangesAsync();
                return ResponseModel.Success("رکورد مورد نظر با موفقیت بروزرسانی گردید");
            }
            catch (Exception ex)
            {
                return null;
            }
        }
    }
}
