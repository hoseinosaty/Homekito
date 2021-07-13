using Barayand.Models.RuntimeModels;
using Barayand.OutModels.Models;
using Barayand.OutModels.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Barayand.DAL.Interfaces
{
    public interface IGenericRepository<TEntity> 
    {

       

        /// <summary>
        /// Get a list of TEntiti's
        /// </summary>
        /// <returns>List<TEntity></returns>
        Task<ResponseStructure> GetAll();
        /// <summary>
        /// Get a paged list of TEntiti's
        /// </summary>
        /// <param name="startindex"></param>
        /// <param name="count"></param>
        /// <param name="totalcount"></param>
        /// <returns>List<TEntity></returns>
        List<TEntity> GetAllPaged(int startindex,int count,out int totalcount);
        /// <summary>
        /// Get a TEntity by entity id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<TEntity> GetById(object id);

        Task<List<TEntity>> ApplyFilter(Expression<Func<TEntity, bool>> filter = null);
        Task<PaginationModel<TEntity>> ApplyFilter(int Take,Expression<Func<TEntity, bool>> filter = null);
        /// <summary>
        /// Insert new TEntity to Database
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        Task<ResponseStructure> Insert(TEntity entity);
        /// <summary>
        /// Delete a TEntity from Database by TEntity
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        Task<ResponseStructure> Delete(TEntity entity);
        /// <summary>
        /// Delete a TEntity from Database by id
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        Task<ResponseStructure> Delete(object id);
        /// <summary>
        /// Update a TEntity by entity
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        Task<ResponseStructure> Update(TEntity entity);
        Task CommitAllChanges();
        Task Dispose();

       

        
    }
}
