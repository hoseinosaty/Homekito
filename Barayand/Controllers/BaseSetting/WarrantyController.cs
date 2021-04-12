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
    public class WarrantyController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly IPublicMethodRepsoitory<WarrantyModel> _repository;
        public WarrantyController(IMapper mapper, IPublicMethodRepsoitory<WarrantyModel> repository)
        {
            this._repository = repository;
            this._mapper = mapper;
        }
        [Route("AddWarranty")]
        [HttpPost]
        public async Task<ActionResult> AddWarranty(OutModels.Models.Warranty warranty)
        {
            try
            {
                WarrantyModel wm = (WarrantyModel)_mapper.Map<OutModels.Models.Warranty, WarrantyModel>(warranty);
                return new JsonResult(await this._repository.Insert(wm));
            }
            catch (Exception ex)
            {
                return null;
            }
        }
        [Route("UpdateWarranty")]
        [HttpPost]
        public async Task<ActionResult> UpdateWarranty(OutModels.Models.Warranty warranty)
        {
            try
            {
                WarrantyModel wm = (WarrantyModel)_mapper.Map<OutModels.Models.Warranty, WarrantyModel>(warranty);
                return new JsonResult(await this._repository.Update(wm));
            }
            catch (Exception ex)
            {
                return null;
            }
        }
        [Route("ActiveWarranty")]
        [HttpPost]
        public async Task<ActionResult> ActiveWarranty(OutModels.Models.Warranty warranty)
        {
            try
            {
                int id = warranty.W_Id;
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
        [Route("DisableWarranty")]
        [HttpPost]
        public async Task<ActionResult> DisableWarranty(OutModels.Models.Warranty warranty)
        {
            try
            {
                int id = warranty.W_Id;
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
        [Route("DeleteWarranty")]
        [HttpPost]
        public async Task<ActionResult> DeleteWarranty(OutModels.Models.Warranty warranty)
        {
            try
            {
                int id = warranty.W_Id;
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
        [Route("LoadWarranty")]
        [HttpPost]
        public async Task<ActionResult> GetAllWarranties()
        {
            try
            {
                List<WarrantyModel> data = (List<WarrantyModel>)(await this._repository.GetAll()).Data;
                List<OutModels.Models.Warranty> result = _mapper.Map<List<WarrantyModel>, List<OutModels.Models.Warranty>>(data);
                return new JsonResult(ResponseModel.Success("WARRANTY_LIST_RETURNED", result));
            }
            catch (Exception ex)
            {
                return null;
            }
        }
        [Route("LoadWarrantyCombo")]
        [HttpPost]
        public async Task<ActionResult> GetAllWarrantiesCombo()
        {
            try
            {
                List<WarrantyModel> data = (List<WarrantyModel>)(await this._repository.GetAll()).Data;
                List<ComboItems.Warranty> result = _mapper.Map<List<WarrantyModel>, List<ComboItems.Warranty>>(data);
                return new JsonResult(ResponseModel.Success("WARRANTY_LIST_RETURNED", result));
            }
            catch (Exception ex)
            {
                return null;
            }
        }
    }
}