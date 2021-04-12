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

namespace Barayand.Controllers.Cpanel.Content.Gallery
{
    [Route("api/cpanel/content/gallery/[controller]")]
    [ApiController]
    public class GalleryCatController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly IPublicMethodRepsoitory<GalleryCategoryModel> _repository;

        public GalleryCatController(IMapper mapper,IPublicMethodRepsoitory<GalleryCategoryModel> repository)
        {
            this._repository = repository;
            this._mapper = mapper;
        }

        [Route("AddCategory")]
        [HttpPost]
        public async Task<ActionResult> AddCategory(OutModels.Models.GalleryCategory cat)
        {
            try
            {
                GalleryCategoryModel b = (GalleryCategoryModel)_mapper.Map<OutModels.Models.GalleryCategory, GalleryCategoryModel>(cat);
                return new JsonResult(await this._repository.Insert(b));
            }
            catch (Exception ex)
            {
                return null;
            }
        }
        [Route("LoadGalleryCats/{type?}/{lang?}")]
        [HttpPost]
        public async Task<ActionResult> LoadGalleryCats(int type = 1,string lang="fa")
        {
            try
            {
                List<GalleryCategoryModel> data = (List<GalleryCategoryModel>)(await this._repository.GetAll()).Data;
                List<GalleryCategory> result = _mapper.Map<List<GalleryCategoryModel>, List<GalleryCategory>>(data.Where(x=>x.GC_Type == type).ToList());
                return new JsonResult(ResponseModel.Success("CATEGORY_LIST_RETURNED", result));
            }
            catch (Exception ex)
            {
                return null;
            }
        }
        [Route("UpdateGalleryCategory")]
        [HttpPost]
        public async Task<ActionResult> UpdateGalleryCategory(OutModels.Models.GalleryCategory cat)
        {
            try
            {
                GalleryCategoryModel pcm = (GalleryCategoryModel)_mapper.Map<OutModels.Models.GalleryCategory, GalleryCategoryModel>(cat);
                return new JsonResult(await this._repository.Update(pcm));
            }
            catch (Exception ex)
            {
                return null;
            }
        }
        [Route("ActiveCategory")]
        [HttpPost]
        public async Task<ActionResult> ActiveCategory(OutModels.Models.GalleryCategory cat)
        {
            try
            {
                int id = cat.GC_Id;
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
        public async Task<ActionResult> DisableCategory(OutModels.Models.GalleryCategory cat)
        {
            try
            {
                int id = cat.GC_Id;
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
        public async Task<ActionResult> DeleteCategory(OutModels.Models.GalleryCategory cat)
        {
            try
            {
                int id = cat.GC_Id;
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
        public async Task<ActionResult> GetAllCatsComboItems(int type = 1,string lang = "fa")
        {
            try
            {
                List<GalleryCategoryModel> data = (List<GalleryCategoryModel>)(await this._repository.GetAll()).Data;
                List<ComboItems.IGCategory> result = _mapper.Map<List<GalleryCategoryModel>, List<ComboItems.IGCategory>>(data.Where(x=>x.GC_Type == type ).ToList());
                return new JsonResult(ResponseModel.Success("CATEGORIES_LIST_RETURNED", result));
            }
            catch (Exception ex)
            {
                return null;
            }
        }
    }
}