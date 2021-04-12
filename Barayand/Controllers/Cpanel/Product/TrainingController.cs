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
    public class TrainingController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly IPublicMethodRepsoitory<TrainingModel> _repository;
        private readonly IPRRepository _productrelationrepo;
        public TrainingController(IMapper mapper, IPublicMethodRepsoitory<TrainingModel> repository, IPRRepository productrelationrepo)
        {
            this._repository = repository;
            this._productrelationrepo = productrelationrepo;
            this._mapper = mapper;
        }
        [Route("AddTrain")]
        [HttpPost]
        public async Task<ActionResult> AddTrain(OutModels.Models.Trainings product)
        {
            try
            {
                TrainingModel b = (TrainingModel)_mapper.Map<OutModels.Models.Trainings, TrainingModel>(product);
                return new JsonResult(await this._repository.Insert(b));
            }
            catch (Exception ex)
            {
                return null;
            }
        }
        [Route("UpdateTrain")]
        [HttpPost]
        public async Task<ActionResult> UpdateTrain(OutModels.Models.Trainings product)
        {
            try
            {
                TrainingModel b = (TrainingModel)_mapper.Map<OutModels.Models.Trainings, TrainingModel>(product);
                return new JsonResult(await this._repository.Update(b));
            }
            catch (Exception ex)
            {
                return null;
            }
        }
        [Route("ActiveTrain")]
        [HttpPost]
        public async Task<ActionResult> ActiveTrain(OutModels.Models.Trainings product)
        {
            try
            {
                int id = product.T_Id;
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
        [Route("DisableTrain")]
        [HttpPost]
        public async Task<ActionResult> DisableTrain(OutModels.Models.Trainings product)
        {
            try
            {
                int id = product.T_Id;
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
        [Route("DeleteTrain")]
        [HttpPost]
        public async Task<ActionResult> DeleteTrain(OutModels.Models.Trainings product)
        {
            try
            {
                int id = product.T_Id;
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
        [Route("LoadTrainings/{catid}")]
        [HttpPost]
        public async Task<ActionResult> LoadTrainings(int catid = 0)
        {
            try
            {
                List<TrainingModel> data = (List<TrainingModel>)(await this._repository.GetAll()).Data;
                List<OutModels.Models.Trainings> result = _mapper.Map<List<TrainingModel>, List<OutModels.Models.Trainings>>(data.Where(x => x.T_EndLevelCatId == catid).ToList());
                return new JsonResult(ResponseModel.Success("TRAININGS_LIST_RETURNED", result));
            }
            catch (Exception ex)
            {
                return new JsonResult(ResponseModel.ServerInternalError(data: ex));
            }
        }
    }
}