using Barayand.Models.Entity;
using Barayand.OutModels.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
namespace Barayand.DAL.Interfaces
{
    public interface IUserRepository : IPublicMethodRepsoitory<UserModel>
    {
        Task<ResponseStructure> UserLogin(UserModel um);
        Task<ResponseStructure> AdminLogin(UserModel um);
        Task<ResponseStructure> UpdateProfile(Microsoft.AspNetCore.Http.HttpRequest httpRequest,int userId);
        Task<ResponseStructure> UpdatePassword(Microsoft.AspNetCore.Http.HttpRequest httpRequest, Microsoft.AspNetCore.Http.HttpResponse response, int userId);
        Task<ResponseStructure> SuspendUser(UserModel um);
        Task<ResponseStructure> ActiveUser(UserModel um);
        Task<ResponseStructure> UpdateUserAdmin(UserModel um);
        Task<ResponseStructure> GetAllAdmins();
        Task<ResponseStructure> InertAdmin(UserModel um);
        
    }
}
