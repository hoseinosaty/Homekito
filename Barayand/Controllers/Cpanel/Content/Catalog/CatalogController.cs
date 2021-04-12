using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Barayand.Common.Services;
using Barayand.DAL.Interfaces;
using Barayand.Models.Entity;
using Barayand.OutModels.Models;
using Barayand.OutModels.Response;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Barayand.Controllers.Cpanel.Content.Catalog
{
    [Route("api/cpanel/content/[controller]")]
    [ApiController]
    public class CatalogController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly IPublicMethodRepsoitory<CatalogModel> _repository;
        public CatalogController(IMapper mapper, IPublicMethodRepsoitory<CatalogModel> repsoitory)
        {
            this._mapper = mapper;
            this._repository = repsoitory;
        }
        [Route("AddCatalog")]
        [HttpPost]
        public async Task<ActionResult> AddCatalog(OutModels.Models.Catalog ig)
        {
            try
            {
                CatalogModel b = (CatalogModel)_mapper.Map<OutModels.Models.Catalog, CatalogModel>(ig);
                return new JsonResult(await this._repository.Insert(b));
            }
            catch (Exception ex)
            {
                return null;
            }
        }
        [Route("LoadCatalogs/{lang?}")]
        [HttpPost]
        public async Task<ActionResult> LoadCatalogs(string lang = "fa")
        {
            try
            {
                List<CatalogModel> data = (List<CatalogModel>)(await this._repository.GetAll()).Data;
                List<OutModels.Models.Catalog> result = _mapper.Map<List<CatalogModel>, List<OutModels.Models.Catalog>>(data.ToList());
                return new JsonResult(ResponseModel.Success("CHILDS_LIST_RETURNED", result));
            }
            catch (Exception ex)
            {
                return null;
            }
        }
        [Route("UpdateCatalog")]
        [HttpPost]
        public async Task<ActionResult> UpdateCatalog(OutModels.Models.Catalog ig)
        {
            try
            {
                CatalogModel pcm = (CatalogModel)_mapper.Map<OutModels.Models.Catalog, CatalogModel>(ig);
                return new JsonResult(await this._repository.Update(pcm));
            }
            catch (Exception ex)
            {
                return null;
            }
        }
        [Route("ActiveCatalog")]
        [HttpPost]
        public async Task<ActionResult> ActiveChild(OutModels.Models.Catalog ig)
        {
            try
            {
                int id = ig.C_Id;
                if (id == 0)
                {
                    return new JsonResult(ResponseModel.Error("گالری مورد نظر یافت نشد"));
                }
                return new JsonResult(await this._repository.LogicalAvailable(id, true));
            }
            catch (Exception ex)
            {
                return null;
            }
        }
        [Route("DisableCatalog")]
        [HttpPost]
        public async Task<ActionResult> DisableCatalog(OutModels.Models.Catalog ig)
        {
            try
            {
                int id = ig.C_Id;
                if (id == 0)
                {
                    return new JsonResult(ResponseModel.Error("گالری مورد نظر یافت نشد"));
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
