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
using Barayand.OutModels.Miscellaneous;

namespace Barayand.Controllers.BaseSetting
{
    [Route("api/cpanel/basesetting/[controller]")]
    [ApiController]
    public class AmzingRequestesController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly IPublicMethodRepsoitory<AmazingRequestModel> _repository;
        private readonly IPublicMethodRepsoitory<ProductModel> _productrepository;
        private readonly IPublicMethodRepsoitory<BetterPriceFoundModel> _betterpricerepository;
        private readonly IPublicMethodRepsoitory<ProdFeedbackModel> _prodfeedbackrepo;
        private readonly IUserRepository _userrepository;
        public AmzingRequestesController(IMapper mapper, IPublicMethodRepsoitory<AmazingRequestModel> repository, IPublicMethodRepsoitory<BetterPriceFoundModel> betterpricerepo, IPublicMethodRepsoitory<ProductModel> productrepo, IUserRepository userRepository, IPublicMethodRepsoitory<ProdFeedbackModel> prodfeedbackrepo)
        {
            this._repository = repository;
            this._betterpricerepository = betterpricerepo;
            this._productrepository = productrepo;
            this._userrepository = userRepository;
            this._prodfeedbackrepo = prodfeedbackrepo;
            this._mapper = mapper;
        }
        [Route("AddAmzingRequest")]
        [HttpPost]
        public async Task<ActionResult> AddAmzingRequest(OutModels.Models.AmazingRequest attribute)
        {
            try
            {
                AmazingRequestModel am = (AmazingRequestModel)_mapper.Map<OutModels.Models.AmazingRequest, AmazingRequestModel>(attribute);
                return new JsonResult(await this._repository.Insert(am));
            }
            catch (Exception ex)
            {
                return null;
            }
        }
        [Route("LoadAmazingRequests/{type}")]
        [HttpPost]
        public async Task<ActionResult> LoadAmazingRequests(int type)
        {
            try
            {
                var All = await this._repository.GetAll();
                List<AmazingRequestModel> data = ((List<AmazingRequestModel>)(All).Data).Where(x => x.A_Type == type).OrderByDescending(x => x.Created_At).ToList();
                List<OutModels.Models.AmazingRequest> result = _mapper.Map<List<AmazingRequestModel>, List<OutModels.Models.AmazingRequest>>(data);
                foreach(var item in result)
                {
                    var prd = await _productrepository.GetById(item.A_ProductId);
                    if(prd != null)
                    {
                        item.A_ProductTitle = prd.P_Title+"--"+prd.P_Code;
                    }
                    else
                    {
                        item.A_ProductTitle = "----";
                    }
                    var user = await _userrepository.GetById(item.A_UserId);
                    if(user!=null)
                    {
                        item.A_UserName = user.U_Name + " " + user.U_Family;
                    }
                    else
                    {
                        item.A_UserName = "----";
                    }
                }
                return new JsonResult(ResponseModel.Success("AMAZINGREQUEST_LIST_RETURNED", result));
            }
            catch (Exception ex)
            {
                return null;
            }
        }
        //////////////////////
        [Route("AddBetterPriceRequest")]
        [HttpPost]
        public async Task<ActionResult> AddBetterPriceRequest(OutModels.Models.BetterPriceFound attribute)
        {
            try
            {
                BetterPriceFoundModel am = (BetterPriceFoundModel)_mapper.Map<OutModels.Models.BetterPriceFound
                    , BetterPriceFoundModel>(attribute);
                return new JsonResult(await this._betterpricerepository.Insert(am));
            }
            catch (Exception ex)
            {
                return null;
            }
        }
        [Route("LoadBetterPrice")]
        [HttpPost]
        public async Task<ActionResult> LoadBetterPrice()
        {
            try
            {
                var All = await this._betterpricerepository.GetAll();
                List<BetterPriceFoundModel> data = ((List<BetterPriceFoundModel>)(All).Data).OrderByDescending(x => x.Created_At).ToList();
                List<OutModels.Models.BetterPriceFound> result = _mapper.Map<List<BetterPriceFoundModel>, List<OutModels.Models.BetterPriceFound>>(data);
                foreach (var item in result)
                {
                    var prd = await _productrepository.GetById(item.B_ProductId);
                    if (prd != null)
                    {
                        item.B_ProductTitle = prd.P_Title + "--" + prd.P_Code;
                    }
                    else
                    {
                        item.B_ProductTitle = "----";
                    }
                }
                return new JsonResult(ResponseModel.Success("AMAZINGREQUEST_LIST_RETURNED", result));
            }
            catch (Exception ex)
            {
                return null;
            }
        }
        //////////////////////
        [Route("AddProductFeedback")]
        [HttpPost]
        public async Task<ActionResult> AddProductFeedback(OutModels.Models.ProdFeedback attribute)
        {
            try
            {
                foreach(var t in attribute.F_Topics)
                {
                    ProdFeedbackModel am = new ProdFeedbackModel();
                    if(t == 6)
                    {
                        am.F_DuplicateUrl = attribute.F_DuplicateUrl;
                    }
                    am.F_Description = attribute.F_Description;
                    am.F_ProductId = attribute.F_ProductId;
                    am.F_Topic = t;
                    await this._prodfeedbackrepo.Insert(am);
                }
                return new JsonResult(ResponseModel.Success("با تشکر ، بازخورد شما با موفقیت ثبت گردید"));
            }
            catch (Exception ex)
            {
                return null;
            }
        }
        [Route("LoadProductFeedback")]
        [HttpPost]
        public async Task<ActionResult> LoadProductFeedback()
        {
            try
            {
                var All = await this._betterpricerepository.GetAll();
                List<ProdFeedbackModel> data = ((List<ProdFeedbackModel>)(All).Data).OrderByDescending(x => x.Created_At).ToList();
                List<OutModels.Models.ProdFeedback> result = _mapper.Map<List<ProdFeedbackModel>, List<OutModels.Models.ProdFeedback>>(data);
                foreach (var item in result)
                {
                    var prd = await _productrepository.GetById(item.F_ProductId);
                    if (prd != null)
                    {
                        item.F_ProductTitle = prd.P_Title + "--" + prd.P_Code;
                    }
                    else
                    {
                        item.F_ProductTitle = "----";
                    }
                }
                return new JsonResult(ResponseModel.Success("AMAZINGREQUEST_LIST_RETURNED", result));
            }
            catch (Exception ex)
            {
                return null;
            }
        }

    }
}
