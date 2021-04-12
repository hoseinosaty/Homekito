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
    [Route("api/cpanel/notices/[controller]")]
    [ApiController]
    public class NoticesCategoryController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly IPublicMethodRepsoitory<NoticesCategoryModel> _repository;
        public NoticesCategoryController(IMapper mapper, IPublicMethodRepsoitory<NoticesCategoryModel> repository)
        {
            this._repository = repository;
            this._mapper = mapper;
        }
        [Route("AddCategory")]
        [HttpPost]
        public async Task<ActionResult> AddCategory(OutModels.Models.NoticesGategory cat)
        {
            try
            {
                NoticesCategoryModel b = (NoticesCategoryModel)_mapper.Map<OutModels.Models.NoticesGategory, NoticesCategoryModel>(cat);
                return new JsonResult(await this._repository.Insert(b));
            }
            catch (Exception ex)
            {
                return null;
            }
        }
        [Route("LoadNoticesCats/{type?}/{lang}")]
        [HttpPost]
        public async Task<ActionResult> LoadGalleryCats(int type = 1,string lang = null)
        {
            try
            {
                List<NoticesCategoryModel> data = (List<NoticesCategoryModel>)(await this._repository.GetAll()).Data;
                List<NoticesGategory> result = _mapper.Map<List<NoticesCategoryModel>, List<NoticesGategory>>(data.Where(x => x.NC_Type == type && x.Lang == lang).ToList());
                return new JsonResult(ResponseModel.Success("CATEGORY_LIST_RETURNED", result));
            }
            catch (Exception ex)
            {
                return null;
            }
        }
        [Route("UpdateNoticesGategory")]
        [HttpPost]
        public async Task<ActionResult> UpdateNoticesGategory(OutModels.Models.NoticesGategory cat)
        {
            try
            {
                NoticesCategoryModel pcm = (NoticesCategoryModel)_mapper.Map<OutModels.Models.NoticesGategory, NoticesCategoryModel>(cat);
                return new JsonResult(await this._repository.Update(pcm));
            }
            catch (Exception ex)
            {
                return null;
            }
        }
        [Route("ActiveCategory")]
        [HttpPost]
        public async Task<ActionResult> ActiveCategory(OutModels.Models.NoticesGategory cat)
        {
            try
            {
                int id = cat.NC_Id;
                if (id == 0)
                {
                    return new JsonResult(ResponseModel.Error("دسته بندی مورد نظر یافت نشد"));
                }
                return new JsonResult(await this._repository.LogicalAvailable(id, true));
            }
            catch (Exception ex)
            {
                return null;
            }
        }
        [Route("DisableCategory")]
        [HttpPost]
        public async Task<ActionResult> DisableCategory(OutModels.Models.NoticesGategory cat)
        {
            try
            {
                int id = cat.NC_Id;
                if (id == 0)
                {
                    return new JsonResult(ResponseModel.Error("دسته بندی مورد نظر یافت نشد"));
                }
                return new JsonResult(await this._repository.LogicalAvailable(id, false));
            }
            catch (Exception ex)
            {
                return null;
            }
        }
        [Route("DeleteCategory")]
        [HttpPost]
        public async Task<ActionResult> DeleteCategory(OutModels.Models.NoticesGategory cat)
        {
            try
            {
                int id = cat.NC_Id;
                if (id == 0)
                {
                    return new JsonResult(ResponseModel.Error("دسته بندی مورد نظر یافت نشد"));
                }
                return new JsonResult(await this._repository.LogicalDelete(id));
            }
            catch (Exception ex)
            {
                return null;
            }
        }
        [Route("LoadIGComboItems/{type?}/{lang?}")]
        [HttpPost]
        public async Task<ActionResult> GetAllCatsComboItems(int type = 1,string lang = null)
        {
            try
            {
                List<NoticesCategoryModel> data = (List<NoticesCategoryModel>)(await this._repository.GetAll()).Data;
                List<ComboItems.NoticesCategory> result = _mapper.Map<List<NoticesCategoryModel>, List<ComboItems.NoticesCategory>>(data.Where(x => x.NC_Type == type && x.Lang == lang).ToList());
                return new JsonResult(ResponseModel.Success("CATEGORIES_LIST_RETURNED", result));
            }
            catch (Exception ex)
            {
                return null;
            }
        }
    }
}