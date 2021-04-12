using Barayand.Models.Entity;
using Barayand.OutModels.Miscellaneous;
using Barayand.OutModels.Response;
using Microsoft.Extensions.Primitives;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Barayand.Common.Services
{
    public  class BasketService
    {
        public BasketService()
        {

        }
        public  async Task<ResponseStructure> AddToCart(Microsoft.AspNetCore.Http.HttpRequest httpRequest, Microsoft.AspNetCore.Http.HttpResponse httpResponse,List<ProductModel> AllProducts)
        {
  
            //StringValues data;
            //if (!httpRequest.Headers.TryGetValue("AddToCart", out data))
            //{
            //    return ResponseModel.Error("Invalid access detect.");
            //}
            //var dec = Barayand.Common.Services.CryptoJsService.DecryptStringAES(data);
            //BasketItem rm = JsonConvert.DeserializeObject<BasketItem>(dec);
            //var product = AllProducts.FirstOrDefault(x => x.P_Id == rm.ProductId);
            //if (product == null)
            //{
            //    return ResponseModel.Error("Invalid access detect.");
            //}
            //product.P_Description = null;
            //product.P_Seo = null;
            //product.P_ImgGallery = null;
            //BasketModel basket = new BasketModel();
            //string cookie;
            //if(httpRequest.Cookies.TryGetValue("Cart",out cookie))
            //{
            //    if(cookie != null)
            //    {
            //        var basketInfo = Barayand.Common.Services.CryptoJsService.DecryptStringAES(cookie);
            //        basket = JsonConvert.DeserializeObject<BasketModel>(basketInfo);
            //        if(basket.CartItems.Count > 0)
            //        {
            //            if(basket.CartItems.FirstOrDefault(x=>x.Product.P_Type != product.P_Type) != null)
            //            {
            //                return ResponseModel.Error("You can only add a type of product (Book or Art or Learning trains) in same time.");
            //            }
            //        }
            //        var existsProduct = basket.CartItems.FirstOrDefault(x=>x.Product.P_Id == rm.ProductId);
            //        if(existsProduct != null)
            //        {
            //            existsProduct.Quantity += rm.Quantity;
            //        }
            //        else
            //        {
            //            rm.Product = product;
            //            basket.CartItems.Add(rm);
            //        }
                   
            //    }
            //    else
            //    {
            //        rm.Product = product;
            //        basket.CartItems.Add(rm);
            //    }
            //}
            //else
            //{
            //    //rm.Product = product;
            //    basket.CartItems.Add(rm);
            //    basket.OrderDate = DateTime.Now;
            //}

            //string token = Barayand.Common.Services.CryptoJsService.EncryptStringToAES(JsonConvert.SerializeObject(basket));
            //httpResponse.Cookies.Delete("Cart");
            //httpResponse.Cookies.Append("Cart", token);
            return ResponseModel.Success("Product added.");
        }
        public async Task<ResponseStructure> RemoveCartItem(Microsoft.AspNetCore.Http.HttpRequest httpRequest, Microsoft.AspNetCore.Http.HttpResponse httpResponse)
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
                    basket.CartItems = basket.CartItems.Where(x=>x.Product.P_Id != rm.ProductId && x.PrintAble != rm.PrintAble).ToList();

                }
            }
            httpResponse.Cookies.Delete("Cart");
            
            if(basket.CartItems.Count > 0)
            {
                string token = Barayand.Common.Services.CryptoJsService.EncryptStringToAES(JsonConvert.SerializeObject(basket));

                httpResponse.Cookies.Append("Cart", token);
            }
            return ResponseModel.Success("Product removed.");
        }
        public async Task<ResponseStructure> FreeUpCart(Microsoft.AspNetCore.Http.HttpRequest httpRequest, Microsoft.AspNetCore.Http.HttpResponse httpResponse)
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
        public async Task<ResponseStructure> UseCoppon(Microsoft.AspNetCore.Http.HttpRequest httpRequest, Microsoft.AspNetCore.Http.HttpResponse httpResponse, List<CopponModel> AllCoppons)
        {

            StringValues data;
            if (!httpRequest.Headers.TryGetValue("AddToCart", out data))
            {
                return ResponseModel.Error("Invalid access detect.");
            }
            var dec = Barayand.Common.Services.CryptoJsService.DecryptStringAES(data);
            BasketItem rm = JsonConvert.DeserializeObject<BasketItem>(dec);
            var coppon = AllCoppons.FirstOrDefault(x => x.CP_Code == rm.CopponCode);
            if (coppon == null)
            {
                return ResponseModel.Error("Coppon code is invalid.");
            }
            if(DateTime.Now >= coppon.CP_EndDate || DateTime.Now < coppon.CP_StartDate)
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
                    if (basket.Coppon.Count(x=>x.CP_Code == coppon.CP_Code) > 0)
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
        }
        public async Task<ResponseStructure> SaveReciptientInfo(Microsoft.AspNetCore.Http.HttpRequest httpRequest, Microsoft.AspNetCore.Http.HttpResponse httpResponse)
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
        public async Task<BasketModel> GetBasketItems(Microsoft.AspNetCore.Http.HttpRequest httpRequest)
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
                    }
                }
                return basket;
            }
            catch(Exception ex)
            {
                return basket;
            }
        }
    }
}
