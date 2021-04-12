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
    public class AmazingRequestRepository : GenericRepository<AmazingRequestModel>,IPublicMethodRepsoitory<AmazingRequestModel>
    {
        private readonly BarayandContext _context;
        public AmazingRequestRepository(BarayandContext context) : base(context)
        {
            this._context = context;
        }

        public Task<ResponseStructure> LogicalAvailable(object id, bool newState)
        {
            throw new NotImplementedException();
        }

        public Task<ResponseStructure> LogicalDelete(object id)
        {
            throw new NotImplementedException();
        }
        public async Task<ResponseStructure> Insert(AmazingRequestModel entity)
        {
            try
            {
                var checkExists = _context.AmazingRequests.FirstOrDefault(x=>x.A_UserId == entity.A_UserId && x.A_ProductId == entity.A_ProductId && x.A_Type == entity.A_Type);
                if(checkExists == null)
                {
                    _context.AmazingRequests.Add(entity);
                    await CommitAllChanges();
                }
                return ResponseModel.Success("operation successfully completed");
            }
            catch (Exception ex)
            {
                return ResponseModel.ServerInternalError(data: ex);
            }
        }
    }
}
