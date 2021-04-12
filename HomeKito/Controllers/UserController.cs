using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Barayand.Common.Services;
using Barayand.DAL.Interfaces;
using Barayand.Models.Entity;
using Barayand.OutModels.Miscellaneous;
using Barayand.OutModels.Response;
using Barayand.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace HomeKito.Controllers
{
    public class UserController : Controller
    {
        private readonly IMapper _mapper;
        private readonly IPublicMethodRepsoitory<DynamicPagesContent> _DynamicPageRepository;
        private readonly IPublicMethodRepsoitory<InvoiceModel> _invoicerepository;
        private readonly IPublicMethodRepsoitory<OrderModel> _orderrepository;
        private readonly IPublicMethodRepsoitory<ProductModel> _productrepository;
        private readonly IPublicMethodRepsoitory<OfflinRequestModel> _offreqrepository;
        private readonly IPublicMethodRepsoitory<TicketModel> _ticketrepo;
        private readonly IFavoriteRepository _favoritrepostory;
        private readonly ISmsService _smsservice;
        private readonly IUserRepository _repository;
        private readonly ILocalizationService _lang;
        private readonly IWalletHistoryRepository _walletrepository;
        private readonly IViewRenderer _viewRenderer;
        private readonly ISmsService _sms;
        public UserController(IMapper mapper, IUserRepository repository, IFavoriteRepository favoritrepostory, IPublicMethodRepsoitory<DynamicPagesContent> DynamicPageRepository, IPublicMethodRepsoitory<InvoiceModel> invoicerepository, IWalletHistoryRepository walletHistoryRepository, IPublicMethodRepsoitory<OfflinRequestModel> offreqrepository, ISmsService smsService, ILocalizationService lang, IPublicMethodRepsoitory<OrderModel> orderrepo, IPublicMethodRepsoitory<ProductModel> productrepo, IPublicMethodRepsoitory<TicketModel> ticketrepo, IViewRenderer viewRenderer, ISmsService sms)
        {
            this._repository = repository;
            this._mapper = mapper;
            _DynamicPageRepository = DynamicPageRepository;
            _invoicerepository = invoicerepository;
            _walletrepository = walletHistoryRepository;
            _offreqrepository = offreqrepository;
            _orderrepository = orderrepo;
            _favoritrepostory = favoritrepostory;
            _productrepository = productrepo;
            _ticketrepo = ticketrepo;
            _smsservice = smsService;
            _lang = lang;
            _viewRenderer = viewRenderer;
            _sms = sms;
        }
        [Route("User")]
        public IActionResult Index(string redirect = null)
        {
            return View();
        }
        [Route("ConfirmUser")]
        public async Task<IActionResult> ConfirmUser(string mobile)
        {
            if (!string.IsNullOrEmpty(mobile))
            {

                string rnd =  UtilesService.RandomDigit(5);
                await _sms.Otp(mobile, rnd);
                TempData["rnd"] = rnd;
                TempData["mobile"] = mobile;
                return View();
            }
            return RedirectToAction("Index", "Home");
        }
        [Route("RegisterLogin")]
        public async Task<IActionResult> RegisterLogin(string code)
        {
            if (!string.IsNullOrEmpty(code))
            {
                string rnd = TempData["rnd"].ToString();
                string mob = TempData["mobile"].ToString();
                if(rnd != code)
                {
                    return new JsonResult(ResponseModel.Error("کد وارد شده صحیح نمیباشد"));
                }

                var User =((List<UserModel>)((await _repository.GetAll()).Data)).FirstOrDefault(x => x.U_Phone == (string)mob);

                if (User != null)
                {
                    var result = await _repository.UserLogin(User);

                    if (result.Status == false)
                    {
                        return new JsonResult(ResponseModel.Error(result.Msg));
                    }
                    var loginuser = ((UserModel)result.Data);
                    Response.Cookies.Append("HomeKitoUser", loginuser.Token);

                    return new JsonResult(ResponseModel.Success(msg:"ok"));
                }
                else
                {
                    UserModel um = new UserModel();
                    um.U_Role = 2;
                    um.U_Name = "";
                    um.U_Family = "";
                    um.U_Email = "ram@gmail.com";
                    um.U_Phone = mob;
                    um.U_Password = "homekito";
                    var result = await _repository.Insert(um);
                    if (result.Status)
                    {
                        var loginuser = ((UserModel)(await _repository.UserLogin(um)).Data);
                        Response.Cookies.Append("HomeKitoUser", loginuser.Token);
                        var view = await _viewRenderer.RenderAsync(this, "_RegisterSuccess", mob);
                        return new JsonResult(ResponseModel.Success(data:view,msg:null));
                    }
                    await _sms.SignUp(mob, rnd);
                    //return new JsonResult(result.Msg);
                    return new JsonResult(ResponseModel.Error(result.Msg));
                }
            }
            return RedirectToAction("Index", "Home");
        }
        [Route("User/Profile")]
        public async Task<IActionResult> Profile()
        {
            try
            {
               
                var authorize = Barayand.Common.Services.TokenService.AuthorizeUser(Request);
                if (authorize < 1)
                {
                    if (authorize == 0)
                    {
                        Response.Cookies.Delete("HomeKitoUser");
                    }
                    return RedirectToAction("Index", new { redirect = "/User/Profile" });
                }

                UserModel userModel = await _repository.GetById(authorize);
                if (userModel == null)
                {
                    return RedirectToAction("Index", new { redirect = "/User/Profile" });
                }
                List<InvoiceModel> invoices = ((List<InvoiceModel>)(await _invoicerepository.GetAll()).Data);
                invoices = (invoices != null) ? invoices.Where(x => x.I_Status > 0 && x.I_UserId == authorize).ToList() : null;
                List<InvoiceModel> FivaLastInvoices = new List<InvoiceModel>();

                List<OfflinRequestModel> OfflineRequests = ((List<OfflinRequestModel>)(await _offreqrepository.GetAll()).Data).Where(x=>x.O_User == authorize).ToList();
                if (invoices != null)
                {
                    FivaLastInvoices = invoices.Take(5).ToList();
                }
                else
                {
                    FivaLastInvoices = null;
                }
                ViewBag.Favorites = await _favoritrepostory.GetByUser(authorize);
                ViewBag.WalletHistory = await _walletrepository.GetAllUserTransactions(authorize);
                ViewBag.LastInvoices = FivaLastInvoices;
                ViewBag.UserInvoices = invoices;
                ViewBag.OfflineRequests = OfflineRequests;
                return View(userModel);
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        [Route("User/AddFavorite")]
        [HttpPost]
        public async Task<IActionResult> AddFavorite([FromBody] FavoriteModel favoriteModel)
        {
            try
            {
                var authorize = Barayand.Common.Services.TokenService.AuthorizeUser(Request);
                if (authorize < 1)
                {
                    return new JsonResult(ResponseModel.Error("Your login token has expired."));
                }
                favoriteModel.F_UserId = authorize;
                return new JsonResult(await _favoritrepostory.Insert(favoriteModel));
            }
            catch (Exception ex)
            {
                return null;
            }
        }
        [Route("User/RemoveFavorite/{id}")]
        [HttpPost]
        public async Task<IActionResult> RemoveFavorite(int id)
        {
            try
            {
                var authorize = Barayand.Common.Services.TokenService.AuthorizeUser(Request);
                if (authorize < 1)
                {
                    return new JsonResult(ResponseModel.Error("Your login token has expired."));
                }
                var Favs = ((List<FavoriteModel>)(await _favoritrepostory.GetAll()).Data).FirstOrDefault(x=>x.F_UserId == authorize && x.F_EntityId == id);
                if(Favs == null)
                {
                    return new JsonResult(ResponseModel.Error("Your login token has expired."));
                }
                return new JsonResult(await _favoritrepostory.Delete(Favs));
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        [Route("User/RemoveFavoriteUser/{id}")]
        [HttpPost]
        public async Task<IActionResult> RemoveFavoriteUser(int id)
        {
            try
            {
                var authorize = Barayand.Common.Services.TokenService.AuthorizeUser(Request);
                if (authorize < 1)
                {
                    return new JsonResult(ResponseModel.Error("Your login token has expired."));
                }

                return new JsonResult(await _favoritrepostory.Delete(id));
            }
            catch (Exception ex)
            {
                return null;
            }
        }
        [Route("/User/Ticket/{type}/{entity}")]
        public async Task<IActionResult> Ticket(string type , decimal entity)
        {
            try
            {
                var authorize = Barayand.Common.Services.TokenService.AuthorizeUser(Request);
                if (authorize < 1)
                {
                    if (authorize == 0)
                    {
                        Response.Cookies.Delete("HomekitoUser");
                    }
                    return RedirectToAction("Index", new { redirect = "/User/Profile" });
                }
                var allTickets = ((List<TicketModel>)(await _ticketrepo.GetAll()).Data).Where(x=>x.T_Cid == entity).ToList();
                ViewBag.User = authorize;
                ViewBag.entity =  entity;
                return View(allTickets);
            }
            catch(Exception ex)
            {
                return RedirectToAction("Index","User");
            }
        }
        [Route("User/Update")]
        [HttpPost]
        public async Task<IActionResult> UpdateProfile(UserModel user)
        {
            try
            {
                var authorize = Barayand.Common.Services.TokenService.AuthorizeUser(Request);
                if (authorize < 1)
                {
                    if (authorize == 0)
                    {
                        Response.Cookies.Delete("HomeKitoUser");
                    }
                    return new JsonResult(ResponseModel.Error("Your login token has expired.", new { EXPIREDTOKEN = true }));
                }
                return new JsonResult(await _repository.UpdateProfile(Request, authorize));
            }
            catch (Exception ex)
            {
                return null;
            }
        }
        [Route("User/UpdatePassword")]
        [HttpPost]
        public async Task<IActionResult> UpdatePassword()
        {
            try
            {
                var authorize = Barayand.Common.Services.TokenService.AuthorizeUser(Request);
                if (authorize < 1)
                {
                    if (authorize == 0)
                    {
                        Response.Cookies.Delete("Homekitouser");
                    }
                    return new JsonResult(ResponseModel.Error("Your login token has expired.", new { EXPIREDTOKEN = true }));
                }
                return new JsonResult(await _repository.UpdatePassword(Request, Response, authorize));
            }
            catch (Exception ex)
            {
                return null;
            }
        }
        [Route("User/LoadOrderProducts/{oid}")]
        [HttpPost]
        public async Task<IActionResult> LoadOrderProducts(string oid)
        {
            try
            {
                var authorize = Barayand.Common.Services.TokenService.AuthorizeUser(Request);
                if (authorize < 1)
                {
                    if (authorize == 0)
                    {
                        Response.Cookies.Delete("HomekitoUser");
                    }
                    return new JsonResult(ResponseModel.Error("Your login token has expired.", new { EXPIREDTOKEN = true }));
                }
                var orders = ((List<OrderModel>)(await _orderrepository.GetAll()).Data).Where(x=>x.O_ReciptId == oid);
                var rinfo = ((List<InvoiceModel>)(await _invoicerepository.GetAll()).Data).FirstOrDefault(x => x.I_Id == oid);
                List<object> Products = new List<object>();
                bool hasHcopy = false;
                int i = 1;
                foreach(var item in orders)
                {
                    var prd = await _productrepository.GetById(item.O_ProductId);
                    Products.Add(new {
                        counter = i,
                        prodname = prd.P_Title,
                        quantity = item.O_Quantity+" عدد",
                        total = rinfo.I_TotalProductAmount,
                        price = item.O_Price,
                        version = (item.O_Version == 1)? "PDF":"HCOPY",
                        downlink = prd.P_DownloadLink
                    });
                    i++;
                    if(item.O_Version == 2)
                    {
                        hasHcopy = true;
                    }
                }
                var reciverinfo = "";
                if(hasHcopy)
                {
                   
                    if(rinfo != null)
                    {
                        reciverinfo = rinfo.I_RecipientInfo;
                    }
                }
                return new JsonResult(ResponseModel.Success(data:new {hashcopy = hasHcopy,recieptinfo = reciverinfo,products = Products }));
            }
            catch (Exception ex)
            {
                return null;
            }
        }
    }
}
