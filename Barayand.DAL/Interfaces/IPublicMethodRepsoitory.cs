using Barayand.Models.Entity;
using Barayand.OutModels.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Barayand.DAL.Interfaces
{
    public interface IPublicMethodRepsoitory<T>:IGenericRepository<T> where T : class
    {
        /// <summary>
        /// Delete or Recovery Logical deleted entity
        /// </summary>
        /// <param name="id"></param>
        /// <param name="newState"></param>
        /// <returns></returns>
        Task<ResponseStructure> LogicalAvailable(object id, bool newState);
        /// <summary>
        /// Delete For Ever the entity
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<ResponseStructure> LogicalDelete(object id);
    }
}
