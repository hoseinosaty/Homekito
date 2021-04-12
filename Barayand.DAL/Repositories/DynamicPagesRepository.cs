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
    public class DynamicPagesRepository :GenericRepository<DynamicPagesContent>, IPublicMethodRepsoitory<DynamicPagesContent>
    {
        private readonly BarayandContext _context;
        public DynamicPagesRepository(BarayandContext context):base(context)
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

        public Task<ResponseStructure> Delete(DynamicPagesContent entity)
        {
            throw new NotImplementedException();
        }

        public Task<ResponseStructure> Delete(object id)
        {
            throw new NotImplementedException();
        }

        public Task Dispose()
        {
            throw new NotImplementedException();
        }

        public async Task<ResponseStructure> GetAll()
        {
            try
            {
                return ResponseModel.Success(data: this._context.DynamicPagesContent.ToList());
            }
            catch (Exception ex)
            {
                return null;
            }
        }
        public async Task<List<DynamicPagesContent>> GetAllInside()
        {
            try
            {
                return this._context.DynamicPagesContent.ToList();
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

        public List<DynamicPagesContent> GetAllPaged(int startindex, int count, out int totalcount)
        {
            throw new NotImplementedException();
        }

        public async Task<DynamicPagesContent> GetById(object id)
        {
            try
            {
                return ((List<DynamicPagesContent>)((await this.GetAll()).Data)).FirstOrDefault(x=>x.PageName == id.ToString());
            }
            catch(Exception ex)
            {
                return null;
            }
        }

        public async Task<ResponseStructure> Insert(DynamicPagesContent entity)
        {
            try
            {
                var All = await this.GetAllInside();
                bool exists = All.Any(x => x.PageName == entity.PageName && x.Lang == entity.Lang);
                if (exists)
                {
                   entity.D_Id = All.FirstOrDefault(x=> x.PageName == entity.PageName).D_Id;
                   return await this.Update(entity);
                }
                else
                {
                   await this._context.DynamicPagesContent.AddAsync(entity: entity);
                   await this.CommitAllChanges();
                   return ResponseModel.Success("operation successfully completed");
                }
                
            }
            catch(Exception ex)
            {
                return ResponseModel.Error(msg:ex.Message,data:ex);
            }
        }


        public Task<ResponseStructure> LogicalAvailable(object id, bool newState)
        {
            throw new NotImplementedException();
        }

        public Task<ResponseStructure> LogicalDelete(object id)
        {
            throw new NotImplementedException();
        }

        public async Task<ResponseStructure> Update(DynamicPagesContent entity)
        {
            try
            {
                var item = await this.GetById(entity.PageName);
                entity.Created_At = item.Created_At;
                entity.Updated_At = DateTime.Now;
                this._context.Entry(item).State = Microsoft.EntityFrameworkCore.EntityState.Detached;
                this._context.DynamicPagesContent.Update(entity);
                await this._context.SaveChangesAsync();
                return ResponseModel.Success("رکورد مورد نظر با موفقیت بروزرسانی گردید");
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
    }
}
