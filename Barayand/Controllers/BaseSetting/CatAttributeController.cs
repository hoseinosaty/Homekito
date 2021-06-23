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
        private readonly IPublicMethodRepsoitory<AttributeModel> _attrs;
        public CatAttributeController(IMapper mapper, IPublicMethodRepsoitory<CatAttrRelationModel> repository, IPublicMethodRepsoitory<AttributeModel> attrs)
        {
            this._repository = repository;
            this._mapper = mapper;
            _attrs = attrs;
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
                var catattrs = ((List<CatAttrRelationModel>)(await _repository.GetAll()).Data).Where(x=>x.X_IsDeleted == false && x.X_Status && x.X_CatId == catid).ToList();
                List<AttributeModel> data = ((List<AttributeModel>)(await _attrs.GetAll()).Data);
                List<object> Data = new List<object>();

                foreach (var item in catattrs)
                {
                    var attribute = data.FirstOrDefault(x => x.A_Id == item.X_AttrId);
                    if(attribute != null)
                    {
                        Data.Add(new
                        {
                            A_Title = attribute.A_Title,
                            A_Id = attribute.A_Id,
                            A_Status = item.X_Status,
                            relationId = item.X_Id,
                        });
                    }
                }
                return new JsonResult(ResponseModel.Success(data:Data));
            }
            catch (Exception ex)
            {
                return new JsonResult(ResponseModel.ServerInternalError(data:ex));
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
                return new JsonResult(ResponseModel.ServerInternalError(data: ex));
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
                return new JsonResult(ResponseModel.ServerInternalError(data: ex));
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
                return new JsonResult(ResponseModel.ServerInternalError(data: ex));
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
                return new JsonResult(ResponseModel.ServerInternalError(data: ex));
            }
        }
    }
}