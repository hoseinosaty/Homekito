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
    public class AddressRepository : GenericRepository<AddressModel>, IPublicMethodRepsoitory<AddressModel>, IAddressRepository
    {
        private readonly BarayandContext _context;

        public AddressRepository(BarayandContext context) : base(context)
        {
            this._context = context;
        }

        public async Task<ResponseStructure> DeleteUserAddress(int AddressId)
        {
            try
            {
                var addr = await GetById(AddressId);
                if(addr == null)
                {
                    return ResponseModel.Error("آدرس مورد نظر یافت نشد");
                }
                addr.isActive = false;
                return await this.Update(addr);
            }
            catch(Exception ex)
            {
                return ResponseModel.ServerInternalError(data:ex);
            }
        }

        public async Task<List<AddressModel>> GetUserActiveAddress(int userID)
        {
            try
            {
                var allAddress = ((List<AddressModel>)(await GetAll()).Data);
                return allAddress.Where(x=>x.A_UserId == userID && x.isActive).ToList();
            }
            catch(Exception ex)
            {
                return new List<AddressModel>();
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
    }
}
