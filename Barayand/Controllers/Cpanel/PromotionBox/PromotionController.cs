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
    public class PromotionController : ControllerBase
    {
        private readonly IPromotionRepository _promotionrepo;
        private readonly IPromotionBoxProdRepository _promotionBoxProd;
        private readonly IMapper _mapper;
        private readonly ILogger<PromotionController> _logger;
        public PromotionController(IPromotionRepository promotionrepo, IMapper mapper, ILogger<PromotionController> logger, IPromotionBoxProdRepository promotionBoxProd)
        {
            _promotionrepo = promotionrepo;
            _mapper = mapper;
            _logger = logger;
            _promotionBoxProd = promotionBoxProd;
        }
        [Route("AddBox")]
        [HttpPost]
        public async Task<IActionResult> AddBox(Barayand.OutModels.Models.PromotionBox pb)
        {
            try
            {
                PromotionBoxModel pbm = (PromotionBoxModel)_mapper.Map<Barayand.OutModels.Models.PromotionBox, PromotionBoxModel>(pb);
                return new JsonResult(await _promotionrepo.Insert(pbm));
            }
            catch (Exception ex)
            {
                return new JsonResult(ResponseModel.ServerInternalError(data: ex));
            }
        }
        [Route("LoadBox/{type}")]
        [HttpPost]
        public async Task<IActionResult> LoadBox(int type = 1)
        {
            try
            {
                var AllPromotions = ((List<PromotionBoxModel>)(await _promotionrepo.GetAll()).Data).Where(x => x.B_Type == type).ToList();
                return new JsonResult(ResponseModel.Success(data: AllPromotions));
            }
            catch (Exception ex)
            {
                return new JsonResult(ResponseModel.ServerInternalError(data: ex));
            }
        }
        [Route("AddProductToBox/{secid?}")]
        [HttpPost]
        public async Task<IActionResult> AddProductToBox(List<Barayand.OutModels.Models.PromotionBoxProducts> pb, int secid = 0)
        {
            try
            {
                List<PromotionBoxProductsModel> pbm = (List<PromotionBoxProductsModel>)_mapper.Map<List<Barayand.OutModels.Models.PromotionBoxProducts>, List<PromotionBoxProductsModel>>(pb);
                return new JsonResult(await _promotionBoxProd.UpdateRelation(pbm, secid));
            }
            catch (Exception ex)
            {
                return new JsonResult(ResponseModel.ServerInternalError(data: ex));
            }
        }
        [Route("GetProductRelation")]
        [HttpPost]
        public async Task<ActionResult> GetProductRelation(Miscellaneous data)
        {
            try
            {
                return new JsonResult(await _promotionBoxProd.GetAllRelation(data));
            }
            catch (Exception ex)
            {
                return new JsonResult(ResponseModel.ServerInternalError(data: ex));
            }
        }
    }
}
