using AutoMapper;
using Barayand.Models.Entity;
using Barayand.OutModels.Miscellaneous;
using Barayand.OutModels.Response;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Barayand.Services.Interfaces
{
    public interface IBasketService
    {
        Task<ResponseStructure> AddToCart(Microsoft.AspNetCore.Http.HttpRequest httpRequest, Microsoft.AspNetCore.Http.HttpResponse httpResponse);
        Task<ResponseStructure> IncreaseProductCount(Microsoft.AspNetCore.Http.HttpRequest httpRequest, Microsoft.AspNetCore.Http.HttpResponse httpResponse);
        Task<ResponseStructure> DecreaseProductCount(Microsoft.AspNetCore.Http.HttpRequest httpRequest, Microsoft.AspNetCore.Http.HttpResponse httpResponse);
        Task<ResponseStructure> RemoveCartItem(Microsoft.AspNetCore.Http.HttpRequest httpRequest, Microsoft.AspNetCore.Http.HttpResponse httpResponse);
        Task<ResponseStructure> FreeUpCart(Microsoft.AspNetCore.Http.HttpRequest httpRequest, Microsoft.AspNetCore.Http.HttpResponse httpResponse);
        Task<ResponseStructure> UseCoppon(Microsoft.AspNetCore.Http.HttpRequest httpRequest, Microsoft.AspNetCore.Http.HttpResponse httpResponse);
        Task<ResponseStructure> SaveReciptientInfo(Microsoft.AspNetCore.Http.HttpRequest httpRequest, Microsoft.AspNetCore.Http.HttpResponse httpResponse);
        Task<BasketViewModel> GetBasketItems(Microsoft.AspNetCore.Http.HttpRequest httpRequest);
        Task<ResponseStructure> TestCheckout(Microsoft.AspNetCore.Http.HttpRequest httpRequest, Microsoft.AspNetCore.Http.HttpResponse httpResponse, int type = 1, bool RequestedPOS = false);
    }
}