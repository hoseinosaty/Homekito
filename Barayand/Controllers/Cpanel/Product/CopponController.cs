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

namespace Barayand.Controllers.Cpanel.Product
{
    [Route("api/cpanel/[controller]")]
    [ApiController]
    public class CopponController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly IPublicMethodRepsoitory<CopponModel> _repository;
        public CopponController(IMapper mapper, IPublicMethodRepsoitory<CopponModel> repository)
        {
            this._repository = repository;
            this._mapper = mapper;
        }
        [Route("AddCoppon")]
        [HttpPost]
        public async Task<ActionResult> AddCoppon(OutModels.Models.Coppon coppon)
        {
            try
            {
                CopponModel b = (CopponModel)_mapper.Map<OutModels.Models.Coppon, CopponModel>(coppon);
                return new JsonResult(await this._repository.Insert(b));
            }
            catch (Exception ex)
            {
                return null;
            }
        }
        [Route("UpdateCoppon")]
        [HttpPost]
        public async Task<ActionResult> UpdateCoppon(OutModels.Models.Coppon coppon)
        {
            try
            {
                CopponModel wm = (CopponModel)_mapper.Map<OutModels.Models.Coppon, CopponModel>(coppon);
                return new JsonResult(await this._repository.Update(wm));
            }
            catch (Exception ex)
            {
                return null;
            }
        }
        [Route("ActiveCoppon")]
        [HttpPost]
        public async Task<ActionResult> ActiveCoppon(OutModels.Models.Coppon coppon)
        {
            try
            {
                int id = coppon.CP_Id;
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
        [Route("DisableCoppon")]
        [HttpPost]
        public async Task<ActionResult> DisableCoppon(OutModels.Models.Coppon coppon)
        {
            try
            {
                int id = coppon.CP_Id;
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
        [Route("DeleteCoppon")]
        [HttpPost]
        public async Task<ActionResult> DeleteCoppon(OutModels.Models.Coppon coppon)
        {
            try
            {
                int id =coppon.CP_Id;
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
        [Route("LoadCoppon")]
        [HttpPost]
        public async Task<ActionResult> GetAllCoppons()
        {
            try
            {
                List<CopponModel> data = ((List<CopponModel>)(await this._repository.GetAll()).Data).Where(x => !x.CP_IsDeleted).ToList();
                List<OutModels.Models.Coppon> result = _mapper.Map<List<CopponModel>, List<OutModels.Models.Coppon>>(data);
                return new JsonResult(ResponseModel.Success("COPPONS_LIST_RETURNED", data));
            }
            catch (Exception ex)
            {
                return null;
            }
        }
    }
}