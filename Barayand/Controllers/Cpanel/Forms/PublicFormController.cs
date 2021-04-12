using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Barayand.DAL.Interfaces;
using Barayand.DAL.Repositories;
using Barayand.Models.Entity;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Barayand.OutModels.Response;
using Barayand.OutModels.Models;
using Microsoft.Extensions.Primitives;
using Newtonsoft.Json;

namespace Barayand.Controllers.Cpanel.Forms
{
    [Route("api/cpanel/[controller]")]
    [ApiController]
    public class PublicFormController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly IPublicMethodRepsoitory<PublicFormsModel> _repository;
        private readonly IPublicMethodRepsoitory<NewsletterModel> _newsletterrepository;
        private readonly IPublicMethodRepsoitory<OfflinRequestModel> _offreqrepo;
        public PublicFormController(IMapper mapper, IPublicMethodRepsoitory<PublicFormsModel> repository, IPublicMethodRepsoitory<NewsletterModel> newsletterrepository, IPublicMethodRepsoitory<OfflinRequestModel> offreqrepo)
        {
            this._repository = repository;
            this._newsletterrepository = newsletterrepository;
            this._mapper = mapper;
            this._offreqrepo = offreqrepo;
        }
        [Route("AddPublicFroms")]
        public async Task<ActionResult> AddPublicFroms()
        {
            try
            {
                StringValues data;
                if (!Request.Headers.TryGetValue("PublicFormData", out data))
                {
                    return new JsonResult(ResponseModel.Error("Invalid access detect."));
                }
                var dec = Barayand.Common.Services.CryptoJsService.DecryptStringAES(data);
                PublicForms rm = JsonConvert.DeserializeObject<PublicForms>(dec);
                PublicFormsModel cm = (PublicFormsModel)_mapper.Map<OutModels.Models.PublicForms, PublicFormsModel>(rm);
                return new JsonResult(await this._repository.Insert(cm));
            }
            catch (Exception ex)
            {
                return null;
            }
        }
        [Route("GetPublicForms/{type?}")]
        public async Task<ActionResult> GetFormData(int type = 1)
        {
            try
            {
                if(type != -1)
                {
                    List<PublicFormsModel> data = ((List<PublicFormsModel>)(await _repository.GetAll()).Data).Where(x => x.F_Type == type).OrderByDescending(x => x.Created_At).ToList();
                    return new JsonResult(ResponseModel.Success(data: data));
                }
                else
                {
                    List<NewsletterModel> data = ((List<NewsletterModel>)(await _newsletterrepository.GetAll()).Data).OrderByDescending(x => x.Created_At).ToList();
                    return new JsonResult(ResponseModel.Success(data: data));
                }
            }
            catch (Exception ex)
            {
                return null;
            }
        }


        ///////NewsLetter
        [Route("AddNewsLetter")]
        public async Task<ActionResult> AddNewsLetter()
        {
            try
            {
                StringValues data;
                if (!Request.Headers.TryGetValue("NewsLetter", out data))
                {
                    return new JsonResult(ResponseModel.Error("Invalid access detect."));
                }
                var dec = Barayand.Common.Services.CryptoJsService.DecryptStringAES(data);
                NewsLetter rm = JsonConvert.DeserializeObject<NewsLetter>(dec);
                NewsletterModel cm = (NewsletterModel)_mapper.Map<OutModels.Models.NewsLetter, NewsletterModel>(rm);
                return new JsonResult(await this._newsletterrepository.Insert(cm));
            }
            catch (Exception ex)
            {
                return null;
            }
        }
        ///////OfflineRequest
        [Route("AddOfflineRequest")]
        public async Task<ActionResult> AddOfflineRequest()
        {
            try
            {
                StringValues data;
                if (!Request.Headers.TryGetValue("OfflineRequest", out data))
                {
                    return new JsonResult(ResponseModel.Error("Invalid access detect."));
                }
                var authorize = Barayand.Common.Services.TokenService.AuthorizeUser(Request);
                if (authorize < 1)
                {
                    return new JsonResult(ResponseModel.Error("User not logged in."));
                }

                var dec = Barayand.Common.Services.CryptoJsService.DecryptStringAES(data);
                OfflinRequestModel cm = JsonConvert.DeserializeObject<OfflinRequestModel>(dec);
                cm.O_User = authorize;
                return new JsonResult(await this._offreqrepo.Insert(cm));
            }
            catch (Exception ex)
            {
                return null;
            }
        }
    }
}