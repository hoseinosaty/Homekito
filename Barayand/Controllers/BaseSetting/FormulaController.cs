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
    public class FormulaController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly IFormulaRepository _repository;
        public FormulaController(IMapper mapper, IFormulaRepository repository)
        {
            this._repository = repository;
            this._mapper = mapper;
        }
        [Route("AddFormula")]
        [HttpPost]
        public async Task<ActionResult> AddFormula(OutModels.Models.Formula formula)
        {
            try
            {
                FormulaModel am = (FormulaModel)_mapper.Map<OutModels.Models.Formula, FormulaModel>(formula);
                return new JsonResult(await this._repository.Insert(am));
            }
            catch (Exception ex)
            {
                return null;
            }
        }
        [Route("ActiveFormula")]
        [HttpPost]
        public async Task<ActionResult> ActiveFormula(OutModels.Models.Formula attribute)
        {
            try
            {
                int id = attribute.F_Id;
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
        [Route("DisableFormula")]
        [HttpPost]
        public async Task<ActionResult> DisableFormula(OutModels.Models.Formula attribute)
        {
            try
            {
                int id = attribute.F_Id;
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
        [Route("DeleteFormula")]
        [HttpPost]
        public async Task<ActionResult> DeleteFormula(OutModels.Models.Formula attribute)
        {
            try
            {
                int id = attribute.F_Id;
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
        [Route("LoadFormula")]
        [HttpPost]
        public async Task<ActionResult> LoadFormula()
        {
            try
            {
                return new JsonResult(ResponseModel.Success(data:((List<FormulaModel>)(await this._repository.GetAll()).Data).Where(x => x.F_IsDeleted == false).ToList()));
            }
            catch (Exception ex)
            {
                return new JsonResult(ResponseModel.ServerInternalError(data:ex));
            }
        }
        [Route("LoadFormulaComboItems")]
        [HttpPost]
        public async Task<ActionResult> LoadFormulaComboItems()
        {
            try
            {
                List<FormulaModel> data = ((List<FormulaModel>)(await this._repository.GetAll()).Data).Where(x => x.F_Status == true && x.F_IsDeleted == false).OrderBy(x => x.Created_At).ToList();
                List<ComboItems.Formula> result = _mapper.Map<List<FormulaModel>, List<ComboItems.Formula>>(data);
                return new JsonResult(ResponseModel.Success("FORMULA_LIST_RETURNED", result));
            }
            catch (Exception ex)
            {
                return null;
            }
        }
    }
}
