using Barayand.DAL.Context;
using Barayand.DAL.Interfaces;
using Barayand.Models.Entity;
using Barayand.OutModels.Models;
using Barayand.OutModels.Response;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
namespace Barayand.DAL.Repositories
{
    public class WarrantyRepository: GenericRepository<WarrantyModel>, IPublicMethodRepsoitory<WarrantyModel>
    {
        private readonly BarayandContext _context;

        public WarrantyRepository(BarayandContext context):base(context)
        {
            this._context = context;
        }
        public async Task CommitAllChanges()
        {
            try
            {
                await this._context.SaveChangesAsync();
            }
            catch (Exception ex)
            {

            }
        }

        public async Task<ResponseStructure> Delete(WarrantyModel entity)
        {
            try
            {
                this._context.Warranty.Remove(entity);
                await this.CommitAllChanges();
                return ResponseModel.Success("رکورد مورد نظر با موفقیت حذف گردید");
            }
            catch (Exception ex)
            {
                return ResponseModel.Error(msg: ex.Message);
            }
        }

        public async Task<ResponseStructure> Delete(object id)
        {
            try
            {
                var entity = await this.GetById(id);
                this._context.Warranty.Remove(entity);
                await this.CommitAllChanges();
                return ResponseModel.Success("رکورد مورد نظر با موفقیت حذف گردید");
            }
            catch (Exception ex)
            {
                return ResponseModel.Error(msg: ex.Message);
            }
        }

        public async Task Dispose()
        {
            try
            {
                await this._context.DisposeAsync();
            }
            catch (Exception ex)
            {

            }
        }

        public async Task<ResponseStructure> GetAll()
        {
            try
            {
                return ResponseModel.Success(data: this._context.Warranty.Where(x => x.W_IsDeleted == false).ToList());
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public Task<ResponseStructure> GetAllByParentIdOneLevel(int parent)
        {
            throw new NotImplementedException();
        }

        public List<WarrantyModel> GetAllPaged(int startindex, int count, out int totalcount)
        {
            throw new NotImplementedException();
        }

        public async Task<WarrantyModel> GetById(object id)
        {
            try
            {
                var entity = await Task.Run(() => this._context.Warranty.AsNoTracking().FirstOrDefault(x => x.W_Id == int.Parse(id.ToString())));
                return entity;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public async Task<ResponseStructure> Insert(WarrantyModel entity)
        {
            try
            {
                await this._context.Warranty.AddAsync(entity: entity);
                await this.CommitAllChanges();
                return ResponseModel.Success("operation successfully completed");
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public Task<ResponseStructure> Insert(CatAttrRelation entity)
        {
            throw new NotImplementedException();
        }

        public async Task<ResponseStructure> LogicalAvailable(object id, bool newState)
        {
            try
            {
                var item = await this.GetById(id);
                item.W_Status = newState;
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
                item.W_IsDeleted = true;
                return await this.Update(item);
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public async Task<ResponseStructure> Update(WarrantyModel entity)
        {
            try
            {
                var item = await this.GetById(entity.W_Id);
                entity.Created_At = item.Created_At;
                entity.Updated_At = DateTime.Now;
                this._context.Entry(item).State = Microsoft.EntityFrameworkCore.EntityState.Detached;
                this._context.Warranty.Update(entity);
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
