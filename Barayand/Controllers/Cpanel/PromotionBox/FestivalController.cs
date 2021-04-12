using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Barayand.DAL.Interfaces;
using Barayand.Models.Entity;
using Barayand.OutModels.Miscellaneous;
using Barayand.OutModels.Response;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace Barayand.Controllers.Cpanel.PromotionBox
{
    [Route("api/cpanel/[controller]")]
    [ApiController]
    public class FestivalController : ControllerBase
    {
        private readonly IFestivalRepository _festivalrepo;
        private readonly IPCRepository _productcatrepo;
        private readonly IMapper _mapper;
        private readonly ILogger<FestivalController> _logger;
        public FestivalController(IFestivalRepository festivalRepository, ILogger<FestivalController> logger, IMapper mapper, IPCRepository pCRepository)
        {
            _festivalrepo = festivalRepository;
            _logger = logger;
            _mapper = mapper;
            _productcatrepo = pCRepository;
        }
        [Route("AddFestival")]
        [HttpPost]
        public async Task<IActionResult> AddFestival(FestivalCreationModel Data)
        {
            try
            {
                return new JsonResult(await _festivalrepo.InsertCollation(Data));
            }
            catch(Exception ex)
            {
                return new JsonResult(ResponseModel.ServerInternalError(data: ex));
            }
        }
        [Route("DeleteFestival/{fest}")]
        [HttpPost]
        public async Task<IActionResult> DeleteFestival(int fest)
        {
            try
            {
                return new JsonResult(await _festivalrepo.Delete(fest));
            }
            catch (Exception ex)
            {
                return new JsonResult(ResponseModel.ServerInternalError(data: ex));
            }
        }
        [Route("ActiveFestival/{fest}")]
        [HttpPost]
        public async Task<IActionResult> ActiveFestival(int fest)
        {
            try
            {
                return new JsonResult(await _festivalrepo.LogicalAvailable(fest,true));
            }
            catch (Exception ex)
            {
                return new JsonResult(ResponseModel.ServerInternalError(data: ex));
            }
        }
        [Route("DisableFestival/{fest}")]
        [HttpPost]
        public async Task<IActionResult> DisableFestival(int fest)
        {
            try
            {
                return new JsonResult(await _festivalrepo.LogicalAvailable(fest,false));
            }
            catch (Exception ex)
            {
                return new JsonResult(ResponseModel.ServerInternalError(data: ex));
            }
        }
        [Route("GetAllFestival")]
        [HttpPost]
        public async Task<IActionResult> GetAllFestivals(int fest)
        {
            try
            {
                var AllFests = ((List<FestivalOfferModel>)(await _festivalrepo.GetAll()).Data).Where(x => x.F_IsDeleted == false).ToList();
                List<object> Result = new List<object>();

                foreach(var item in AllFests)
                {
                    if(item.F_Type == 1) ///is for all products
                    {
                        Result.Add(new { 
                            Id = item.F_Id,
                            Type = item.F_Type,
                            Title = item.F_Title,
                            Discount = item.F_Discount,
                            TypeStr = "همه محصولات",
                            Status = item.F_Status,
                        });
                    }
                    else
                    {
                        var cat =await _productcatrepo.GetById(item.F_EndLevelCategoryId);
                        if(cat != null)
                        {
                            Result.Add(new
                            {
                                Id = item.F_Id,
                                Type = item.F_Type,
                                Title = item.F_Title,
                                Discount = item.F_Discount,
                                TypeStr = cat.PC_Title,
                                Status = item.F_Status,
                                CategoryId = cat.PC_Id,
                                CategoryTitle = cat.PC_Title
                            });
                        }
                    }
                }

                return new JsonResult(ResponseModel.Success(data:Result));
            }
            catch (Exception ex)
            {
                return new JsonResult(ResponseModel.ServerInternalError(data: ex));
            }
        }

    }
}
