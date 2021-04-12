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
    public class HeaderNotifiRepository: GenericRepository<HeaderNotificationModel>, IPublicMethodRepsoitory<HeaderNotificationModel>
    {
        private readonly BarayandContext _context;
        public HeaderNotifiRepository(BarayandContext context) : base(context)
        {
            this._context = context;
        }

        public async Task<ResponseStructure> LogicalAvailable(object id, bool newState)
        {
            try
            {
                var e = await GetById(id);
                if(e == null)
                {
                    return ResponseModel.Error("رکورد مورد نظر یافت نشد");
                }
                e.H_Status = newState;
                return await Update(e);
            }
            catch(Exception ex)
            {
                return ResponseModel.ServerInternalError(data:ex);
            }
        }

        public Task<ResponseStructure> LogicalDelete(object id)
        {
            throw new NotImplementedException();
        }
    }
}
