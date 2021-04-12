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
    public class AttributeController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly IPublicMethodRepsoitory<AttributeModel> _repository;
        
        public AttributeController(IMapper mapper, IPublicMethodRepsoitory<AttributeModel> repository)
        {
            this._repository = repository;
            this._mapper = mapper;
        }
        [Route("AddAttribute")]
        [HttpPost]
        public async Task<ActionResult> AddAttribute(OutModels.Models.Attribute attribute)
        {
            try
            {
                AttributeModel am = (AttributeModel)_mapper.Map<OutModels.Models.Attribute, AttributeModel>(attribute);
                return new JsonResult(await this._repository.Insert(am));
            }
            catch (Exception ex)
            {
                return null;
            }
        }
        [Route("UpdateAttribute")]
        [HttpPost]
        public async Task<ActionResult> UpdateAttribute(OutModels.Models.Attribute attribute)
        {
            try
            {
                AttributeModel am = (AttributeModel)_mapper.Map<OutModels.Models.Attribute, AttributeModel>(attribute);
                return new JsonResult(await this._repository.Update(am));
            }
            catch (Exception ex)
            {
                return null;
            }
        }
        [Route("ActiveAttribute")]
        [HttpPost]
        public async Task<ActionResult> ActiveAttribute(OutModels.Models.Attribute attribute)
        {
            try
            {
                int id = attribute.A_Id;
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
        [Route("DisableAttribute")]
        [HttpPost]
        public async Task<ActionResult> DisableAttribute(OutModels.Models.Attribute attribute)
        {
            try
            {
                int id = attribute.A_Id;
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
        [Route("DeleteAttribute")]
        [HttpPost]
        public async Task<ActionResult> DeleteAttribute(OutModels.Models.Attribute attribute)
        {
            try
            {
                int id = attribute.A_Id;
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
        [Route("LoadAttributes/{lang}")]
        [HttpPost]
        public async Task<ActionResult> GetAllAttributes(string lang)
        {
            try
            {
                var All = await this._repository.GetAll();
                List<AttributeModel> data = ((List<AttributeModel>)(All).Data).Where(x=>x.Lang == lang && x.A_IsDeleted == false).OrderBy(x=>x.A_SortField).ToList();
                List<OutModels.Models.Attribute> result = _mapper.Map<List<AttributeModel>, List<OutModels.Models.Attribute>>(data);
                return new JsonResult(ResponseModel.Success("ATTRIBUTES_LIST_RETURNED", result));
            }
            catch (Exception ex)
            {
                return null;
            }
        }
        [Route("LoadAttrsComboItems/{lang}")]
        [HttpPost]
        public async Task<ActionResult> GetAllAttrsComboItems(string lang)
        {
            try
            {
                List<AttributeModel> data = ((List<AttributeModel>)(await this._repository.GetAll()).Data).Where(x=>x.Lang == lang && x.A_IsDeleted == false).OrderBy(x=>x.A_SortField).ToList();
                List<ComboItems.Attribute> result = _mapper.Map<List<AttributeModel>, List<ComboItems.Attribute>>(data);
                return new JsonResult(ResponseModel.Success("ATTRIBUTE_LIST_RETURNED", result));
            }
            catch (Exception ex)
            {
                return null;
            }
        }
        
    }
}