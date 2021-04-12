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
    public class EnergyGiftWrappController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly IPublicMethodRepsoitory<EnergyUsageModel> _repository;
        public EnergyGiftWrappController(IMapper mapper, IPublicMethodRepsoitory<EnergyUsageModel> repository)
        {
            this._repository = repository;
            this._mapper = mapper;
        }
        [Route("AddEnergyGiftWrapp")]
        [HttpPost]
        public async Task<ActionResult> Add(OutModels.Models.EnergyUsage formula)
        {
            try
            {
                EnergyUsageModel am = (EnergyUsageModel)_mapper.Map<OutModels.Models.EnergyUsage, EnergyUsageModel>(formula);
                return new JsonResult(await this._repository.Insert(am));
            }
            catch (Exception ex)
            {
                return new JsonResult(ResponseModel.ServerInternalError(data: ex));
            }
        }
        [Route("UpdateEnergyGiftWrapp")]
        [HttpPost]
        public async Task<ActionResult> Update(OutModels.Models.EnergyUsage formula)
        {
            try
            {
                EnergyUsageModel am = (EnergyUsageModel)_mapper.Map<OutModels.Models.EnergyUsage, EnergyUsageModel>(formula);
                return new JsonResult(await this._repository.Update(am));
            }
            catch (Exception ex)
            {
                return new JsonResult(ResponseModel.ServerInternalError(data: ex));
            }
        }
        [Route("ActiveEnergyGiftWrapp")]
        [HttpPost]
        public async Task<ActionResult> Active(OutModels.Models.EnergyUsage attribute)
        {
            try
            {
                int id = attribute.E_Id;
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
        [Route("DisableEnergyGiftWrapp")]
        [HttpPost]
        public async Task<ActionResult> Disable(OutModels.Models.EnergyUsage attribute)
        {
            try
            {
                int id = attribute.E_Id;
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
        [Route("DeleteEnergyGiftWrapp")]
        [HttpPost]
        public async Task<ActionResult> Delete(OutModels.Models.EnergyUsage attribute)
        {
            try
            {
                int id = attribute.E_Id;
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
        [Route("LoadEnergyGiftWrapp/{type?}")]
        [HttpPost]
        public async Task<ActionResult> Load(int type = 1)
        {
            try
            {
                return new JsonResult(ResponseModel.Success(data: ((List<EnergyUsageModel>)(await this._repository.GetAll()).Data).Where(x => x.E_IsDeleted == false && x.E_Type == type).ToList()));
            }
            catch (Exception ex)
            {
                return new JsonResult(ResponseModel.ServerInternalError(data: ex));
            }
        }
        [Route("LoadComboEnergyGiftWrapp/{type?}")]
        [HttpPost]
        public async Task<ActionResult> LoadComboItems(int type = 1)
        {
            try
            {
                List<EnergyUsageModel> data = ((List<EnergyUsageModel>)(await this._repository.GetAll()).Data).Where(x => x.E_Status == true && x.E_IsDeleted == false && x.E_Type == type).OrderBy(x => x.Created_At).ToList();
                List<ComboItems.EnergyGiftWrapper> result = _mapper.Map<List<EnergyUsageModel>, List<ComboItems.EnergyGiftWrapper>>(data);
                return new JsonResult(ResponseModel.Success("ENERGYUSAGE_LIST_RETURNED", result));
            }
            catch (Exception ex)
            {
                return new JsonResult(ResponseModel.ServerInternalError(data: ex));
            }
        }
    }
}
