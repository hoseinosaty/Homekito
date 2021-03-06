using Barayand.Models.Entity;
using Barayand.OutModels.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Barayand.DAL.Interfaces
{
    public interface IAddressRepository : IPublicMethodRepsoitory<AddressModel>
    {
        Task<List<AddressModel>> GetUserActiveAddress(int userID);
        Task<ResponseStructure> DeleteUserAddress(int AddressId);
    }
}
