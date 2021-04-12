using Barayand.DAL.Context;
using Barayand.DAL.Interfaces;
using Barayand.OutModels.Response;
using System;
using System.Collections.Generic;

using System.Linq;
using System.Threading.Tasks;

namespace Barayand.DAL.Repositories
{
    public class GenericRepository<TEntity> : IGenericRepository<TEntity> where TEntity : class
    {
        private readonly BarayandContext _context;

        public GenericRepository(BarayandContext context)
        {
            this._context = context;
        }
        private Microsoft.EntityFrameworkCore.DbSet<TEntity> _objectset;
        internal Microsoft.EntityFrameworkCore.DbSet<TEntity> DbSet
        {
            get
            {
                if (_objectset == null)
                {
                    _objectset = this._context.Set<TEntity>();
                }
                return _objectset;
            }
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

        public async Task<ResponseStructure> Delete(TEntity entity)
        {
            try
            {
                this.DbSet.Remove(entity);
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
                this.DbSet.Remove(entity);
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
                return ResponseModel.Success(data:  this.DbSet.ToList());
            }
            catch (Exception ex)
            {
                return ResponseModel.ServerInternalError(data: ex);
            }
        }

        public List<TEntity> GetAllPaged(int startindex, int count, out int totalcount)
        {
            throw new NotImplementedException();
        }

        public async Task<TEntity> GetById(object id)
        {
            try
            {
                var entity = await  this.DbSet.FindAsync(id);
                return entity;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public async Task<ResponseStructure> Insert(TEntity entity)
        {
            try
            {
                await this.DbSet.AddAsync(entity: entity);
                await this.CommitAllChanges();
                return ResponseModel.Success("operation successfully completed");
            }
            catch (Exception ex)
            {
                return ResponseModel.ServerInternalError(data: ex);
            }
        }

        public async Task<ResponseStructure> Update(TEntity entity)
        {
            try
            {
                this.DbSet.Update(entity);
                await this.CommitAllChanges();
                return ResponseModel.Success("رکورد مورد نظر با موفقیت بروزرسانی گردید");
            }
            catch (Exception ex)
            {
                return ResponseModel.ServerInternalError(data: ex);
            }
        }

        public Task<ResponseStructure> GetAllByParentIdOneLevel(int parent)
        {
            throw new NotImplementedException();
        }



        /*****************************/


    }
}
