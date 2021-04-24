using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Barayand.Common.Constants;
using Barayand.Common.Services;
using Barayand.DAL.Context;
using Barayand.DAL.Interfaces;
using Barayand.Models.Entity;
using Barayand.OutModels.Miscellaneous;
using Barayand.OutModels.Models;
using Barayand.OutModels.Response;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;

namespace Barayand.DAL.Repositories
{
    public class UserRepository : GenericRepository<UserModel>, IPublicMethodRepsoitory<UserModel>, IUserRepository
    {
        private readonly BarayandContext _context;
        public UserRepository(BarayandContext context) : base(context)
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

        public async Task<ResponseStructure> AdminLogin(UserModel um)
        {
            try
            {
                var getAll = (List<UserModel>)(await GetAll()).Data;
                var exists = getAll.FirstOrDefault(x => x.U_Email == um.U_Email);
                bool truepwd = false;
                if (exists == null || !CryptoJsService.verifyPassword(exists.U_Password, um.U_Password))
                {
                    return ResponseModel.Error("Sorry,we couldn't find an account with that username and password.");
                }
                if (exists.U_Status == 2)
                {
                    return ResponseModel.Error("Your account has been suspened.");
                }
                if (um.U_Role == 1)
                {
                    var perms = _context.Permissions.Where(x => x.Perm_Uid == exists.U_Id).ToList();
                    foreach (var perm in perms)
                    {
                        um.Permissions.Add(perm.Perm_Urlid);
                    }
                }
                var token = TokenService.GenerateToken(exists.U_Id, exists.U_Name + " " + exists.U_Family, exists.U_Email);
                um.Token = token;
                return ResponseModel.Success(data: um);

            }
            catch (Exception ex)
            {
                return ResponseModel.ServerInternalError(data: ex);
            }
        }
        public async Task<ResponseStructure> UserLogin(UserModel um)
        {
            try
            {
                var getAll = (List<UserModel>)(await GetAll()).Data;
                var exists = getAll.FirstOrDefault(x => x.U_Phone == um.U_Phone);
                bool truepwd = false;
                if (exists == null)
                {
                    return ResponseModel.Error("Sorry,we couldn't find an account with that username and password.");
                }
                if (exists.U_Status == 2)
                {
                    return ResponseModel.Error("Your account has been suspened.");
                }
                var token = TokenService.GenerateToken(exists.U_Id, exists.U_Name + " " + exists.U_Family, exists.U_Phone);
                um.Token = token;
                return ResponseModel.Success(data: um);

            }
            catch (Exception ex)
            {
                return ResponseModel.ServerInternalError(data: ex);
            }
        }
        public async Task<UserModel> GetById(object id)
        {
            try
            {
                return ((List<UserModel>)((await this.GetAll()).Data)).FirstOrDefault(x => x.U_Id == (int)id);
            }
            catch (Exception ex)
            {
                return null;
            }
        }
        public  async Task<ResponseStructure> Insert(UserModel entity)
        {
            try
            {
                
                var getAll = (List<UserModel>)(await GetAll()).Data;
                bool exists = getAll.Count(x => x.U_Phone == entity.U_Phone && x.U_Role == entity.U_Role) > 0;
                if(exists)
                {
                    return ResponseModel.Error("This email already exists.");
                }
                entity.Created_At = DateTime.Now;
                entity.Updated_At = DateTime.Now;
                entity.U_Status = 1;
                entity.U_Password = CryptoJsService.HashPassword(entity.U_Password);
                await this._context.Users.AddAsync(entity: entity);
                await this.CommitAllChanges();
                if(entity.U_Role == 1)
                {
                    foreach(var perm in entity.Permissions)
                    {
                        this._context.Permissions.Add(new PermissionModel() {
                            Perm_Uid = entity.U_Id,
                            Perm_Urlid = perm
                        });
                    }
                    await this.CommitAllChanges();
                }
                return ResponseModel.Success("Registration completed Successfully!");
            }
            catch (Exception ex)
            {
                return ResponseModel.ServerInternalError(data:ex);
            }
        }

        public async Task<ResponseStructure> UpdateProfile(HttpRequest httpRequest, int userId)
        {
            try
            {
                UserModel userModel = await this.GetById(userId);
                if(userModel == null)
                {
                    return ResponseModel.Error("Your login token has expired.", new { EXPIREDTOKEN = true });
                }
                var data = httpRequest.Headers["UpdateData"];
                var dec = Barayand.Common.Services.CryptoJsService.DecryptStringAES(data);
                RegisterModel rm = JsonConvert.DeserializeObject<RegisterModel>(dec);
                userModel.U_Avatar = rm.Avatar;
                userModel.U_Family = rm.Family;
                userModel.U_Name = rm.Name;
                userModel.U_Phone = rm.Phone;
                userModel.U_HomeTel = rm.HomeTel;
                userModel.U_IdentityCode = rm.IdentityCode;
                userModel.U_CreditCardNum = rm.CreditCardNum;
                userModel.U_BirthDate = rm.BirthDate;
                userModel.U_Email = rm.Email;
                return await this.Update(userModel);
            }
            catch (Exception ex)
            {
                return ResponseModel.ServerInternalError(data: ex);
            }
        }

        public async Task<ResponseStructure> UpdatePassword(HttpRequest httpRequest, Microsoft.AspNetCore.Http.HttpResponse response, int userId)
        {
            try
            {
                UserModel userModel = await this.GetById(userId);
                if (userModel == null)
                {
                    return ResponseModel.Error("Your login token has expired.", new { EXPIREDTOKEN = true });
                }
                var data = httpRequest.Headers["PasswordUpdate"];
                var dec = Barayand.Common.Services.CryptoJsService.DecryptStringAES(data);
                RegisterModel rm = JsonConvert.DeserializeObject<RegisterModel>(dec);
                if(!Barayand.Common.Services.CryptoJsService.verifyPassword(userModel.U_Password,rm.CurrentPassword))
                {
                    return ResponseModel.Error("Current password was incorrect.");
                }
                if(rm.ConfirmPassword != rm.Password)
                {
                    return ResponseModel.Error("Password and confirm password does not match.");
                }
                userModel.U_Password = Barayand.Common.Services.CryptoJsService.HashPassword(rm.Password);
                response.Cookies.Delete("ValhallaUser");
                return await this.Update(userModel);
            }
            catch (Exception ex)
            {
                return ResponseModel.ServerInternalError(data: ex);
            }
        }

        public async Task<ResponseStructure> SuspendUser(UserModel um)
        {
            try
            {
                var user = await this.GetById(um.U_Id);
                if(user == null)
                {
                    return ResponseModel.Error("کاربر یافت نشد");
                }
                user.U_Status = 2;
                user.U_SuspendReason = um.U_SuspendReason;
                return await this.Update(user);
            }
            catch(Exception ex)
            {
                return ResponseModel.ServerInternalError(data:ex);
            }
        }

        public async Task<ResponseStructure> ActiveUser(UserModel um)
        {
            try
            {
                var user = await this.GetById(um.U_Id);
                if (user == null)
                {
                    return ResponseModel.Error("کاربر یافت نشد");
                }
                user.U_Status = 1;
                return await this.Update(user);
            }
            catch (Exception ex)
            {
                return ResponseModel.ServerInternalError(data: ex);
            }
        }

        public async Task<ResponseStructure> GetAllAdmins()
        {
            try
            {
                var users = ((List<UserModel>)(await this.GetAll()).Data).Where(x=>x.U_Role == 1).ToList();

                foreach(var item in users)
                {
                    var perms = _context.Permissions.Where(x=>x.Perm_Uid == item.U_Id).ToList();
                    foreach(var perm in perms)
                    {
                        item.Permissions.Add(perm.Perm_Urlid);
                    }
                }
                return ResponseModel.Success(data:users);
            }
            catch(Exception ex)
            {
                return ResponseModel.ServerInternalError(data:ex);
            }
        }

        public async Task<ResponseStructure> UpdateUserAdmin(UserModel um)
        {
            try
            {
                var user = await this.GetById(um.U_Id);
                if(user == null)
                {
                    return ResponseModel.Error("کاربر یافت نشد");
                }
                var getAll = (List<UserModel>)(await GetAll()).Data;
                bool exists = getAll.Count(x => x.U_Email == um.U_Email && x.U_Role == 1 && x.U_Id != um.U_Id) > 0;
                if (exists)
                {
                    return ResponseModel.Error("This email already exists.");
                }
                user.U_Name = um.U_Name;
                user.U_Family = um.U_Family;
                user.U_Phone = um.U_Phone;
                user.U_Email = user.U_Email;
                user.Updated_At = DateTime.Now;
                var update = await this.Update(user);
                if(!update.Status)
                {
                    return update;
                }
                var perms = _context.Permissions.Where(x => x.Perm_Uid == user.U_Id).ToList();
                _context.Permissions.RemoveRange(perms);
                await this.CommitAllChanges();
                foreach (var perm in um.Permissions)
                {
                    this._context.Permissions.Add(new PermissionModel()
                    {
                        Perm_Uid = user.U_Id,
                        Perm_Urlid = perm
                    });
                }
                await this.CommitAllChanges();
                return ResponseModel.Success();
            }
            catch(Exception ex)
            {
                return ResponseModel.ServerInternalError(data:ex);
            }
        }

        public async Task<ResponseStructure> InertAdmin(UserModel um)
        {
            try
            {

                var getAll = (List<UserModel>)(await GetAll()).Data;
                bool exists = getAll.Count(x => x.U_Phone == um.U_Email && x.U_Role == um.U_Role) > 0;
                if (exists)
                {
                    return ResponseModel.Error("This email already exists.");
                }
                um.Created_At = DateTime.Now;
                um.Updated_At = DateTime.Now;
                um.U_Status = 1;
                um.U_Password = CryptoJsService.HashPassword(um.U_Password);
                await this._context.Users.AddAsync(entity: um);
                await this.CommitAllChanges();
                if (um.U_Role == 1)
                {
                    foreach (var perm in um.Permissions)
                    {
                        this._context.Permissions.Add(new PermissionModel()
                        {
                            Perm_Uid = um.U_Id,
                            Perm_Urlid = perm
                        });
                    }
                    await this.CommitAllChanges();
                }
                return ResponseModel.Success("Registration completed Successfully!");
            }
            catch (Exception ex)
            {
                return ResponseModel.ServerInternalError(data: ex);
            }
        }
    }
}
