using Barayand.DAL.Context;
using Barayand.DAL.Interfaces;
using Barayand.Models.Entity;
using Barayand.OutModels.Miscellaneous;
using Barayand.OutModels.Models;
using Barayand.OutModels.Response;
using System;
using System.Collections.Generic;

using System.Linq;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace Barayand.DAL.Repositories
{
    public class ServiceProfitsRepository : GenericRepository<ServiceModel>, IPublicMethodRepsoitory<ServiceModel>
    {
        private readonly BarayandContext _context;
        public ServiceProfitsRepository(BarayandContext context) : base(context)
        {
            this._context = context;
        }

        public async Task<ResponseStructure> LogicalAvailable(object id, bool newState)
        {
            try
            {
                var service = await GetById(id);
                if(service == null)
                {
                    return ResponseModel.Error("رکورد مورد نظر یافت نشد");
                }
                service.S_Status = newState;
                service.Updated_At = DateTime.Now;
                return await Update(service);
            }
            catch(Exception ex)
            {
                return ResponseModel.ServerInternalError(data: ex);
            }
        }

        public async Task<ResponseStructure> LogicalDelete(object id)
        {
            try
            {
                var service = await GetById(id);
                if (service == null)
                {
                    return ResponseModel.Error("رکورد مورد نظر یافت نشد");
                }
                service.Updated_At = DateTime.Now;
                return await Update(service);
            }
            catch (Exception ex)
            {
                return ResponseModel.ServerInternalError(data: ex);
            }
        }
    }
}
