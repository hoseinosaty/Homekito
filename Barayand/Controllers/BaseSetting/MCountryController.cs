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
    public class MCountryController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly IPublicMethodRepsoitory<ManufacturContryModel> _repository;
        public MCountryController(IMapper mapper, IPublicMethodRepsoitory<ManufacturContryModel> repository)
        {
            this._repository = repository;
            this._mapper = mapper;
        }
        [Route("AddMCountry")]
        [HttpPost]
        public async Task<ActionResult> Add(OutModels.Models.ManufactureCuntry formula)
        {
            try
            {
                ManufacturContryModel am = (ManufacturContryModel)_mapper.Map<OutModels.Models.ManufactureCuntry, ManufacturContryModel>(formula);
                return new JsonResult(await this._repository.Insert(am));
            }
            catch (Exception ex)
            {
                return new JsonResult(ResponseModel.ServerInternalError(data:ex));
            }
        }
        [Route("UpdateMCountry")]
        [HttpPost]
        public async Task<ActionResult> Update(OutModels.Models.ManufactureCuntry formula)
        {
            try
            {
                ManufacturContryModel am = (ManufacturContryModel)_mapper.Map<OutModels.Models.ManufactureCuntry, ManufacturContryModel>(formula);
                return new JsonResult(await this._repository.Update(am));
            }
            catch (Exception ex)
            {
                return new JsonResult(ResponseModel.ServerInternalError(data: ex));
            }
        }
        [Route("ActiveMCountry")]
        [HttpPost]
        public async Task<ActionResult> Active(OutModels.Models.ManufactureCuntry attribute)
        {
            try
            {
                int id = attribute.M_Id;
                if (id == 0)
                {
                    return new JsonResult(ResponseModel.Error("موردی یافت نشد"));
                }
                return new JsonResult(await this._repository.LogicalAvailable(id, true));
            }
            catch (Exception ex)
            {
                return new JsonResult(ResponseModel.ServerInternalError(data: ex));
            }
        }
        [Route("DisableMCountry")]
        [HttpPost]
        public async Task<ActionResult> Disable(OutModels.Models.ManufactureCuntry attribute)
        {
            try
            {
                int id = attribute.M_Id;
                if (id == 0)
                {
                    return new JsonResult(ResponseModel.Error("موردی یافت نشد"));
                }
                return new JsonResult(await this._repository.LogicalAvailable(id, false));
            }
            catch (Exception ex)
            {
                return new JsonResult(ResponseModel.ServerInternalError(data: ex));
            }
        }
        [Route("DeleteMCountry")]
        [HttpPost]
        public async Task<ActionResult> Delete(OutModels.Models.ManufactureCuntry attribute)
        {
            try
            {
                int id = attribute.M_Id;
                if (id == 0)
                {
                    return new JsonResult(ResponseModel.Error("موردی یافت نشد"));
                }
                return new JsonResult(await this._repository.LogicalDelete(id));
            }
            catch (Exception ex)
            {
                return new JsonResult(ResponseModel.ServerInternalError(data: ex));
            }
        }
        [Route("LoadMCountry")]
        [HttpPost]
        public async Task<ActionResult> Load()
        {
            try
            {
                return new JsonResult(ResponseModel.Success(data: ((List<ManufacturContryModel>)(await this._repository.GetAll()).Data).Where(x => x.M_IsDeleted == false).ToList()));
            }
            catch (Exception ex)
            {
                return new JsonResult(ResponseModel.ServerInternalError(data: ex));
            }
        }
        [Route("LoadMCountryComboItems")]
        [HttpPost]
        public async Task<ActionResult> LoadComboItems()
        {
            try
            {
                List<ManufacturContryModel> data = ((List<ManufacturContryModel>)(await this._repository.GetAll()).Data).Where(x => x.M_Status == true && x.M_IsDeleted == false).OrderBy(x => x.Created_At).ToList();
                List<ComboItems.MCountry> result = _mapper.Map<List<ManufacturContryModel>, List<ComboItems.MCountry>>(data);
                return new JsonResult(ResponseModel.Success("MCOUNTRY_LIST_RETURNED", result));
            }
            catch (Exception ex)
            {
                return new JsonResult(ResponseModel.ServerInternalError(data: ex));
            }
        }
    }
}
