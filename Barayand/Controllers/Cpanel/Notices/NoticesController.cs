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


namespace Barayand.Controllers.Cpanel.Notices
{
    [Route("api/cpanel/[controller]")]
    [ApiController]
    public class NoticesController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly IPublicMethodRepsoitory<NoticesModel> _repository;
        public NoticesController(IMapper mapper, IPublicMethodRepsoitory<NoticesModel> repository)
        {
            this._repository = repository;
            this._mapper = mapper;
        }
        [Route("AddNotices")]
        [HttpPost]
        public async Task<ActionResult> AddNotices(OutModels.Models.Notices n)
        {
            try
            {
                NoticesModel b = (NoticesModel)_mapper.Map<OutModels.Models.Notices, NoticesModel>(n);
                return new JsonResult(await this._repository.Insert(b));
            }
            catch (Exception ex)
            {
                return null;
            }
        }
        [Route("LoadNotices/{type?}/{lang?}")]
        [HttpPost]
        public async Task<ActionResult> LoadNotices(int type = 1,string lang = null)
        {
            try
            {
                List<NoticesModel> data = (List<NoticesModel>)(await this._repository.GetAll()).Data;
                List<OutModels.Models.Notices> result = _mapper.Map<List<NoticesModel>, List<OutModels.Models.Notices>>(data.Where(x => x.N_Type == type && x.Lang == lang).ToList());
                return new JsonResult(ResponseModel.Success("NOTICES_LIST_RETURNED", result));
            }
            catch (Exception ex)
            {
                return null;
            }
        }
        [Route("UpdateNotices")]
        [HttpPost]
        public async Task<ActionResult> UpdateNotices(OutModels.Models.Notices n)
        {
            try
            {
                NoticesModel pcm = (NoticesModel)_mapper.Map<OutModels.Models.Notices, NoticesModel>(n);
                return new JsonResult(await this._repository.Update(pcm));
            }
            catch (Exception ex)
            {
                return null;
            }
        }
        [Route("ActiveNotices")]
        [HttpPost]
        public async Task<ActionResult> ActiveNotices(OutModels.Models.Notices n)
        {
            try
            {
                int id = n.N_Id;
                if (id == 0)
                {
                    return new JsonResult(ResponseModel.Error("خبر مورد نظر یافت نشد"));
                }
                return new JsonResult(await this._repository.LogicalAvailable(id, true));
            }
            catch (Exception ex)
            {
                return null;
            }
        }
        [Route("DisableNotices")]
        [HttpPost]
        public async Task<ActionResult> DisableCategory(OutModels.Models.Notices n)
        {
            try
            {
                int id = n.N_Id;
                if (id == 0)
                {
                    return new JsonResult(ResponseModel.Error("خبر  مورد نظر یافت نشد"));
                }
                return new JsonResult(await this._repository.LogicalAvailable(id, false));
            }
            catch (Exception ex)
            {
                return null;
            }
        }
        [Route("DeleteNotices")]
        [HttpPost]
        public async Task<ActionResult> DeleteNotices(OutModels.Models.Notices n)
        {
            try
            {
                int id = n.N_Id;
                if (id == 0)
                {
                    return new JsonResult(ResponseModel.Error("خبر  مورد نظر یافت نشد"));
                }
                return new JsonResult(await this._repository.LogicalDelete(id));
            }
            catch (Exception ex)
            {
                return null;
            }
        }
    }
}