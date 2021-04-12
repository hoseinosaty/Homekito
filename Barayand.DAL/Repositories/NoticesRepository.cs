using Barayand.Common.Services;
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
    public class NoticesRepository: GenericRepository<NoticesModel>, IPublicMethodRepsoitory<NoticesModel>
    {
        private readonly BarayandContext _context;

        public NoticesRepository(BarayandContext context) : base(context)
        {
            this._context = context;
        }
        public async Task<ResponseStructure> Insert(NoticesModel entity)
        {
            try
            {
                entity.N_Date = UtilesService.PersianToMiladi(entity.N_ShamsiDate);
                await this._context.Notices.AddAsync(entity: entity);
                await this.CommitAllChanges();
                return ResponseModel.Success("operation successfully completed");
            }
            catch (Exception ex)
            {
                return null;
            }
        }
        public async Task<ResponseStructure> GetAll()
        {
            try
            {
                List<NoticesModel> All = this._context.Notices.Where(x => x.N_IsDeleted == false).ToList();
                foreach(var item in All)
                {
                    var cat = this._context.NoticesCategory.FirstOrDefault(x=>x.NC_Id == item.N_CId);
                    All.FirstOrDefault(x => x.N_Id == item.N_Id).N_CatTitle = (cat != null) ? cat.NC_Title : "";
                }
                return ResponseModel.Success(data: All);
            }
            catch (Exception ex)
            {
                return null;
            }
        }
        public async Task<ResponseStructure> LogicalAvailable(object id, bool newState)
        {
            try
            {
                var item = await this.GetById(id);
                item.N_Status = newState;
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
                item.N_IsDeleted = true;
                return await this.Update(item);
            }
            catch (Exception ex)
            {
                return null;
            }
        }
        public async Task<ResponseStructure> Update(NoticesModel entity)
        {
            try
            {
                var item = await this.GetById(entity.N_Id);
                entity.Created_At = item.Created_At;
                entity.Updated_At = DateTime.Now;
                this._context.Entry(item).State = Microsoft.EntityFrameworkCore.EntityState.Detached;
                this._context.Notices.Update(entity);
                await this.CommitAllChanges();
                return ResponseModel.Success("رکورد مورد نظر با موفقیت بروزرسانی گردید");
            }
            catch (Exception ex)
            {
                return null;
            }
        }
    }
}
