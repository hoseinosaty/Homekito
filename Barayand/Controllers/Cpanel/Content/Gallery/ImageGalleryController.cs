using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Barayand.Common.Services;
using Barayand.DAL.Interfaces;
using Barayand.Models.Entity;
using Barayand.OutModels.Models;
using Barayand.OutModels.Response;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Barayand.Controllers.Cpanel.Content.Gallery
{
    [Route("api/cpanel/content/gallery/[controller]")]
    [ApiController]
    public class ImageGalleryController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly IPublicMethodRepsoitory<ImageGalleryModel> _repository;
        UploaderService _uploaderService;
        public ImageGalleryController(IMapper mapper,IPublicMethodRepsoitory<ImageGalleryModel> repsoitory)
        {
            this._mapper = mapper;
            this._repository = repsoitory;
            this._uploaderService = new UploaderService();
        }

        [Route("AddChild")]
        [HttpPost]
        public async Task<ActionResult> AddChild(OutModels.Models.ImageGallery ig)
        {
            try
            {
                ImageGalleryModel b = (ImageGalleryModel)_mapper.Map<OutModels.Models.ImageGallery, ImageGalleryModel>(ig);
                return new JsonResult(await this._repository.Insert(b));
            }
            catch (Exception ex)
            {
                return null;
            }
        }
        [Route("LoadChilds/{lang}")]
        [HttpPost]
        public async Task<ActionResult> LoadGalleryChilds(string lang = "fa")
        {
            try
            {
                List<ImageGalleryModel> data = (List<ImageGalleryModel>)(await this._repository.GetAll()).Data;
                List<ImageGallery> result = _mapper.Map<List<ImageGalleryModel>, List<ImageGallery>>(data.Where(x=>x.Lang == lang).ToList());
                return new JsonResult(ResponseModel.Success("CHILDS_LIST_RETURNED", result));
            }
            catch (Exception ex)
            {
                return null;
            }
        }
        [Route("UpdateChild")]
        [HttpPost]
        public async Task<ActionResult> UpdateGalleryChild(OutModels.Models.ImageGallery ig)
        {
            try
            {
                ImageGalleryModel pcm = (ImageGalleryModel)_mapper.Map<OutModels.Models.ImageGallery, ImageGalleryModel>(ig);
                return new JsonResult(await this._repository.Update(pcm));
            }
            catch (Exception ex)
            {
                return null;
            }
        }
        [Route("ActiveChild")]
        [HttpPost]
        public async Task<ActionResult> ActiveChild(OutModels.Models.ImageGallery ig)
        {
            try
            {
                int id = ig.IG_Id;
                if (id == 0)
                {
                    return new JsonResult(ResponseModel.Error("گالری مورد نظر یافت نشد"));
                }
                return new JsonResult(await this._repository.LogicalAvailable(id, true));
            }
            catch (Exception ex)
            {
                return null;
            }
        }
        [Route("DisableChild")]
        [HttpPost]
        public async Task<ActionResult> DisableChild(OutModels.Models.ImageGallery ig)
        {
            try
            {
                int id = ig.IG_Id;
                if (id == 0)
                {
                    return new JsonResult(ResponseModel.Error("گالری مورد نظر یافت نشد"));
                }
                return new JsonResult(await this._repository.LogicalAvailable(id, false));
            }
            catch (Exception ex)
            {
                return null;
            }
        }
        [Route("DeleteChild")]
        [HttpPost]
        public async Task<ActionResult> DeleteChild(OutModels.Models.ImageGallery ig)
        {
            try
            {
                ImageGalleryModel igm = await this._repository.GetById(ig.IG_Id);
                var res = this._uploaderService.RemoveFile("IMAGEGALLERY", "GALLERYCHILD",igm.IG_ImageUrl);
                return new JsonResult(await this._repository.Delete(igm));
            }
            catch (Exception ex)
            {
                return null;
            }
        }
    }
}