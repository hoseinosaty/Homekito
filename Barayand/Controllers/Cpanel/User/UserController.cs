using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Barayand.DAL.Interfaces;
using Barayand.Models.Entity;
using Barayand.OutModels.Miscellaneous;
using Barayand.OutModels.Models;
using Barayand.OutModels.Response;
using Barayand.Services.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace Barayand.Controllers.Cpanel.User
{
    [Route("api/cpanel/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IAddressRepository _addressrepositoy;
        private readonly IUserRepository _userrepo;
        private readonly ISmsService _smsService;
        private readonly IWalletHistoryRepository _walletrepo;
        private readonly ILogger<UserController> _logger;
        public UserController(ILogger<UserController> logger, IUserRepository userRepository, ISmsService smsService, IWalletHistoryRepository walletrepo, IAddressRepository addressRepository)
        {
            _logger = logger;
            _smsService = smsService;
            _walletrepo = walletrepo;
            _userrepo = userRepository;
            _addressrepositoy = addressRepository;
        }
        [Route("AddUser")]
        [HttpPost]
        public async Task<ActionResult> AddUser(UserModel um)
        {
            try
            {
                um.U_Role = 1;
                var result = await _userrepo.Insert(um);
                if (result.Status)
                {
                    if (um.U_Phone != "")
                    {
                        await _smsService.SignUp(um.U_Phone, um.surename);
                    }
                }
                return new JsonResult(result);
            }
            catch (Exception ex)
            {
                _logger.LogError("Erro in user controller", ex);
                return new JsonResult(ResponseModel.ServerInternalError(data: ex));
            }
        }
        [Route("UpdateUser")]
        [HttpPost]
        public async Task<ActionResult> UpdateUser(UserModel um)
        {
            try
            {
                um.U_Role = 1;
                return new JsonResult(await _userrepo.UpdateUserAdmin(um));
            }
            catch (Exception ex)
            {
                _logger.LogError("Erro in user controller", ex);
                return new JsonResult(ResponseModel.ServerInternalError(data: ex));
            }
        }
        [Route("SuspendUser")]
        [HttpPost]
        public async Task<ActionResult> SuspendUser(UserModel um)
        {
            try
            {
                um.U_Role = 1;
                return new JsonResult(await _userrepo.SuspendUser(um));
            }
            catch (Exception ex)
            {
                _logger.LogError("Erro in user controller", ex);
                return new JsonResult(ResponseModel.ServerInternalError(data: ex));
            }
        }
        [Route("LoadWalletHistory")]
        [HttpPost]
        public async Task<ActionResult> LoadUserWalletHistory(UserModel um)
        {
            try
            {
                var allHistories = (List<WalletHistoryModel>)(await _walletrepo.GetAll()).Data;
                allHistories = allHistories.Where(x => x.WH_CustomerId == um.U_Id).ToList();
                List<object> result = new List<object>();
                int i = 1;
                foreach (var item in allHistories)
                {
                    result.Add(new
                    {
                        counter = i,
                        amount = item.WH_Amount,
                        type = item.WH_TransactionType == 1 ? "واریز" : "برداشت",
                        user = item.WH_AdderId == 0 ? "مدیر سیستم" : "کاربر",
                        date = ((DateTime)item.Created_At).ToString("yyyy/MM/dd HH:mm:ss")
                    });
                    i++;
                }
                return new JsonResult(ResponseModel.Success("Wallet history returned", result));
            }
            catch (Exception ex)
            {
                return new JsonResult(ResponseModel.ServerInternalError(data: ex));
            }
        }
        [Route("ActiveUser")]
        [HttpPost]
        public async Task<ActionResult> ActiveUser(UserModel um)
        {
            try
            {
                um.U_Role = 1;
                return new JsonResult(await _userrepo.ActiveUser(um));
            }
            catch (Exception ex)
            {
                _logger.LogError("Erro in user controller", ex);
                return new JsonResult(ResponseModel.ServerInternalError(data: ex));
            }
        }
        [Route("Login")]
        [HttpPost]
        public async Task<ActionResult> Login(UserModel um)
        {
            try
            {
                um.U_Role = 1;
                var login = await _userrepo.AdminLogin(um);
                if (!login.Status)
                {
                    return new JsonResult(login);
                }
                var token = ((UserModel)login.Data).Token;
                Response.Cookies.Append("BarayandCPUser", token);
                return new JsonResult(ResponseModel.Success("Login successfull.", new
                {
                    un = ((UserModel)login.Data).U_Name,
                    uln = ((UserModel)login.Data).U_Family,
                    ue = ((UserModel)login.Data).U_Email,
                    up = ((UserModel)login.Data).U_Phone,
                    ua = ((UserModel)login.Data).U_Avatar,
                    uid = ((UserModel)login.Data).U_Id,
                    perms = ((UserModel)login.Data).Permissions
                }));
            }
            catch (Exception ex)
            {
                _logger.LogError("Error in user login", ex);
                return new JsonResult(ResponseModel.ServerInternalError(data: ex));
            }
        }
        [Route("LoadUsers/{type?}")]
        [HttpPost]
        public async Task<ActionResult> LoadUsers(int type = 1)
        {
            try
            {
                if (type == 1)
                {
                    return new JsonResult(await _userrepo.GetAllAdmins());
                }
                else
                {
                    var csms = ((List<UserModel>)(await _userrepo.GetAll()).Data).Where(x => x.U_Role == 2).ToList();
                    csms.ForEach(async x => x.UserAdresses = await _addressrepositoy.GetUserActiveAddress(x.U_Id));
                    return new JsonResult(ResponseModel.Success(data: csms));
                }
            }
            catch (Exception ex)
            {
                _logger.LogError("Erro in user controller", ex);
                return new JsonResult(ResponseModel.ServerInternalError(data: ex));
            }
        }
        [Route("ChargeWallet")]
        [HttpPost]
        public async Task<ActionResult> ChargeWallet(UserModel um)
        {
            try
            {
                var user = await _userrepo.GetById(um.U_Id);
                if (user == null)
                {
                    return new JsonResult(ResponseModel.Error("کاربر یافت نشد"));
                }
                var doTransaction = await _walletrepo.RechargeWallet(user.U_Id, um.U_Wallet);
                if (doTransaction.Status)
                {
                    if (user.U_Phone != null && user.U_Phone != "")
                    {
                        await _smsService.WalletAllert(user.U_Phone, 1, um.U_Wallet.ToString("#,#"));
                    }
                    return new JsonResult(ResponseModel.Success("عملیات با موفقیت انجام گردید"));
                }
                else
                {
                    return new JsonResult(doTransaction);
                }

            }
            catch (Exception ex)
            {
                _logger.LogError("Erro in user controller", ex);
                return new JsonResult(ResponseModel.ServerInternalError(data: ex));
            }
        }
        [Route("ReduceWallet")]
        [HttpPost]
        public async Task<ActionResult> ReduceWallet(UserModel um)
        {
            try
            {
                var user = await _userrepo.GetById(um.U_Id);
                if (user == null)
                {
                    return new JsonResult(ResponseModel.Error("کاربر یافت نشد"));
                }
                var doTransaction = await _walletrepo.DecreaseWallet(user.U_Id, um.U_Wallet);
                if (doTransaction.Status)
                {
                    if (user.U_Phone != null && user.U_Phone != "")
                    {
                        await _smsService.WalletAllert(user.U_Phone, 2, um.U_Wallet.ToString("#,#"));
                    }
                    return new JsonResult(ResponseModel.Success("عملیات با موفقیت انجام گردید"));
                }
                else
                {
                    return new JsonResult(doTransaction);
                }

            }
            catch (Exception ex)
            {
                _logger.LogError("Erro in user controller", ex);
                return new JsonResult(ResponseModel.ServerInternalError(data: ex));
            }
        }
        [Route("AddAddress")]
        [HttpPost]
        public async Task<ActionResult> AddAddress(AddressModel um)
        {
            try
            {
                var result = await _addressrepositoy.Insert(um);
                
                return new JsonResult(result);
            }
            catch (Exception ex)
            {
                _logger.LogError("Erro in user controller", ex);
                return new JsonResult(ResponseModel.ServerInternalError(data: ex));
            }
        }
        [Route("DeleteAddress")]
        [HttpPost]
        public async Task<ActionResult> DeleteAddress(AddressModel um)
        {
            try
            {
                var result = await _addressrepositoy.DeleteUserAddress(um.A_Id);
                
                return new JsonResult(result);
            }
            catch (Exception ex)
            {
                _logger.LogError("Erro in user controller", ex);
                return new JsonResult(ResponseModel.ServerInternalError(data: ex));
            }
        }
    }
}
