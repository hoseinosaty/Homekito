using AutoMapper;
using Barayand.Common.Services;
using Barayand.DAL.Interfaces;
using Barayand.Models.Entity;
using Barayand.OutModels.Miscellaneous;
using Barayand.OutModels.Response;
using Barayand.Services.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore.Internal;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Primitives;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Barayand.Services.Services
{
    public class BasketServicegbook : IBasketService
    {
        private readonly IPublicMethodRepsoitory<ProductModel> _productrepo;
        private readonly IPublicMethodRepsoitory<CopponModel> _couponrepo;
        private readonly IUserRepository _userrepository;
        private readonly IWalletHistoryRepository _walletrepository;
        private readonly ILogger<BasketService> _logger;
        public readonly IPublicMethodRepsoitory<InvoiceModel> _invoicerepository;
        public readonly IPublicMethodRepsoitory<OrderModel> _orderrepository;
        public readonly IPublicMethodRepsoitory<OptionsModel> _optionrepository;
        private readonly ILocalizationService _lang;
        private readonly ISmsService _smsService;
        private readonly IPriceCalculatorService _priceCalculator;
        private readonly IViewRenderer renderer;
        private int CVRT = 1000;
        private int BINPERC = 1;
        public List<ProductModel> AllProducts;
        public BasketServicegbook(ILogger<BasketService> logger, IPublicMethodRepsoitory<ProductModel> productrepo, IPublicMethodRepsoitory<CopponModel> couponrepo, IUserRepository userRepository, IWalletHistoryRepository walletHistoryRepository, ILocalizationService lang, IViewRenderer viewRenderer, IPublicMethodRepsoitory<InvoiceModel> invoicerepo, IPublicMethodRepsoitory<OrderModel> orderrepository, ISmsService smsService, IPriceCalculatorService priceCalculator, IPublicMethodRepsoitory<OptionsModel> optionrepository)
        {
            this._logger = logger;
            this._productrepo = productrepo;
            this._couponrepo = couponrepo;
            this._userrepository = userRepository;
            this._walletrepository = walletHistoryRepository;
            this._invoicerepository = invoicerepo;
            this._orderrepository = orderrepository;
            this._smsService = smsService;
            this._priceCalculator = priceCalculator;
            this._optionrepository = optionrepository;
            this.Initilize();
            renderer = viewRenderer;
            _lang = lang;

        }
        public async Task Initilize()
        {
            var cvrtbase = ((List<OptionsModel>)(await _optionrepository.GetAll()).Data).FirstOrDefault(x => x.O_Key == "CVRTTOMANTOPOINT");
            var bin = ((List<OptionsModel>)(await _optionrepository.GetAll()).Data).FirstOrDefault(x => x.O_Key == "GIFTBINPERCENTAGE");
            if (cvrtbase != null)
            {
                CVRT = int.Parse(cvrtbase.O_Value);
            }
            if (bin != null)
            {
                BINPERC = int.Parse(bin.O_Value);
            }
        }
        public async Task<ResponseStructure> AddToCart(HttpRequest httpRequest, HttpResponse httpResponse)
        {
            try
            {
                AllProducts = ((List<ProductModel>)(await _productrepo.GetAll()).Data);
                StringValues data;
                StringValues PrintAble ;
                if (!httpRequest.Headers.TryGetValue("AddToCart", out data))
                {
                    return ResponseModel.Error("Invalid access detect.");
                }
                if (!httpRequest.Headers.TryGetValue("PrintAble", out PrintAble))
                {
                    return ResponseModel.Error("Invalid access detect.");
                }
                var dec = Barayand.Common.Services.CryptoJsService.DecryptStringAES(data);
                BasketItem rm = JsonConvert.DeserializeObject<BasketItem>(dec);
                var findProduct = AllProducts.FirstOrDefault(x => x.P_Id == rm.ProductId);
                if (findProduct == null)
                {
                    return ResponseModel.Error("Invalid access detect.");
                }
                var product = new ProductBasketModel() {
                    P_BinPrice = findProduct.P_BinPrice,
                    P_Code = findProduct.P_Code,
                    P_Discount = findProduct.P_Discount,
                    P_DiscountPeriodTime = findProduct.P_DiscountPeriodTime,
                    P_DiscountType = findProduct.P_DiscountType,
                    P_ExternalPrice = findProduct.P_ExternalPrice,
                    P_GiftBin = findProduct.P_GiftBin,
                    P_Id = findProduct.P_Id,
                    P_Image = findProduct.P_Image,
                    P_PageCount = findProduct.P_PageCount,
                    P_PeriodDiscountPrice = findProduct.P_PeriodDiscountPrice,
                    P_PeriodDiscountPriceType = findProduct.P_PeriodDiscountPriceType,
                    P_PeriodPrintableFomrulaId = findProduct.P_PeriodPrintableFomrulaId,
                    P_PeriodPrintablePrice = findProduct.P_PeriodPrintablePrice,
                    P_PeriodPrintablePriceType = findProduct.P_PeriodPrintablePriceType,
                    P_Price = findProduct.P_Price,
                    P_PriceFormulaId = findProduct.P_PriceFormulaId,
                    P_PriceType = findProduct.P_PriceType,
                    P_PrintAbleVerFormulaId = findProduct.P_PrintAbleVerFormulaId,
                    P_PrintAbleVerPrice = findProduct.P_PrintAbleVerPrice,
                    P_PrintAbleVerPriceType = findProduct.P_PrintAbleVerPriceType,
                    P_PrintAbleVersion = findProduct.P_PrintAbleVersion,
                    P_PriodDiscountFormulaId = findProduct.P_PriodDiscountFormulaId,
                    P_Title = findProduct.P_Title,
                    P_Type = findProduct.P_Type,
                    P_Weight = findProduct.P_Weight,
                    PriceModel = await _priceCalculator.CalculateBookPrice(findProduct.P_Id,_lang.GetLang())
                };
                rm.PrintAble = bool.Parse(PrintAble); 
                BasketModel basket = new BasketModel();
                string cookie;
                if (httpRequest.Cookies.TryGetValue("Cart", out cookie))
                {
                    if (cookie != null)
                    {
                        var basketInfo = Barayand.Common.Services.CryptoJsService.DecryptStringAES(cookie);
                        basket = JsonConvert.DeserializeObject<BasketModel>(basketInfo);
                        if (basket.CartItems.Count > 0)
                        {
                            if (basket.CartItems.FirstOrDefault(x => x.Product.P_Type != product.P_Type) != null)
                            {
                                return ResponseModel.Error("You can only add a type of product (Book or Art or Learning trains) in same time.");
                            }
                        }
                        if(bool.Parse(PrintAble) == false)
                        {
                            var checkDuplicatePdf = basket.CartItems.FirstOrDefault(x => x.Product.P_Id == rm.ProductId && x.PrintAble == false);
                            if (checkDuplicatePdf != null)
                            {
                                return ResponseModel.Error("از هر کتاب شما تنها میتوانید یک نسخه PDF سفارش دهید");
                            }
                        }
                        var existsProduct = basket.CartItems.FirstOrDefault(x => x.Product.P_Id == rm.ProductId && x.PrintAble == bool.Parse(PrintAble));
                        if (existsProduct != null)
                        {
                            existsProduct.Quantity += rm.Quantity;
                        }
                        else
                        {
                            rm.Product = product;
                            basket.CartItems.Add(rm);
                        }

                    }
                    else
                    {
                        rm.Product = product;
                        basket.CartItems.Add(rm);
                    }
                }
                else
                {
                    rm.Product = product;
                    basket.CartItems.Add(rm);
                    basket.OrderDate = DateTime.Now;
                }
                
                string token = Barayand.Common.Services.CryptoJsService.EncryptStringToAES(JsonConvert.SerializeObject(basket));
                httpResponse.Cookies.Delete("Cart");
                httpResponse.Cookies.Append("Cart", token);
                return ResponseModel.Success("Product added.");
            }
            catch(Exception ex)
            {
                _logger.LogError("Error in adding entity to customer basket",ex);
                return ResponseModel.ServerInternalError(data:ex);
            }
        }

        public async Task<ResponseStructure> FreeUpCart(HttpRequest httpRequest, HttpResponse httpResponse)
        {
            try
            {
                StringValues data;
                BasketModel basket = new BasketModel();
                string cookie;
                if (httpRequest.Cookies.TryGetValue("Cart", out cookie))
                {
                    if (cookie != null)
                    {
                        httpResponse.Cookies.Delete("Cart");
                        string token = Barayand.Common.Services.CryptoJsService.EncryptStringToAES(JsonConvert.SerializeObject(basket));

                        httpResponse.Cookies.Append("Cart", token);
                    }
                }
                return ResponseModel.Success("Cart free.");
            }
            catch (Exception ex)
            {
                _logger.LogError("Error in deleting  customer basket", ex);
                return ResponseModel.ServerInternalError(data: ex);
            }
        }

        public async Task<BasketModel> GetBasketItems(HttpRequest httpRequest)
        {
            BasketModel basket = new BasketModel();
            try
            {
                string cookie;
                if (httpRequest.Cookies.TryGetValue("Cart", out cookie))
                {
                    if (cookie != null)
                    {
                        var basketInfo = Barayand.Common.Services.CryptoJsService.DecryptStringAES(cookie);
                        basket = JsonConvert.DeserializeObject<BasketModel>(basketInfo);
                        foreach(var item in basket.CartItems)
                        {
                            item.Product.PriceModel =await _priceCalculator.CalculateBookPrice(item.Product.P_Id,_lang.GetLang());
                        }
                    }
                }
                return basket;
            }
            catch (Exception ex)
            {
                _logger.LogError("Error in adding entity to customer basket", ex);
                return basket;
            }
        }

        public async Task<ResponseStructure> RemoveCartItem(HttpRequest httpRequest, HttpResponse httpResponse)
        {
            try
            {
                StringValues data;
                if (!httpRequest.Headers.TryGetValue("RemoveCartItem", out data))
                {
                    return ResponseModel.Error("Invalid access detect.");
                }
                var dec = Barayand.Common.Services.CryptoJsService.DecryptStringAES(data);
                BasketItem rm = JsonConvert.DeserializeObject<BasketItem>(dec);

                BasketModel basket = new BasketModel();
                string cookie;
                if (httpRequest.Cookies.TryGetValue("Cart", out cookie))
                {
                    if (cookie != null)
                    {
                        var basketInfo = Barayand.Common.Services.CryptoJsService.DecryptStringAES(cookie);
                        basket = JsonConvert.DeserializeObject<BasketModel>(basketInfo);
                        var item = basket.CartItems.FirstOrDefault(x=>x.PrintAble == rm.PrintAble && x.Product.P_Id == rm.ProductId);
                        if(item != null)
                        {
                            basket.CartItems.Remove(item);
                        }


                    }
                }
                httpResponse.Cookies.Delete("Cart");

                if (basket.CartItems.Count > 0)
                {
                    string token = Barayand.Common.Services.CryptoJsService.EncryptStringToAES(JsonConvert.SerializeObject(basket));

                    httpResponse.Cookies.Append("Cart", token);
                }
                return ResponseModel.Success("Product removed.");
            }
            catch (Exception ex)
            {
                _logger.LogError("Error in remove entity from customer basket", ex);
                return ResponseModel.ServerInternalError(data: ex);
            }
        }

        public async Task<ResponseStructure> SaveReciptientInfo(HttpRequest httpRequest, HttpResponse httpResponse)
        {
            try
            {
                StringValues data;
                if (!httpRequest.Headers.TryGetValue("ReciptientInfo", out data))
                {
                    return ResponseModel.Error("Invalid access detect.");
                }
                var dec = Barayand.Common.Services.CryptoJsService.DecryptStringAES(data);
                ReciptientInfoModel rm = JsonConvert.DeserializeObject<ReciptientInfoModel>(dec);
                BasketModel basket = new BasketModel();
                string cookie;
                if (httpRequest.Cookies.TryGetValue("Cart", out cookie))
                {
                    if (cookie != null)
                    {
                        var basketInfo = Barayand.Common.Services.CryptoJsService.DecryptStringAES(cookie);
                        basket = JsonConvert.DeserializeObject<BasketModel>(basketInfo);
                        basket.RecipientInfo = rm;
                    }

                }

                string token = Barayand.Common.Services.CryptoJsService.EncryptStringToAES(JsonConvert.SerializeObject(basket));
                httpResponse.Cookies.Delete("Cart");
                httpResponse.Cookies.Append("Cart", token);
                return ResponseModel.Success("Reciptient information saved.");
            }
            catch (Exception ex)
            {
                _logger.LogError("Error in adding reciver info to customer basket", ex);
                return ResponseModel.ServerInternalError(data: ex);
            }
        }

        public async Task<ResponseStructure> UseCoppon(HttpRequest httpRequest, HttpResponse httpResponse)
        {
            try
            {
                StringValues data;
                if (!httpRequest.Headers.TryGetValue("CouponInfo", out data))
                {
                    return ResponseModel.Error("Invalid access detect.");
                }
                var dec = Barayand.Common.Services.CryptoJsService.DecryptStringAES(data);
                BasketItem rm = JsonConvert.DeserializeObject<BasketItem>(dec);
                var coppon = ((List<CopponModel>)(await _couponrepo.GetAll()).Data).FirstOrDefault(x => x.CP_Code == rm.CopponCode);
                if (coppon == null)
                {
                    return ResponseModel.Error("Coppon code is invalid.");
                }
                if (DateTime.Now >= coppon.CP_EndDate || DateTime.Now < coppon.CP_StartDate)
                {
                    return ResponseModel.Error("Coppon has been expired.");
                }
                BasketModel basket = new BasketModel();
                string cookie;
                if (httpRequest.Cookies.TryGetValue("Cart", out cookie))
                {
                    if (cookie != null)
                    {
                        var basketInfo = Barayand.Common.Services.CryptoJsService.DecryptStringAES(cookie);
                        basket = JsonConvert.DeserializeObject<BasketModel>(basketInfo);
                        if (basket.Coppon.Count > 0)
                        {
                            return ResponseModel.Error("Coppon was applied before.");
                        }
                        if (basket.Coppon.Count(x => x.CP_Code == coppon.CP_Code) > 0)
                        {
                            return ResponseModel.Error("Coppon was applied before.");
                        }
                        basket.Coppon.Add(coppon);
                    }

                }

                string token = Barayand.Common.Services.CryptoJsService.EncryptStringToAES(JsonConvert.SerializeObject(basket));
                httpResponse.Cookies.Delete("Cart");
                httpResponse.Cookies.Append("Cart", token);
                return ResponseModel.Success("Product added.");
                return ResponseModel.Success();
            }
            catch (Exception ex)
            {
                _logger.LogError("Error in adding coupon to customer basket", ex);
                return ResponseModel.ServerInternalError(data: ex);
            }
        }

        public async Task<ResponseStructure> TestCheckout(HttpRequest httpRequest, HttpResponse httpResponse,int type = 1)
        {
            try
            {
                var authorize = Barayand.Common.Services.TokenService.AuthorizeUser(httpRequest);
                if (authorize < 1)
                {
                    return ResponseModel.Error("User not logged in.");
                }
                var basket = await GetBasketItems(httpRequest);
                if (basket.CartItems.Count < 1)
                {
                    return ResponseModel.Error("Basket is empty.");
                }
                UserModel userModel = await _userrepository.GetById(authorize);
                decimal wll = userModel.U_Wallet;
                decimal Sum = basket.BasketTotalAmount(_lang.GetLang(),CVRT);
                InvoiceModel invoice = new InvoiceModel();
                invoice.I_TotalAmount = basket.BasketTotalAmount(_lang.GetLang(), CVRT);
                invoice.I_Id = UtilesService.RandomDigit(12);
                invoice.I_UserId = authorize;
                invoice.I_ShippingCost = basket.ShippingCost;
                invoice.I_RecipientInfo = JsonConvert.SerializeObject(basket.RecipientInfo);
                invoice.I_CopponDiscount = basket.SumDiscount();
                invoice.I_CopponId = (basket.Coppon.Count > 0) ? basket.Coppon.FirstOrDefault().CP_Id : 0;
                invoice.Created_At = DateTime.Now;
                invoice.I_PaymentDate = DateTime.Now;
                invoice.I_TotalProductAmount = basket.BasketTotalProductPrice(_lang.GetLang(),CVRT);
                invoice.I_PaymentInfo = "Test payment";
                invoice.I_PaymentType = type;

                
                if(type == 2)
                {
                    decimal w = (_lang.GetLang() == "fa") ?userModel.U_Wallet : userModel.U_Wallet / CVRT;
                    if(w < basket.BasketTotalAmount(_lang.GetLang(), CVRT))
                    {
                        return ResponseModel.Error("موجودی کیف پول شما کافی نمیباشد");
                    }
                    else
                    {
                        w = w - basket.BasketTotalAmount(_lang.GetLang(), CVRT);
                        await _walletrepository.DecreaseWallet(userModel.U_Id,basket.BasketTotalAmount("fa", CVRT));
                        await _smsService.WalletAllert(userModel.U_Phone,2, basket.BasketTotalAmount(_lang.GetLang(), CVRT).ToString("#,# تومان"));
                        invoice.I_Status = 2;
                    }
                }
                if (type == 3)
                {
                    decimal w =  userModel.U_Coupon ;
                    if (w < basket.BasketProductBinPriceTotal(BINPERC))
                    {
                        return ResponseModel.Error("تعداد بن های شما برای پرداخت کافی نمیباشد");
                    }
                    else
                    {
                        userModel.U_Coupon = userModel.U_Coupon - basket.BasketProductBinPriceTotal(BINPERC);
                        await _userrepository.Update(userModel);
                        invoice.I_Status = 2;
                    }
                }
                if(type == 4)
                {
                    invoice.I_Status = 1;
                }
                if (type == 1)
                {
                    invoice.I_Status = 2;
                }
                ResponseStructure res = await _invoicerepository.Insert(invoice);
                if (!res.Status)
                {
                    return res;
                }
                int sumGiftBon = basket.BasketProductBinPriceTotal(BINPERC);
                foreach (var item in basket.CartItems)
                {
                    //sumGiftBon += (item.Product.P_GiftBin * item.Quantity);
                    var orderres = await _orderrepository.Insert(new OrderModel()
                    {
                        O_Discount = (!item.PrintAble) ? basket.BasketProductPrice(item.Product.P_Id, _lang.GetLang(), false, false) : basket.BasketProductPrintablePrice(item.Product.P_Id, _lang.GetLang(), false, false),
                        O_DiscountType = item.Product.P_DiscountType,
                        O_Price = (!item.PrintAble) ? basket.BasketProductPrice(item.Product.P_Id, _lang.GetLang(), true, false) : basket.BasketProductPrintablePrice(item.Product.P_Id, _lang.GetLang(), true, false),
                        O_Quantity = item.Quantity,
                        O_ProductId = item.Product.P_Id,
                        O_ReciptId = invoice.I_Id,
                        Created_At = DateTime.Now,
                        Lang = _lang.GetLang(),
                        O_Version = item.PrintAble ? 2 : 1
                        
                    });
                }
                userModel.U_Coupon += sumGiftBon;
                await _userrepository.Update(userModel);
                await this.FreeUpCart(httpRequest, httpResponse);
                if(_lang.GetLang() == "fa")
                {
                    await _smsService.OrderAlert(userModel.U_Phone, invoice.I_Id, basket.BasketTotalAmount(_lang.GetLang(),CVRT).ToString("#,# تومان"));
                }
                
                return ResponseModel.Success("سفارش شما با موفقیت ثبت گردید",new {invoiceid = invoice.I_Id });
                

            }
            catch (Exception ex)
            {
                return ResponseModel.ServerInternalError(data: ex);
            }
        }
    }
}
