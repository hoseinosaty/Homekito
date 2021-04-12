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
    public class ExpertReviewController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly IExpertReviewRepository _repository;
        public ExpertReviewController(IMapper mapper, IExpertReviewRepository repository)
        {
            _mapper = mapper;
            _repository = repository;
        }
        [Route("AddExpertReview")]
        [HttpPost]
        public async Task<IActionResult> AddExpert(OutModels.Models.ExpertReview er)
        {
            try
            {
                ExpertReviewModel erm = (ExpertReviewModel)_mapper.Map<OutModels.Models.ExpertReview, ExpertReviewModel>(er);
                return new JsonResult(await _repository.Insert(erm));
            }
            catch(Exception ex)
            {
                return new JsonResult(ResponseModel.ServerInternalError(data:ex));
            }
        }
        [Route("UpdateExpertReview")]
        [HttpPost]
        public async Task<IActionResult> UpdateExpertReview(OutModels.Models.ExpertReview er)
        {
            try
            {
                ExpertReviewModel erm = (ExpertReviewModel)_mapper.Map<OutModels.Models.ExpertReview, ExpertReviewModel>(er);
                return new JsonResult(await _repository.Update(erm));
            }
            catch (Exception ex)
            {
                return new JsonResult(ResponseModel.ServerInternalError(data: ex));
            }
        }

        [Route("DisableExpertReview")]
        [HttpPost]
        public async Task<IActionResult> DisableExpertReview(OutModels.Models.ExpertReview er)
        {
            try
            {
                return new JsonResult(await _repository.LogicalAvailable(er.E_Id,false));
            }
            catch (Exception ex)
            {
                return new JsonResult(ResponseModel.ServerInternalError(data: ex));
            }
        }
        [Route("ActiveExpertReview")]
        [HttpPost]
        public async Task<IActionResult> ActiveExpertReview(OutModels.Models.ExpertReview er)
        {
            try
            {
                return new JsonResult(await _repository.LogicalAvailable(er.E_Id, true));
            }
            catch (Exception ex)
            {
                return new JsonResult(ResponseModel.ServerInternalError(data: ex));
            }
        }
        [Route("DeleteExpertReview")]
        [HttpPost]
        public async Task<IActionResult> DeleteExpertReview(OutModels.Models.ExpertReview er)
        {
            try
            {
                return new JsonResult(await _repository.LogicalDelete(er.E_Id));
            }
            catch (Exception ex)
            {
                return new JsonResult(ResponseModel.ServerInternalError(data: ex));
            }
        }
        [Route("LoadExpertReviews/{pid}")]
        [HttpPost]
        public async Task<IActionResult> LoadExpertReviews(int pid)
        {
            try
            {
                return new JsonResult(ResponseModel.Success(data:await _repository.GetByProduct(pid)));
            }
            catch (Exception ex)
            {
                return new JsonResult(ResponseModel.ServerInternalError(data: ex));
            }
        }
    }
}
