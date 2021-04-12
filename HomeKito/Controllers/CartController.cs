using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using Barayand.Common.Services;
using Barayand.DAL.Interfaces;
using Barayand.Models.Entity;
using Barayand.OutModels.Miscellaneous;
using Barayand.OutModels.Response;
using Barayand.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;

namespace HomeKito.Controllers
{
    public class CartController : Controller
    {
        public readonly IPublicMethodRepsoitory<ProductModel> _repository;
        public readonly IPublicMethodRepsoitory<UserModel> _userrepository;
        public readonly IPublicMethodRepsoitory<CopponModel> _copponrepository;
        public readonly IPublicMethodRepsoitory<InvoiceModel> _invoicerepository;
        public readonly IPublicMethodRepsoitory<OrderModel> _orderrepository;
        public readonly IPublicMethodRepsoitory<OptionsModel> _optionrepository;
        private readonly IPublicMethodRepsoitory<DynamicPagesContent> _DynamicPageRepository;
      //  private readonly IPaymentService _paymentService;
       // private readonly IWalletHistoryRepository _walletrepository;
        private readonly IViewRenderer renderer;
        private readonly IBasketService _basketservice;
        private readonly IMapper _mapper;
     //   private readonly IPriceCalculatorService _priceCalculator;
        public CartController(IPublicMethodRepsoitory<ProductModel> repository, IPublicMethodRepsoitory<UserModel> userrepository, IPublicMethodRepsoitory<CopponModel> copponrepository, /*IPaymentService paymentService,*/ IPublicMethodRepsoitory<InvoiceModel> invoicerepository, IPublicMethodRepsoitory<OrderModel> orderrepository, IPublicMethodRepsoitory<DynamicPagesContent> DynamicPageRepository, /*IWalletHistoryRepository walletHistoryRepository,*/ IViewRenderer viewRenderer, IBasketService basketService, IMapper mapper,/* IPriceCalculatorService priceCalculator,*/ IPublicMethodRepsoitory<OptionsModel> optionrepository)
        {
            _repository = repository;
            _userrepository = userrepository;
            _copponrepository = copponrepository;
           // _paymentService = paymentService;
            _invoicerepository = invoicerepository;
            _orderrepository = orderrepository;
            _DynamicPageRepository = DynamicPageRepository;
         //   _walletrepository = walletHistoryRepository;
            renderer = viewRenderer;
            _basketservice = basketService;
            _mapper = mapper;
            _optionrepository = optionrepository;
         //   _priceCalculator = priceCalculator;
        }
        [Route("Basket")]
        public async Task<IActionResult> Index()
        {
            var basket = await _basketservice.GetBasketItems(Request);
            ViewBag.LoggedIn = TokenService.AuthorizeUser(Request) > 0;
            ViewBag.User = await _userrepository.GetById(TokenService.AuthorizeUser(Request));
            return View(basket);
        }
        [Route("Cart/AddToCart")]
        [HttpPost]
        public async Task<IActionResult> Add()
        {
            try
            {
                return new JsonResult(await _basketservice.AddToCart(Request, Response));
            }
            catch (Exception ex)
            {
                return new JsonResult(ResponseModel.ServerInternalError());
            }
        }
        [Route("Cart/IncreaseQuanity")]
        [HttpPost]
        public async Task<IActionResult> IncreaseQuanity()
        {
            try
            {
                return new JsonResult(await _basketservice.IncreaseProductCount(Request, Response));
            }
            catch (Exception ex)
            {
                return new JsonResult(ResponseModel.ServerInternalError());
            }
        }        
        [Route("Cart/DecreaseQuanity")]
        [HttpPost]
        public async Task<IActionResult> DecreaseQuanity()
        {
            try
            {
                return new JsonResult(await _basketservice.DecreaseProductCount(Request, Response));
            }
            catch (Exception ex)
            {
                return new JsonResult(ResponseModel.ServerInternalError());
            }
        }

        [Route("Cart/UseCoppon")]
        [HttpPost]
        public async Task<IActionResult> useCoppon()
        {
            try
            {
                return new JsonResult(await _basketservice.UseCoppon(Request, Response));
            }
            catch (Exception ex)
            {
                return new JsonResult(ResponseModel.ServerInternalError());
            }
        }
        [Route("Cart/RemoveItem")]
        [HttpPost]
        public async Task<IActionResult> RemoveItem()
        {
            try
            {
                return new JsonResult(await _basketservice.RemoveCartItem(Request, Response));
            }
            catch (Exception ex)
            {
                return new JsonResult(ResponseModel.ServerInternalError());
            }
        }
        [Route("Cart/SaveReciptientInfo")]
        [HttpPost]
        public async Task<IActionResult> SaveReciptientInfo()
        {
            try
            {
                return new JsonResult(await _basketservice.SaveReciptientInfo(Request, Response));
            }
            catch (Exception ex)
            {
                return new JsonResult(ResponseModel.ServerInternalError());
            }
        }
        [Route("Cart/TestCheckout/{type?}/{requestedPOS?}")]
        [HttpPost]
        public async Task<IActionResult> TestCheckout(int type = 1,bool requestedPOS = false)
        {
            try
            {
                return new JsonResult(await _basketservice.TestCheckout(Request, Response,type,requestedPOS));
            }
            catch (Exception ex)
            {
                return new JsonResult(ResponseModel.ServerInternalError());
            }
        }

        [Route("cart/CheckBasket")]
        public async Task<IActionResult> CheckBasket()//step 1
        {
            var basket = await _basketservice.GetBasketItems(Request);
            ViewBag.LoggedIn = TokenService.AuthorizeUser(Request) > 0;
            ViewBag.User = await _userrepository.GetById(TokenService.AuthorizeUser(Request));
            return View(basket);
        }
        [Route("cart/ReciptionInfo")]
        public async Task<IActionResult> ReciptionInfo()//step 2
        {
            var basket = await _basketservice.GetBasketItems(Request);
            ViewBag.LoggedIn = TokenService.AuthorizeUser(Request) > 0;
            ViewBag.User = await _userrepository.GetById(TokenService.AuthorizeUser(Request));
            return View(basket);
        }
        [Route("cart/PaymentCheckout")]
        public async Task<IActionResult> PaymentCheckout()//step 3
        {
            var basket = await _basketservice.GetBasketItems(Request);
            ViewBag.LoggedIn = TokenService.AuthorizeUser(Request) > 0;
            ViewBag.User = await _userrepository.GetById(TokenService.AuthorizeUser(Request));
            return View(basket);
        }
        [Route("cart/PaymentSuccess/{code}")]
        public async Task<IActionResult> PaymentSuccess(string code)//step 4
        {
            var basket = await _basketservice.GetBasketItems(Request);
            ViewBag.LoggedIn = TokenService.AuthorizeUser(Request) > 0;
            ViewBag.User = await _userrepository.GetById(TokenService.AuthorizeUser(Request));
            ViewBag.Code = code;
            return View();
        }

        [Route("Cart/GetTotalBasketAmount")]
        [HttpPost]
        public async Task<IActionResult> GetTotalBasketAmount()
        {
            try
            {
                var basket = await _basketservice.GetBasketItems(Request);
                int count = basket.Products.Count();
               
               return new JsonResult(ResponseModel.Success("Basket items calculated.", new { count = count }));
            }
            catch (Exception ex)
            {
                return new JsonResult(ResponseModel.ServerInternalError());
            }
        }
    }
}
