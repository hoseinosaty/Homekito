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
    [Route("api/cpanel/product/[controller]")]
    [ApiController]
    public class BrandController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly IPublicMethodRepsoitory<BrandModel> _repository;
        public BrandController(IMapper mapper, IPublicMethodRepsoitory<BrandModel> repository)
        {
            this._repository = repository;
            this._mapper = mapper;
        }
        [Route("AddBrand")]
        [HttpPost]
        public async Task<ActionResult> AddBrand(OutModels.Models.Brand brand)
        {
            try
            {
                BrandModel b = (BrandModel)_mapper.Map<OutModels.Models.Brand, BrandModel>(brand);
                return new JsonResult(await this._repository.Insert(b));
            }
            catch (Exception ex)
            {
                return null;
            }
        }
        [Route("UpdateBrand")]
        [HttpPost]
        public async Task<ActionResult> UpdateBrand(OutModels.Models.Brand brand)
        {
            try
            {
                BrandModel wm = (BrandModel)_mapper.Map<OutModels.Models.Brand, BrandModel>(brand);
                return new JsonResult(await this._repository.Update(wm));
            }
            catch (Exception ex)
            {
                return null;
            }
        }
        [Route("ActiveBrand")]
        [HttpPost]
        public async Task<ActionResult> ActiveBrand(OutModels.Models.Brand brand)
        {
            try
            {
                int id = brand.B_Id;
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
        [Route("DisableBrand")]
        [HttpPost]
        public async Task<ActionResult> DisableBrand(OutModels.Models.Brand brand)
        {
            try
            {
                int id = brand.B_Id;
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
        [Route("DeleteBrand")]
        [HttpPost]
        public async Task<ActionResult> DeleteBrand(OutModels.Models.Brand brand)
        {
            try
            {
                int id = brand.B_Id;
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
        [Route("LoadBrand/{lang}")]
        [HttpPost]
        public async Task<ActionResult> GetAllBrands(string lang)
        {
            try
            {
                List<BrandModel> data = ((List<BrandModel>)(await this._repository.GetAll()).Data).Where(x=>x.Lang == lang && x.B_IsDeleted == false).OrderBy(x=>x.B_SortField).ToList();
                List<OutModels.Models.Brand> result = _mapper.Map<List<BrandModel>, List<OutModels.Models.Brand>>(data);
                return new JsonResult(ResponseModel.Success("BRANDS_LIST_RETURNED", result));
            }
            catch (Exception ex)
            {
                return null;
            }
        }
        [Route("LoadBrandComboItems/{lang}")]
        [HttpPost]
        public async Task<ActionResult> GetAllBrandsComboItems(string lang)
        {
            try
            {
                List<BrandModel> data = ((List<BrandModel>)(await this._repository.GetAll()).Data).Where(x=>x.Lang == lang && x.B_IsDeleted == false).OrderBy(x=>x.B_SortField).ToList();
                List<ComboItems.Brand> result = _mapper.Map<List<BrandModel>, List<ComboItems.Brand>>(data);
                return new JsonResult(ResponseModel.Success("BRANDS_LIST_RETURNED", result));
            }
            catch (Exception ex)
            {
                return null;
            }
        }
    }
}