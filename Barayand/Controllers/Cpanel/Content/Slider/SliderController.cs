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
namespace Barayand.Controllers.Cpanel.Content.Slider
{
    [Route("api/cpanel/[controller]")]
    [ApiController]
    public class SliderController : ControllerBase
    {
        private readonly IPublicMethodRepsoitory<SliderModel> _sliderrepo;
        public SliderController(IPublicMethodRepsoitory<SliderModel> sliderrepo)
        {
            this._sliderrepo = sliderrepo;
        }
        [Route("AddSlider")]
        [HttpPost]
        public async Task<ActionResult> AddChild(SliderModel s)
        {
            try
            {
                return new JsonResult(await this._sliderrepo.Insert(s));
            }
            catch (Exception ex)
            {
                return new JsonResult(ResponseModel.Error(data:ex));
            }
        }
        [Route("LoadSliders/{lang}")]
        [HttpPost]
        public async Task<ActionResult> LoadSliders(string lang = "fa")
        {
            try
            {
                List<SliderModel> data = ((List<SliderModel>)(await this._sliderrepo.GetAll()).Data).Where(x=>x.Lang == lang).ToList();
                return new JsonResult(ResponseModel.Success("CHILDS_LIST_RETURNED", data));
            }
            catch (Exception ex)
            {
                return null;
            }
        }
        [Route("UpdateSlider")]
        [HttpPost]
        public async Task<ActionResult> UpdateSlider(SliderModel s)
        {
            try
            {
                return new JsonResult(await this._sliderrepo.Update(s));
            }
            catch (Exception ex)
            {
                return null;
            }
        }
        [Route("ActiveSlider")]
        [HttpPost]
        public async Task<ActionResult> ActiveSlider(SliderModel s)
        {
            try
            {
                int id = s.S_Id;
                if (id == 0)
                {
                    return new JsonResult(ResponseModel.Error("اسلایدر مورد نظر یافت نشد"));
                }
                return new JsonResult(await this._sliderrepo.LogicalAvailable(id, true));
            }
            catch (Exception ex)
            {
                return null;
            }
        }
        [Route("DisableSlider")]
        [HttpPost]
        public async Task<ActionResult> DisableSlider(SliderModel s)
        {
            try
            {
                int id = s.S_Id;
                if (id == 0)
                {
                    return new JsonResult(ResponseModel.Error("گالری مورد نظر یافت نشد"));
                }
                return new JsonResult(await this._sliderrepo.LogicalAvailable(id, false));
            }
            catch (Exception ex)
            {
                return null;
            }
        }
    }
}
