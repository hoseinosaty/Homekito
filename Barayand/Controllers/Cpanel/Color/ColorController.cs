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

namespace Barayand.Controllers.Cpanel.Color
{
    [Route("api/cpanel/product/[controller]")]
    [ApiController]
    public class ColorController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly IPublicMethodRepsoitory<ColorModel> _repository;
        public ColorController(IMapper mapper, IPublicMethodRepsoitory<ColorModel> repository)
        {
            this._repository = repository;
            this._mapper = mapper;
        }
        [Route("AddColor")]
  
        public async Task<ActionResult> AddColor(OutModels.Models.Color color)
        {
            try
            {
                ColorModel cm = (ColorModel)_mapper.Map<OutModels.Models.Color, ColorModel>(color);
                return new JsonResult(await this._repository.Insert(cm));
            }
            catch (Exception ex)
            {
                return null;
            }
        }
        [Route("UpdateColor")]
        [HttpPost]
        public async Task<ActionResult> UpdateColor(OutModels.Models.Color color)
        {
            try
            {
                ColorModel cm = (ColorModel)_mapper.Map<OutModels.Models.Color, ColorModel>(color);
                return new JsonResult(await this._repository.Update(cm));
            }
            catch (Exception ex)
            {
                return null;
            }
        }
        [Route("ActiveColor")]
        [HttpPost]
        public async Task<ActionResult> ActiveColor(OutModels.Models.Color color)
        {
            try
            {
                int id = color.C_Id;
                if (id == 0)
                {
                    return new JsonResult(ResponseModel.Error("موردی یافت نشد"));
                }
                return new JsonResult(await this._repository.LogicalAvailable(id, true));
            }
            catch (Exception ex)
            {
                return null;
            }
        }
        [Route("DisableColor")]
        [HttpPost]
        public async Task<ActionResult> DisableColor(OutModels.Models.Color color)
        {
            try
            {
                int id = color.C_Id;
                if (id == 0)
                {
                    return new JsonResult(ResponseModel.Error("موردی یافت نشد"));
                }
                return new JsonResult(await this._repository.LogicalAvailable(id, false));
            }
            catch (Exception ex)
            {
                return null;
            }
        }
        [Route("DeleteColor")]
        [HttpPost]
        public async Task<ActionResult> DeleteColor(OutModels.Models.Color color)
        {
            try
            {
                int id = color.C_Id;
                if (id == 0)
                {
                    return new JsonResult(ResponseModel.Error("موردی یافت نشد"));
                }
                return new JsonResult(await this._repository.LogicalDelete(id));
            }
            catch (Exception ex)
            {
                return null;
            }
        }
        [Route("LoadColors")]
        [HttpPost]
        public async Task<ActionResult> GetAllColors()
        {
            try
            {
                List<ColorModel> data = (List<ColorModel>)(await this._repository.GetAll()).Data;
                List<OutModels.Models.Color> result = _mapper.Map<List<ColorModel>, List<OutModels.Models.Color>>(data);
                return new JsonResult(ResponseModel.Success("COLOR_LIST_RETURNED", result));
            }
            catch (Exception ex)
            {
                return null;
            }
        }
        [Route("LoadColorsCombo")]
        [HttpPost]
        public async Task<ActionResult> GetAllColorsCombo()
        {
            try
            {
                List<ColorModel> data = (List<ColorModel>)(await this._repository.GetAll()).Data;
                List<ComboItems.Color> result = _mapper.Map<List<ColorModel>, List<ComboItems.Color>>(data);
                return new JsonResult(ResponseModel.Success("COLOR_LIST_RETURNED", result));
            }
            catch (Exception ex)
            {
                return null;
            }
        }
    }
}