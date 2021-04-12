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
    [Route("api/cpanel/sitesetting/[controller]")]
    [ApiController]
    public class ProductLabelController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly IPublicMethodRepsoitory<ProductLabelModel> _repository;
        public ProductLabelController(IMapper mapper, IPublicMethodRepsoitory<ProductLabelModel> repository)
        {
            this._repository = repository;
            this._mapper = mapper;
        }
        [Route("AddLabel")]
        [HttpPost]
        public async Task<ActionResult> AddLabel(OutModels.Models.ProductLabel label)
        {
            try
            {
                ProductLabelModel lbl = (ProductLabelModel)_mapper.Map<OutModels.Models.ProductLabel, ProductLabelModel>(label);
                return new JsonResult(await this._repository.Insert(lbl));
            }
            catch (Exception ex)
            {
                return null;
            }
        }
        [Route("UpdateLabel")]
        [HttpPost]
        public async Task<ActionResult> UpdateLabel(OutModels.Models.ProductLabel label)
        {
            try
            {
                ProductLabelModel lbl = (ProductLabelModel)_mapper.Map<OutModels.Models.ProductLabel, ProductLabelModel>(label);
                return new JsonResult(await this._repository.Update(lbl));
            }
            catch (Exception ex)
            {
                return null;
            }
        }
        [Route("ActiveLabel")]
        [HttpPost]
        public async Task<ActionResult> ActiveLabel(OutModels.Models.ProductLabel label)
        {
            try
            {
                int id = label.L_Id;
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
        [Route("DisableLabel")]
        [HttpPost]
        public async Task<ActionResult> DisableLabel(OutModels.Models.ProductLabel label)
        {
            try
            {
                int id = label.L_Id;
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
        [Route("DeleteLabel")]
        [HttpPost]
        public async Task<ActionResult> DeleteLabel(OutModels.Models.ProductLabel label)
        {
            try
            {
                int id = label.L_Id;
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
        [Route("LoadLabeles/{lang}")]
        [HttpPost]
        public async Task<ActionResult> GetAllLabeles(string lang)
        {
            try
            {
                List<ProductLabelModel> data = ((List<ProductLabelModel>)(await this._repository.GetAll()).Data).Where(x=>x.Lang == lang && x.L_IsDeleted == false).OrderBy(x=>x.Created_At).ToList();
                List<OutModels.Models.ProductLabel> result = _mapper.Map<List<ProductLabelModel>, List<OutModels.Models.ProductLabel>>(data);
                return new JsonResult(ResponseModel.Success("LABELS_LIST_RETURNED", result));
            }
            catch (Exception ex)
            {
                return null;
            }
        }
        [Route("LoadLabelComboItems/{lang}")]
        [HttpPost]
        public async Task<ActionResult> GetAllLabelsComboItems(string lang)
        {
            try
            {
                List<ProductLabelModel> data = ((List<ProductLabelModel>)(await this._repository.GetAll()).Data).Where(x=>x.Lang == lang && x.L_Status && !x.L_IsDeleted).OrderBy(x=>x.Created_At).ToList();
                List<ComboItems.Label> result = _mapper.Map<List<ProductLabelModel>, List<ComboItems.Label>>(data);
                return new JsonResult(ResponseModel.Success("LABLES_LIST_RETURNED", result));
            }
            catch (Exception ex)
            {
                return null;
            }
        }

    }
}