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
    public class BasketService : IBasketService
    {
        private readonly IPublicMethodRepsoitory<ProductCombineModel> _productcombinerepo;
        private readonly IPublicMethodRepsoitory<ProductModel> _productrepo;
        private readonly IProductManualRepository _productmanualrepo;
        private readonly IPublicMethodRepsoitory<CopponModel> _couponrepo;
        private readonly IUserRepository _userrepository;
        private readonly IWalletHistoryRepository _walletrepository;
        private readonly ILogger<BasketService> _logger;
        public readonly IPublicMethodRepsoitory<InvoiceModel> _invoicerepository;
        public readonly IPublicMethodRepsoitory<OrderModel> _orderrepository;
        public readonly IPublicMethodRepsoitory<OptionsModel> _optionrepository;
        private readonly ILocalizationService _lang;
        private readonly ISmsService _smsService;
        private readonly IPCalcRepository _priceCalculator;
        private readonly IViewRenderer renderer;
        public List<ProductCombineModel> AllProducts;
        private bool STOREISACTIVE = true;
        private bool DELETEDISCOUNTS = false;
        private bool ISACTIVESPECIALSALE = false;
        public BasketService(ILogger<BasketService> logger, IPublicMethodRepsoitory<ProductCombineModel> productcombinerepo, IPublicMethodRepsoitory<CopponModel> couponrepo, IUserRepository userRepository, IWalletHistoryRepository walletHistoryRepository, ILocalizationService lang, IViewRenderer viewRenderer, IPublicMethodRepsoitory<InvoiceModel> invoicerepo, IPublicMethodRepsoitory<OrderModel> orderrepository, ISmsService smsService, IPCalcRepository priceCalculator, IPublicMethodRepsoitory<OptionsModel> optionrepository, IPublicMethodRepsoitory<ProductModel> productrepo, IProductManualRepository productmanualrepo)
        {
            this._logger = logger;
            this._productcombinerepo = productcombinerepo;
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
            _productrepo = productrepo;
            _productmanualrepo = productmanualrepo;
        }
        public async Task Initilize()
        {
            var storeisactive = ((List<OptionsModel>)(await _optionrepository.GetAll()).Data).FirstOrDefault(x => x.O_Key == "SHOPSTATE");
            var deletediscounts = ((List<OptionsModel>)(await _optionrepository.GetAll()).Data).FirstOrDefault(x => x.O_Key == "DELETEDISCOUNTS");
            var isactivespecialsale = ((List<OptionsModel>)(await _optionrepository.GetAll()).Data).FirstOrDefault(x => x.O_Key == "SPECIALSALESTATE");
            if (storeisactive != null)
                STOREISACTIVE = (storeisactive.O_Value == "true") ? true : false;
            if (deletediscounts != null)
                DELETEDISCOUNTS = (deletediscounts.O_Value == "true") ? true : false;
            if (isactivespecialsale != null)
                ISACTIVESPECIALSALE = (isactivespecialsale.O_Value == "true") ? true : false;

            AllProducts = ((List<ProductCombineModel>)(await _productcombinerepo.GetAll()).Data);

        }
        public async Task<ResponseStructure> AddToCart(HttpRequest httpRequest, HttpResponse httpResponse)
        {
            try
            {
                if (!this.STOREISACTIVE)
                    return ResponseModel.Error("با عرض پوزش ، در حال حاضر امکان سفارش گیری وجود ندارد");

                StringValues data;

                if (!httpRequest.Headers.TryGetValue("AddToCart", out data))
                    return ResponseModel.Error("دسترسی غیر مجاز");

                var dec = Barayand.Common.Services.CryptoJsService.DecryptStringAES(data);

                FullPropertyBasketItem rm = JsonConvert.DeserializeObject<FullPropertyBasketItem>(dec);

                ProductCombineModel findProduct = new ProductCombineModel();
                if (rm.ProductType == 1)
                {
                    findProduct = AllProducts.FirstOrDefault(x => x.X_Id == rm.ProductCombineId);
                    if (findProduct == null)
                        return ResponseModel.Error("تنوع محصول مورد نظر یافت نشد");

                    if (!findProduct.X_Status || findProduct.X_IsDeleted || findProduct.X_AvailableCount < 1)
                        return ResponseModel.Error("متاسفانه محصول مورد نظر در انبار موجود نمیباشد");

                    if (rm.ProductType == 1 && (rm.Quantity) > findProduct.X_AvailableCount)
                        return ResponseModel.Error("محصول مورد نظر در تعداد درخواستی موجود نمیباشد");
                }


                FullPropertyBasketModel BasketModel = new FullPropertyBasketModel();
                string cookie;
                if (httpRequest.Cookies.TryGetValue("Cart", out cookie))
                {
                    if (cookie != null)
                    {
                        var basketInfo = Barayand.Common.Services.CryptoJsService.DecryptStringAES(cookie);
                        BasketModel = JsonConvert.DeserializeObject<FullPropertyBasketModel>(basketInfo);
                        if (BasketModel.CartItems.Count() > 0)
                        {
                            if (BasketModel.CartItems.Count(x => x.ProductCombineId == rm.ProductCombineId && x.ProductType == rm.ProductType) > 0)
                            {
                                var existsCombine = BasketModel.CartItems.FirstOrDefault(x => x.ProductCombineId == rm.ProductCombineId && x.ProductType == rm.ProductType);

                                if ((existsCombine.Quantity + rm.Quantity) > findProduct.X_AvailableCount)
                                    return ResponseModel.Error("محصول مورد نظر در تعداد درخواستی موجود نمیباشد");

                                existsCombine.Quantity = existsCombine.Quantity + rm.Quantity;

                            }
                            else
                            {
                                BasketModel.CartItems.Add(rm);
                            }
                        }
                    }
                    else
                    {
                        BasketModel.CartItems.Add(rm);
                    }
                }
                else
                {
                    BasketModel.CartItems.Add(rm);
                    BasketModel.OrderDate = DateTime.Now;
                }
                string token = Barayand.Common.Services.CryptoJsService.EncryptStringToAES(JsonConvert.SerializeObject(BasketModel));
                httpResponse.Cookies.Delete("Cart");
                httpResponse.Cookies.Append("Cart", token);
                return ResponseModel.Success("محصول مورد نظر با موفقیت به سبد خرید اضافه گردید");
            }
            catch (Exception ex)
            {
                _logger.LogError("Error in adding product to basket", ex);
                return ResponseModel.ServerInternalError(data: ex);
            }
        }

        public async Task<ResponseStructure> FreeUpCart(HttpRequest httpRequest, HttpResponse httpResponse)
        {
            try
            {
                StringValues data;
                FullPropertyBasketModel basket = new FullPropertyBasketModel();
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
                _logger.LogError("Error in free up basket cart", ex);
                return ResponseModel.ServerInternalError(data: ex);
            }
        }

        public async Task<BasketViewModel> GetBasketItems(HttpRequest httpRequest)
        {
            try
            {
                BasketViewModel basketView = new BasketViewModel();
                string cookie;
                if (httpRequest.Cookies.TryGetValue("Cart", out cookie))
                {
                    if (cookie != null)
                    {
                        FullPropertyBasketModel BasketModel = new FullPropertyBasketModel();
                        var basketInfo = Barayand.Common.Services.CryptoJsService.DecryptStringAES(cookie);
                        BasketModel = JsonConvert.DeserializeObject<FullPropertyBasketModel>(basketInfo);
                        decimal SumWithDiscount = 0;
                        decimal SumWithOutDiscount = 0;
                        if (BasketModel.CartItems.Count() > 0)
                        {
                            List<ProductList> productLists = new List<ProductList>();
                            foreach (var item in BasketModel.CartItems)
                            {
                                ProductList productList = new ProductList();
                                if (item.ProductType == 1)//item is product combine
                                {
                                    var productcomine = await _productcombinerepo.GetById(item.ProductCombineId);
                                    if (productcomine != null)
                                    {
                                        ProductModel product = await _productrepo.GetById(productcomine.X_ProductId);
                                        if (product != null)
                                        {
                                            var priceModel = await _priceCalculator.CalculateProductCombinePrice(productcomine.X_Id, product.P_EndLevelCatId);
                                            productList.ProductTitle = product.P_Title;
                                            productList.ProductImage = product.P_Image;
                                            productList.ProductCombineId = productcomine.X_Id;
                                            productList.Quantity = item.Quantity;
                                            productList.Price = priceModel.Price;
                                            productList.DiscountedPrice = priceModel.DiscountedPrice;
                                            if (priceModel.HasDiscount)
                                            {
                                                productList.Total = (productList.DiscountedPrice * item.Quantity);
                                                SumWithDiscount += productList.Total;
                                            }
                                            else
                                            {
                                                productList.Total = (productList.Price * item.Quantity);
                                                if (!await _priceCalculator.checkProductCombineExistsDiscount(item.ProductCombineId, product.P_EndLevelCatId))
                                                {
                                                    SumWithOutDiscount += productList.Total;
                                                }
                                            }
                                            productList.IsAvailable = (productcomine.X_AvailableCount >= item.Quantity);
                                            productList.ColorTitle = productcomine.ColorDetail.C_Title;
                                            productList.WarrantyTitle = productcomine.WarrantyModel.W_Title;
                                            productList.GiftProduct = product.Gift;
                                            productLists.Add(productList);

                                        }
                                    }
                                }
                                else//item is product manual
                                {
                                    var manual = await _productmanualrepo.GetById(item.ProductCombineId);
                                    if (manual != null)
                                    {
                                        var product = await _productrepo.GetById(manual.M_ProductId);
                                        if (product != null)
                                        {
                                            productList.ProductTitle = manual.M_Title + "-" + product.P_Title;
                                            productList.ProductImage = product.P_Image;
                                            productList.ProductCombineId = 0;
                                            productList.Quantity = item.Quantity;
                                            productList.Price = manual.M_Price;
                                            productList.DiscountedPrice = 0;
                                            productList.Total = (productList.Price * item.Quantity);
                                            productList.ColorTitle = "---";
                                            productList.WarrantyTitle = "---";
                                            productList.GiftProduct = null;
                                            productLists.Add(productList);
                                        }
                                    }
                                }
                            }
                            basketView.Products.AddRange(productLists);
                            basketView.ReciptientInfo = BasketModel.RecipientInfo;
                            if (BasketModel.Coppon.Count() > 0)
                            {
                                var c = BasketModel.Coppon.FirstOrDefault();
                                if (SumWithOutDiscount > 0)
                                {
                                    var clc = (SumWithOutDiscount - c.CP_Discount);
                                    clc = clc < 0 ? 0 : clc;
                                    BasketModel.Total = clc + SumWithDiscount;
                                }
                                else
                                {
                                    BasketModel.Total = SumWithDiscount;
                                }
                                var coupunAmount = c.CP_Discount;
                                basketView.CouponInfo = new Coupon() { CouponAmount = coupunAmount, CouponDiscount = c.CP_Discount, CouponId = c.CP_Code };
                            }
                            else
                            {
                                BasketModel.Total = SumWithDiscount + SumWithOutDiscount;
                            }
                            basketView.Total = BasketModel.Total;
                        }
                    }
                }

                return basketView;
            }
            catch (Exception ex)
            {
                return new BasketViewModel();
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
                FullPropertyBasketItem rm = JsonConvert.DeserializeObject<FullPropertyBasketItem>(dec);

                FullPropertyBasketModel basket = new FullPropertyBasketModel();
                string cookie;
                if (httpRequest.Cookies.TryGetValue("Cart", out cookie))
                {
                    if (cookie != null)
                    {
                        var basketInfo = Barayand.Common.Services.CryptoJsService.DecryptStringAES(cookie);
                        basket = JsonConvert.DeserializeObject<FullPropertyBasketModel>(basketInfo);
                        var item = basket.CartItems.FirstOrDefault(x => x.ProductCombineId == rm.ProductCombineId && x.ProductType == rm.ProductType);
                        if (item != null)
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
                _logger.LogError("Error in removing cart item", ex);
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
                FullPropertyBasketModel basket = new FullPropertyBasketModel();
                string cookie;
                if (httpRequest.Cookies.TryGetValue("Cart", out cookie))
                {
                    if (cookie != null)
                    {
                        var basketInfo = Barayand.Common.Services.CryptoJsService.DecryptStringAES(cookie);
                        basket = JsonConvert.DeserializeObject<FullPropertyBasketModel>(basketInfo);
                        basket.RecipientInfo = rm;
                    }

                }

                string token = Barayand.Common.Services.CryptoJsService.EncryptStringToAES(JsonConvert.SerializeObject(basket));
                httpResponse.Cookies.Delete("Cart");
                httpResponse.Cookies.Append("Cart", token);
                return ResponseModel.Success("اطلاعات دریافت کننده سفارش با موفقیت ذخیره گردید.");
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
                FullPropertyBasketItem rm = JsonConvert.DeserializeObject<FullPropertyBasketItem>(dec);
                var coppon = ((List<CopponModel>)(await _couponrepo.GetAll()).Data).FirstOrDefault(x => x.CP_Code == rm.CopponCode);
                if (coppon == null)
                {
                    return ResponseModel.Error("کد تخفیف نامعتبر است.");
                }
                if (DateTime.Now >= coppon.CP_EndDate || DateTime.Now < coppon.CP_StartDate)
                {
                    return ResponseModel.Error("کد تخفیف منقضی شده است.");
                }
                FullPropertyBasketModel basket = new FullPropertyBasketModel();
                string cookie;
                if (httpRequest.Cookies.TryGetValue("Cart", out cookie))
                {
                    if (cookie != null)
                    {
                        var basketInfo = Barayand.Common.Services.CryptoJsService.DecryptStringAES(cookie);
                        basket = JsonConvert.DeserializeObject<FullPropertyBasketModel>(basketInfo);
                        if (basket.Coppon.Count > 0)
                        {
                            return ResponseModel.Error("بیشتر از یک کد تخفیف نمیتوانید استفاده نمایید");
                        }
                        if (basket.Coppon.Count(x => x.CP_Code == coppon.CP_Code) > 0)
                        {
                            return ResponseModel.Error("کد تخفیف قبلا اعمال شده است");
                        }
                        int DiscountAbleProductCount = 0;
                        foreach (var item in basket.CartItems)
                        {
                            if (item.ProductType == 1)
                            {
                                var productcomine = await _productcombinerepo.GetById(item.ProductCombineId);
                                var product = await _productrepo.GetById(productcomine.X_ProductId);
                                var priceModel = await _priceCalculator.CalculateProductCombinePrice(productcomine.X_Id, product.P_EndLevelCatId);
                                if (!priceModel.HasDiscount)
                                {
                                    if (!await _priceCalculator.checkProductCombineExistsDiscount(item.ProductCombineId, product.P_EndLevelCatId))
                                    {
                                        DiscountAbleProductCount++;
                                    }
                                }
                            }
                            else
                            {
                                DiscountAbleProductCount++;
                            }
                        }
                        if (DiscountAbleProductCount > 0)
                        {
                            basket.Coppon.Add(coppon);
                        }
                        else
                        {
                            return ResponseModel.Error("کد تخفیف مورد نظر مختص به محصولات بدون تخفیف میباشد.");
                        }
                    }

                }

                string token = Barayand.Common.Services.CryptoJsService.EncryptStringToAES(JsonConvert.SerializeObject(basket));
                httpResponse.Cookies.Delete("Cart");
                httpResponse.Cookies.Append("Cart", token);
                return ResponseModel.Success("کد تخفیف با موفقیت اعمال گردید");
            }
            catch (Exception ex)
            {
                _logger.LogError("Error in adding coupon to customer basket", ex);
                return ResponseModel.ServerInternalError(data: ex);
            }
        }
        public async Task<ResponseStructure> TestCheckout(HttpRequest httpRequest, HttpResponse httpResponse, int type = 1, bool RequestedPOS = false)
        {
            try
            {
                var authorize = Barayand.Common.Services.TokenService.AuthorizeUser(httpRequest);
                if (authorize < 1)
                {
                    return ResponseModel.Error("کاربر وارد نشده است");
                }
                UserModel userModel = await _userrepository.GetById(authorize);
                if (userModel == null)
                {
                    return ResponseModel.Error("کاربر یافت نشد");
                }
                FullPropertyBasketModel BasketModel = new FullPropertyBasketModel();
                string cookie;
                string InvoiceID = null;
                if (httpRequest.Cookies.TryGetValue("Cart", out cookie))
                {
                    if (cookie != null)
                    {
                        var basketInfo = Barayand.Common.Services.CryptoJsService.DecryptStringAES(cookie);
                        BasketModel = JsonConvert.DeserializeObject<FullPropertyBasketModel>(basketInfo);
                        if (BasketModel.CartItems.Count() > 0)
                        {
                            BasketViewModel basketView = await GetBasketItems(httpRequest);
                            InvoiceModel Invoice = new InvoiceModel();
                            Invoice.I_BoxWrapperId = BasketModel.GiftBoxWrapperId;
                            Invoice.I_CopponDiscount = (BasketModel.Coppon.Count() > 0) ? BasketModel.Coppon.Sum(x => x.CP_Discount) : 0;
                            Invoice.I_CopponId = (BasketModel.Coppon.Count() > 0) ? BasketModel.Coppon.FirstOrDefault().CP_Id : 0;
                            Invoice.I_DeliveryDate = BasketModel.RecipientInfo.DeliveryDate;
                            Invoice.I_PaymentDate = DateTime.Now;
                            Invoice.I_PaymentInfo = "Test payment";
                            Invoice.I_PaymentType = type;
                            Invoice.I_RecipientInfo = JsonConvert.SerializeObject(BasketModel.RecipientInfo);
                            Invoice.I_RequestedPOS = RequestedPOS;
                            Invoice.I_Status = 2;
                            Invoice.I_TotalProductAmount = basketView.Products.Sum(x => x.Total);
                            Invoice.I_TotalAmount = basketView.Total;
                            Invoice.I_UserId = userModel.U_Id;
                            Invoice.I_Id = UtilesService.RandomDigit(12);
                            var insertInvoiceResult = await _invoicerepository.Insert(Invoice);
                            if (insertInvoiceResult.Status == false)
                            {
                                return insertInvoiceResult;
                            }
                            int AvailAbleCounts = 0;
                            foreach (var item in BasketModel.CartItems)
                            {
                                OrderModel order = new OrderModel();
                                if (item.ProductType == 1)
                                {
                                    var combine = await _productcombinerepo.GetById(item.ProductCombineId);
                                    if (combine != null && combine.X_AvailableCount >= item.Quantity )
                                    {
                                        AvailAbleCounts++;
                                        order.O_ColorId = combine.X_ColorId;
                                        order.O_ProductId = combine.X_ProductId;
                                        order.O_WarrantyId = combine.X_WarrantyId;
                                        order.O_ReciptId = Invoice.I_Id;
                                        order.O_Quantity = item.Quantity;
                                        order.O_ProductType = 1;
                                        order.O_Price = combine.PriceModel.Price;
                                        order.O_Discount = combine.PriceModel.HasDiscount ? combine.PriceModel.Discount : 0;
                                        order.O_DiscountType = 1;
                                        order.O_GiftId = item.GiftProductCombineId;
                                        await _orderrepository.Insert(order);
                                    }
                                }
                                else
                                {
                                    var pmanual = await _productmanualrepo.GetById(item.ProductCombineId);
                                    if (pmanual != null)
                                    {
                                        AvailAbleCounts++;
                                        order.O_ProductId = pmanual.M_ProductId;
                                        order.O_ReciptId = Invoice.I_Id;
                                        order.O_Quantity = item.Quantity;
                                        order.O_ProductType = 2;
                                        order.O_Price = pmanual.M_Price;
                                        await _orderrepository.Insert(order);
                                    }
                                }
                            }
                            InvoiceID = Invoice.I_Id;
                            if(AvailAbleCounts < 1)
                            {
                                await _invoicerepository.Delete(InvoiceID);
                                await FreeUpCart(httpRequest, httpResponse);
                                return ResponseModel.Error("تمامی کالاهای موجود در سبد خرید شما ناموجود میباشند");
                            }
                        }
                    }
                }

                await FreeUpCart(httpRequest, httpResponse);
                return ResponseModel.Success("سفارش شما با موفقیت ثبت گردید", data: InvoiceID);
            }
            catch (Exception ex)
            {
                return ResponseModel.ServerInternalError(data: ex);
            }
        }
        public async Task<ResponseStructure> IncreaseProductCount(HttpRequest httpRequest, HttpResponse httpResponse)
        {
            try
            {
                if (!this.STOREISACTIVE)
                    return ResponseModel.Error("با عرض پوزش ، در حال حاضر امکان سفارش گیری وجود ندارد");

                StringValues data;

                if (!httpRequest.Headers.TryGetValue("AddToCart", out data))
                    return ResponseModel.Error("دسترسی غیر مجاز");

                var dec = Barayand.Common.Services.CryptoJsService.DecryptStringAES(data);

                FullPropertyBasketItem rm = JsonConvert.DeserializeObject<FullPropertyBasketItem>(dec);

                ProductCombineModel findProduct = new ProductCombineModel();
                if (rm.ProductType == 1)
                {
                    findProduct = AllProducts.FirstOrDefault(x => x.X_Id == rm.ProductCombineId);
                    if (findProduct == null)
                        return ResponseModel.Error("تنوع محصول مورد نظر یافت نشد");

                    if (!findProduct.X_Status || findProduct.X_IsDeleted || findProduct.X_AvailableCount < 1)
                        return ResponseModel.Error("متاسفانه محصول مورد نظر در انبار موجود نمیباشد");

                    if (rm.ProductType == 1 && (rm.Quantity) > findProduct.X_AvailableCount)
                        return ResponseModel.Error("محصول مورد نظر در تعداد درخواستی موجود نمیباشد");
                }


                FullPropertyBasketModel BasketModel = new FullPropertyBasketModel();
                string cookie;
                if (httpRequest.Cookies.TryGetValue("Cart", out cookie))
                {
                    if (cookie != null)
                    {
                        var basketInfo = Barayand.Common.Services.CryptoJsService.DecryptStringAES(cookie);
                        BasketModel = JsonConvert.DeserializeObject<FullPropertyBasketModel>(basketInfo);
                        if (BasketModel.CartItems.Count() > 0)
                        {
                            if (BasketModel.CartItems.Count(x => x.ProductCombineId == rm.ProductCombineId && x.ProductType == rm.ProductType) > 0)
                            {
                                var existsCombine = BasketModel.CartItems.FirstOrDefault(x => x.ProductCombineId == rm.ProductCombineId && x.ProductType == rm.ProductType);

                                if ((existsCombine.Quantity + rm.Quantity) > findProduct.X_AvailableCount)
                                    return ResponseModel.Error("محصول مورد نظر در تعداد درخواستی موجود نمیباشد");

                                existsCombine.Quantity = existsCombine.Quantity + rm.Quantity;
                            }
                        }
                    }
                }

                string token = Barayand.Common.Services.CryptoJsService.EncryptStringToAES(JsonConvert.SerializeObject(BasketModel));
                httpResponse.Cookies.Delete("Cart");
                httpResponse.Cookies.Append("Cart", token);
                return ResponseModel.Success("محصول مورد نظر با موفقیت به سبد خرید اضافه گردید");
            }
            catch (Exception ex)
            {
                _logger.LogError("Error in adding product to basket", ex);
                return ResponseModel.ServerInternalError(data: ex);
            }
        }

        public async Task<ResponseStructure> DecreaseProductCount(HttpRequest httpRequest, HttpResponse httpResponse)
        {
            try
            {
                if (!this.STOREISACTIVE)
                    return ResponseModel.Error("با عرض پوزش ، در حال حاضر امکان سفارش گیری وجود ندارد");

                StringValues data;

                if (!httpRequest.Headers.TryGetValue("AddToCart", out data))
                    return ResponseModel.Error("دسترسی غیر مجاز");

                var dec = Barayand.Common.Services.CryptoJsService.DecryptStringAES(data);

                FullPropertyBasketItem rm = JsonConvert.DeserializeObject<FullPropertyBasketItem>(dec);

                ProductCombineModel findProduct = new ProductCombineModel();

                FullPropertyBasketModel BasketModel = new FullPropertyBasketModel();
                string cookie;
                if (httpRequest.Cookies.TryGetValue("Cart", out cookie))
                {
                    if (cookie != null)
                    {
                        var basketInfo = Barayand.Common.Services.CryptoJsService.DecryptStringAES(cookie);
                        BasketModel = JsonConvert.DeserializeObject<FullPropertyBasketModel>(basketInfo);
                        if (BasketModel.CartItems.Count() > 0)
                        {
                            if (BasketModel.CartItems.Count(x => x.ProductCombineId == rm.ProductCombineId && x.ProductType == rm.ProductType) > 0)
                            {
                                var existsCombine = BasketModel.CartItems.FirstOrDefault(x => x.ProductCombineId == rm.ProductCombineId && x.ProductType == rm.ProductType);


                                existsCombine.Quantity = existsCombine.Quantity - rm.Quantity;
                                if (existsCombine.Quantity < 1)
                                    BasketModel.CartItems.Remove(existsCombine);
                            }
                        }
                    }
                }

                string token = Barayand.Common.Services.CryptoJsService.EncryptStringToAES(JsonConvert.SerializeObject(BasketModel));
                httpResponse.Cookies.Delete("Cart");
                httpResponse.Cookies.Append("Cart", token);
                return ResponseModel.Success("محصول مورد نظر با موفقیت به سبد خرید اضافه گردید");
            }
            catch (Exception ex)
            {
                _logger.LogError("Error in adding product to basket", ex);
                return ResponseModel.ServerInternalError(data: ex);
            }
        }
    }
}
