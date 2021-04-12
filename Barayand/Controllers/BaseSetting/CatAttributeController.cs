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
    public class CatAttributeController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly IPublicMethodRepsoitory<CatAttrRelationModel> _repository;
        public CatAttributeController(IMapper mapper, IPublicMethodRepsoitory<CatAttrRelationModel> repository)
        {
            this._repository = repository;
            this._mapper = mapper;
        }
        [Route("AddCategoryAttribute")]
        [HttpPost]
        public async Task<ActionResult> AddRelation(OutModels.Models.CatAttrRelation relation)
        {
            try
            {
                CatAttrRelationModel am = (CatAttrRelationModel)_mapper.Map<OutModels.Models.CatAttrRelation, CatAttrRelationModel>(relation);
                return new JsonResult(await this._repository.Insert(am));
            }
            catch (Exception ex)
            {
                return null;
            }
        }
        [Route("LoadAttrsByCategory/{catid?}")]
        [HttpPost]
        public async Task<ActionResult> GetAllAttrsByCat(int catid)
        {
            try
            {
                CatAttrRelationRepository carr = new CatAttrRelationRepository(new DAL.Context.BarayandContext(null));
                return new JsonResult(await carr.GetAttrsByCat(catid));
            }
            catch (Exception ex)
            {
                return null;
            }
        }
        [Route("UpdateCatAttribute")]
        [HttpPost]
        public async Task<ActionResult> UpdateCatAttr(OutModels.Models.CatAttrRelation attribute)
        {
            try
            {
                CatAttrRelationModel am = (CatAttrRelationModel)_mapper.Map<OutModels.Models.CatAttrRelation, CatAttrRelationModel>(attribute);
                return new JsonResult(await this._repository.Update(am));
            }
            catch (Exception ex)
            {
                return null;
            }
        }
        [Route("DeleteCatAttribute")]
        [HttpPost]
        public async Task<ActionResult> DeleteCatAttribute(OutModels.Models.CatAttrRelation attribute)
        {
            try
            {
                int id = attribute.X_Id;
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
        [Route("ActiveCatAttribute")]
        [HttpPost]
        public async Task<ActionResult> ActiveCatAttribute(OutModels.Models.CatAttrRelation attribute)
        {
            try
            {
                int id = attribute.X_Id;
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
        [Route("DisableCatAttribute")]
        [HttpPost]
        public async Task<ActionResult> DisableCatAttribute(OutModels.Models.CatAttrRelation attribute)
        {
            try
            {
                int id = attribute.X_Id;
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
    }
}