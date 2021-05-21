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
    public class AttributeAnswerController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly IAttributeAnswerRepository _repository;

        public AttributeAnswerController(IMapper mapper, IAttributeAnswerRepository repository)
        {
            _mapper = mapper;
            _repository = repository;
        }
        [Route("LoadAnswerByRelation/{rid?}")]
        public async Task<ActionResult> GetAnswerByRelationId(int rid)
        {
            try
            {
                var answers =(List<AttrAnswerModel>)((await this._repository.GetAll())).Data;
                answers = answers.Where(x=>x.X_IsDeleted == false).ToList();
                var catAnswers = answers.Where(x=>x.X_CatAttrId == rid).ToList();
                return new JsonResult(ResponseModel.Success(data:catAnswers));
            }
            catch(Exception ex)
            {
                return null;
            }
        }
        [Route("ActiveAttrAnswer")]
        [HttpPost]
        public async Task<ActionResult> ActiveAttribute(OutModels.Models.AttrAnswer attribute)
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
        [Route("DisableAttrAnswer")]
        [HttpPost]
        public async Task<ActionResult> DisableAttribute(OutModels.Models.AttrAnswer attribute)
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
        [Route("DeleteAttrAnswer")]
        [HttpPost]
        public async Task<ActionResult> DeleteAttribute(OutModels.Models.AttrAnswer attribute)
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
        [Route("AddAnswer")]
        [HttpPost]
        public async Task<ActionResult> AddAnswer(AttrAnswerModel attribute)
        {
            try
            {
                return new JsonResult(await this._repository.AddAnswer(attribute));
            }
            catch (Exception ex)
            {
                return null;
            }
        }
        [Route("UpdateAnswer")]
        [HttpPost]
        public async Task<ActionResult> UpdateAnswer(AttrAnswerModel attribute)
        {
            try
            {
                return new JsonResult(await this._repository.UpdateAnswer(attribute));
            }
            catch (Exception ex)
            {
                return null;
            }
        }
    }
}