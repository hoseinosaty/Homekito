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
using Barayand.Services.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Barayand.Controllers.Cpanel.Content.Gallery
{
    [Route("api/cpanel/content/gallery/[controller]")]
    [ApiController]
    public class VideoGalleryController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly IPublicMethodRepsoitory<VideoGalleryModel> _repository;
        public VideoGalleryController(IMapper mapper,IPublicMethodRepsoitory<VideoGalleryModel> repsoitory)
        {
            this._mapper = mapper;
            this._repository = repsoitory;
        }

        [Route("AddChild")]
        [HttpPost]
        public async Task<ActionResult> AddChild(OutModels.Models.VideoGallery vg)
        {
            try
            {
                VideoGalleryModel b = (VideoGalleryModel)_mapper.Map<OutModels.Models.VideoGallery, VideoGalleryModel>(vg);
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
                List<VideoGalleryModel> data = (List<VideoGalleryModel>)(await this._repository.GetAll()).Data;
                List<VideoGallery> result = _mapper.Map<List<VideoGalleryModel>, List<VideoGallery>>(data.Where(x=>x.Lang == lang).ToList());
                return new JsonResult(ResponseModel.Success("CHILDS_LIST_RETURNED", result));
            }
            catch (Exception ex)
            {
                return null;
            }
        }
        [Route("UpdateChild")]
        [HttpPost]
        public async Task<ActionResult> UpdateGalleryChild(OutModels.Models.VideoGallery vg)
        {
            try
            {
                VideoGalleryModel pcm = (VideoGalleryModel)_mapper.Map<OutModels.Models.VideoGallery, VideoGalleryModel>(vg);
                return new JsonResult(await this._repository.Update(pcm));
            }
            catch (Exception ex)
            {
                return null;
            }
        }
        [Route("ActiveChild")]
        [HttpPost]
        public async Task<ActionResult> ActiveChild(OutModels.Models.VideoGallery vg)
        {
            try
            {
                int id = vg.VG_Id;
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
        public async Task<ActionResult> DisableChild(OutModels.Models.VideoGallery vg)
        {
            try
            {
                int id = vg.VG_Id;
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
        public async Task<ActionResult> DeleteChild(OutModels.Models.VideoGallery vg)
        {
            try
            {
                return new JsonResult(await this._repository.Delete(vg.VG_Id));
            }
            catch (Exception ex)
            {
                return null;
            }
        }
    }
}