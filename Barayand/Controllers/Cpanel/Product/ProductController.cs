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
using Newtonsoft.Json;

namespace Barayand.Controllers.Cpanel.Product
{
    [Route("api/cpanel/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly IPublicMethodRepsoitory<ProductModel> _repository;
        private readonly IPublicMethodRepsoitory<ProductCombineModel> _combinerepository;
        private readonly IPublicMethodRepsoitory<WarrantyModel> _warrantyrepo;
        private readonly IPublicMethodRepsoitory<ColorModel> _colorrepo;
        private readonly IProductManualRepository _manualrepo;
        private readonly IPRRepository _productrelationrepo;
        private readonly IPerfectProductRepository _productperfectrepo;
        private readonly ISetProductRepository _productsetrepo;
        private readonly IGiftProductRepository _productgiftrepo;
        private readonly IProductAttrAnswerRepository _productAttrAnswerrepo;
        public ProductController(IMapper mapper, IPublicMethodRepsoitory<ProductModel> repository, IPRRepository productrelationrepo, IPublicMethodRepsoitory<ProductCombineModel> combinerepo, IPublicMethodRepsoitory<WarrantyModel> warrantyrepo, IPublicMethodRepsoitory<ColorModel> colorrepo, IPerfectProductRepository perfectProduct, ISetProductRepository productsetrepo, IProductManualRepository manualrepo, IGiftProductRepository productgiftrepo, IProductAttrAnswerRepository productAttrAnswerrepo)
        {
            this._repository = repository;
            this._productrelationrepo = productrelationrepo;
            this._combinerepository = combinerepo;
            this._warrantyrepo = warrantyrepo;
            this._colorrepo = colorrepo;
            this._productperfectrepo = perfectProduct;
            this._productsetrepo = productsetrepo;
            this._manualrepo = manualrepo;
            this._productgiftrepo = productgiftrepo;
            this._mapper = mapper;
            _productAttrAnswerrepo = productAttrAnswerrepo;
        }
        [Route("SetProductAttr")]
        [HttpPost]
        public async Task<ActionResult> SetProductAttr()
        {
            try
            {
                var rawdata = Request.Form["data"];
                List<IncomeProdFieldSetModel> data = JsonConvert.DeserializeObject<List<IncomeProdFieldSetModel>>(rawdata);
                return new JsonResult(await this._productAttrAnswerrepo.SetProductAttributes(data));
            }
            catch (Exception ex)
            {
                return new JsonResult(ResponseModel.ServerInternalError(data: ex));
            }
        }
        [Route("GetProductAttr/{pid}")]
        [HttpPost]
        public async Task<ActionResult> GetProductAttr(int pid)
        {
            try
            {
                return new JsonResult(ResponseModel.Success(msg: "PRODUCT_ATTRIBUTE_ANSWER_RETURNED", data: await this._productAttrAnswerrepo.GetProductAttributes(pid)));
            }
            catch (Exception ex)
            {
                return new JsonResult(ResponseModel.ServerInternalError(data: ex));
            }
        }
        [Route("AddProduct")]
        [HttpPost]
        public async Task<ActionResult> AddProduct(OutModels.Models.Product product)
        {
            try
            {
                ProductModel b = (ProductModel)_mapper.Map<OutModels.Models.Product, ProductModel>(product);
                return new JsonResult(await this._repository.Insert(b));
            }
            catch (Exception ex)
            {
                return null;
            }
        }
        [Route("UpdateProduct")]
        [HttpPost]
        public async Task<ActionResult> UpdateProduct(OutModels.Models.Product product)
        {
            try
            {
                ProductModel b = (ProductModel)_mapper.Map<OutModels.Models.Product, ProductModel>(product);
                return new JsonResult(await this._repository.Update(b));
            }
            catch (Exception ex)
            {
                return new JsonResult(ResponseModel.ServerInternalError(data: ex));
            }
        }
        [Route("ActiveProduct")]
        [HttpPost]
        public async Task<ActionResult> ActiveProduct(OutModels.Models.Product product)
        {
            try
            {
                int id = product.P_Id;
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
        [Route("DisableProduct")]
        [HttpPost]
        public async Task<ActionResult> DisableProduct(OutModels.Models.Product product)
        {
            try
            {
                int id = product.P_Id;
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
        [Route("DeleteProduct")]
        [HttpPost]
        public async Task<ActionResult> DeleteProduct(OutModels.Models.Product product)
        {
            try
            {
                int id = product.P_Id;
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
        [Route("LoadProducts/{catid}/{lang}")]
        [HttpPost]
        public async Task<ActionResult> LoadProducts(string lang = null, int catid = 0)
        {
            try
            {
                List<ProductModel> data = (List<ProductModel>)(await this._repository.GetAll()).Data;
                List<OutModels.Models.Product> result = new List<OutModels.Models.Product>();
                if (catid != 0)
                {
                    result = _mapper.Map<List<ProductModel>, List<OutModels.Models.Product>>(data.Where(x => x.P_EndLevelCatId == catid).OrderByDescending(x => x.Created_At).ToList());
                }
                else
                {
                    result = _mapper.Map<List<ProductModel>, List<OutModels.Models.Product>>(data.OrderBy(x => x.Created_At).ToList());
                }
                return new JsonResult(ResponseModel.Success("PRODUCTS_LIST_RETURNED", result));
            }
            catch (Exception ex)
            {
                return new JsonResult(ResponseModel.ServerInternalError(data: ex));
            }
        }
        [Route("SearchProduct/{title?}/{code?}")]
        [HttpPost]
        public async Task<ActionResult> SearchProduct(string title = null,string code = null)
        {
            try
            {
                List<ProductModel> data = (List<ProductModel>)(await this._repository.GetAll()).Data;
                List<OutModels.Models.Product> result = new List<OutModels.Models.Product>();
                if(title != null)
                {
                    data = data.Where(x =>x.P_Title.Contains(title,StringComparison.InvariantCultureIgnoreCase) || x.P_Code.Contains(title, StringComparison.InvariantCultureIgnoreCase)).ToList();
                }
                if(code != null)
                {
                    data = data.Where(x => x.P_Code.Contains(code, StringComparison.InvariantCultureIgnoreCase)).ToList();
                }
                    result = _mapper.Map<List<ProductModel>, List<OutModels.Models.Product>>(data.OrderByDescending(x => x.Created_At).ToList());
                
                return new JsonResult(ResponseModel.Success("PRODUCTS_LIST_RETURNED", result));
            }
            catch (Exception ex)
            {
                return new JsonResult(ResponseModel.ServerInternalError(data: ex));
            }
        }
        [Route("LoadProductsForBoxs/{pid}/{page?}/{count?}/{title?}/{code?}")]
        [HttpPost]
        public async Task<ActionResult> LoadProductsForBoxs(int page = 1, int count = 5, int pid = 0, string title = null, string code = null)
        {
            try
            {
                List<ProductModel> data = (List<ProductModel>)(await this._repository.GetAll()).Data;
                List<ProductCombineModel> AllCombines = ((List<ProductCombineModel>)(await this._combinerepository.GetAll()).Data).Where(x => !x.X_IsDeleted).ToList();
                List<OutModels.Models.Product> result = new List<OutModels.Models.Product>();
                if (pid != 0)
                {
                    result = _mapper.Map<List<ProductModel>, List<OutModels.Models.Product>>(data.Where(x => x.P_Id != pid).ToList());
                }
                else
                {
                    result = _mapper.Map<List<ProductModel>, List<OutModels.Models.Product>>(data.ToList());
                }

                if (title != null)
                {
                    result = result.Where(x => x.P_Title.Contains(title, StringComparison.InvariantCultureIgnoreCase) || x.P_Code.Contains(title, StringComparison.InvariantCultureIgnoreCase)).ToList();
                }
                if (code != null)
                {
                    result = result.Where(x => x.P_Code.Contains(code, StringComparison.InvariantCultureIgnoreCase)).ToList();
                }

                List<object> Prods = new List<object>();
                foreach (var item in result)
                {
                    var ProdCombines = AllCombines.Where(x => x.X_ProductId == item.P_Id && x.X_Status && !x.X_IsDeleted).ToList();
                    if (ProdCombines.Count > 0)
                    {
                        int i = 1;
                        foreach (var combine in ProdCombines)
                        {
                            var w = await _warrantyrepo.GetById(combine.X_WarrantyId);
                            var c = await _colorrepo.GetById(combine.X_ColorId);
                            if (w != null && c != null)
                            {
                                Prods.Add(new
                                {
                                    counter = i,
                                    id = item.P_Id,
                                    code = item.P_Code,
                                    title = item.P_Title,
                                    warranty = w.W_Title,
                                    wid = w.W_Id,
                                    cid = c.C_Id,
                                    color = c.C_Title,
                                    price = combine.X_Price.ToString("#,#"),
                                    discount = combine.X_Discount,
                                    sdate = new string[] { },
                                    stime = "",
                                    etime = "",
                                    status = true,
                                    showinindex = true,
                                    discounttype = true
                                });
                                i++;
                            }
                        }
                    }
                }
                int totalPages = (int)Math.Ceiling((double)Prods.Count() / count);
                Prods = Prods.Skip(count * (page - 1)).Take(count).ToList();
                return new JsonResult(ResponseModel.Success("PRODUCTS_LIST_RETURNED", new { Products = Prods, TotalPages = totalPages, CurrentPage = page }));
            }
            catch (Exception ex)
            {
                return new JsonResult(ResponseModel.ServerInternalError(data: ex));
            }
        }
        [Route("LoadProductsDeletePid/{pid}/{page?}/{count?}/{title?}/{code?}")]
        [HttpPost]
        public async Task<ActionResult> LoadProducts(int page = 1, int count = 5, int pid = 0, string title = null, string code = null)
        {
            try
            {
                List<ProductModel> data = (List<ProductModel>)(await this._repository.GetAll()).Data;
                List<ProductCombineModel> AllCombines = ((List<ProductCombineModel>)(await this._combinerepository.GetAll()).Data).Where(x => !x.X_IsDeleted).ToList();
                List<OutModels.Models.Product> result = new List<OutModels.Models.Product>();
                if (pid != 0)
                {
                    result = _mapper.Map<List<ProductModel>, List<OutModels.Models.Product>>(data.Where(x => x.P_Id != pid).ToList());
                }
                else
                {
                    result = _mapper.Map<List<ProductModel>, List<OutModels.Models.Product>>(data.ToList());
                }

                if (title != null)
                {
                    result = result.Where(x => x.P_Title.Contains(title, StringComparison.InvariantCultureIgnoreCase)).ToList();
                }
                if (code != null)
                {
                    result = result.Where(x => x.P_Code.Contains(code, StringComparison.InvariantCultureIgnoreCase)).ToList();
                }

                List<object> Prods = new List<object>();
                foreach (var item in result)
                {
                    var ProdCombines = AllCombines.Where(x => x.X_ProductId == item.P_Id && x.X_Status && !x.X_IsDeleted).ToList();
                    if (ProdCombines.Count > 0)
                    {
                        int i = 1;
                        foreach (var combine in ProdCombines)
                        {
                            var w = await _warrantyrepo.GetById(combine.X_WarrantyId);
                            var c = await _colorrepo.GetById(combine.X_ColorId);
                            if (w != null && c != null)
                            {
                                Prods.Add(new
                                {
                                    counter = i,
                                    id = item.P_Id,
                                    code = item.P_Code,
                                    title = item.P_Title,
                                    warranty = w.W_Title,
                                    wid = w.W_Id,
                                    cid = c.C_Id,
                                    color = c.C_Title,
                                    price = combine.X_Price.ToString("#,#"),
                                    discount = combine.X_Discount.ToString("#,#") + (combine.X_DiscountType == 1 ? " %" : "")
                                });
                                i++;
                            }
                        }
                    }
                }
                int totalPages = (int)Math.Ceiling((double)Prods.Count() / count);
                Prods = Prods.Skip(count * (page - 1)).Take(count).ToList();
                return new JsonResult(ResponseModel.Success("PRODUCTS_LIST_RETURNED", new { Products = Prods, TotalPages = totalPages, CurrentPage = page }));
            }
            catch (Exception ex)
            {
                return new JsonResult(ResponseModel.ServerInternalError(data: ex));
            }
        }
        ////////
        [Route("AddProductRelation/{prodid}")]
        [HttpPost]
        public async Task<ActionResult> AddProductRelation(List<RelatedProductModel> data, int prodid)
        {
            try
            {
                return new JsonResult(await _productrelationrepo.UpdateRelation(data, prodid));
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
                return new JsonResult(await _productrelationrepo.GetAllRelation(data));
            }
            catch (Exception ex)
            {
                return new JsonResult(ResponseModel.ServerInternalError(data: ex));
            }
        }

        [Route("AddProductPefrect/{prodid}")]
        [HttpPost]
        public async Task<ActionResult> AddProductPefrect(List<PerfectProductModel> data, int prodid)
        {
            try
            {
                return new JsonResult(await _productperfectrepo.UpdateRelation(data, prodid));
            }
            catch (Exception ex)
            {
                return new JsonResult(ResponseModel.ServerInternalError(data: ex));
            }
        }
        [Route("GetProductPerfect")]
        [HttpPost]
        public async Task<ActionResult> GetProductPerfect(Miscellaneous data)
        {
            try
            {
                return new JsonResult(await _productperfectrepo.GetAllRelation(data));
            }
            catch (Exception ex)
            {
                return new JsonResult(ResponseModel.ServerInternalError(data: ex));
            }
        }
        [Route("AddProductGift/{prodid}")]
        [HttpPost]
        public async Task<ActionResult> AddProductGift(List<GiftProductModel> data, int prodid)
        {
            try
            {
                return new JsonResult(await _productgiftrepo.UpdateRelation(data, prodid));
            }
            catch (Exception ex)
            {
                return new JsonResult(ResponseModel.ServerInternalError(data: ex));
            }
        }
        [Route("GetProductGift")]
        [HttpPost]
        public async Task<ActionResult> GetProductGift(Miscellaneous data)
        {
            try
            {
                return new JsonResult(await _productgiftrepo.GetAllRelation(data));
            }
            catch (Exception ex)
            {
                return new JsonResult(ResponseModel.ServerInternalError(data: ex));
            }
        }

        [Route("AddProductSet/{prodid}")]
        [HttpPost]
        public async Task<ActionResult> AddProductSet(List<SetProductsModel> data, int prodid)
        {
            try
            {
                return new JsonResult(await _productsetrepo.UpdateRelation(data, prodid));
            }
            catch (Exception ex)
            {
                return new JsonResult(ResponseModel.ServerInternalError(data: ex));
            }
        }
        [Route("GetProductSet")]
        [HttpPost]
        public async Task<ActionResult> GetProductSet(Miscellaneous data)
        {
            try
            {
                return new JsonResult(await _productsetrepo.GetAllRelation(data));
            }
            catch (Exception ex)
            {
                return new JsonResult(ResponseModel.ServerInternalError(data: ex));
            }
        }

        ////
        [Route("AddProductCombine")]
        [HttpPost]
        public async Task<ActionResult> AddProductCombine(OutModels.Models.ProductCombine product)
        {
            try
            {
                ProductCombineModel b = (ProductCombineModel)_mapper.Map<OutModels.Models.ProductCombine, ProductCombineModel>(product);
                return new JsonResult(await this._combinerepository.Insert(b));
            }
            catch (Exception ex)
            {
                return new JsonResult(ResponseModel.ServerInternalError(data: ex));
            }
        }
        [Route("UpdateProductCombine")]
        [HttpPost]
        public async Task<ActionResult> UpdateProductCombine(OutModels.Models.ProductCombine product)
        {
            try
            {
                ProductCombineModel b = (ProductCombineModel)_mapper.Map<OutModels.Models.ProductCombine, ProductCombineModel>(product);
                return new JsonResult(await this._combinerepository.Update(b));
            }
            catch (Exception ex)
            {
                return new JsonResult(ResponseModel.ServerInternalError(data: ex));
            }
        }
        [Route("ActiveProductCombine")]
        [HttpPost]
        public async Task<ActionResult> ActiveProductCombine(OutModels.Models.ProductCombine product)
        {
            try
            {
                int id = product.X_Id;
                if (id == 0)
                {
                    return new JsonResult(ResponseModel.Error("موردی یافت نشد"));
                }
                return new JsonResult(await this._combinerepository.LogicalAvailable(id, true));
            }
            catch (Exception ex)
            {
                return new JsonResult(ResponseModel.ServerInternalError(data: ex));
            }
        }
        [Route("DisableProductCombine")]
        [HttpPost]
        public async Task<ActionResult> DisableProductCombine(OutModels.Models.ProductCombine product)
        {
            try
            {
                int id = product.X_Id;
                if (id == 0)
                {
                    return new JsonResult(ResponseModel.Error("موردی یافت نشد"));
                }
                return new JsonResult(await this._combinerepository.LogicalAvailable(id, false));
            }
            catch (Exception ex)
            {
                return new JsonResult(ResponseModel.ServerInternalError(data: ex));
            }
        }
        [Route("DeleteProductCombine")]
        [HttpPost]
        public async Task<ActionResult> DeleteProductCombine(OutModels.Models.ProductCombine product)
        {
            try
            {
                int id = product.X_Id;
                if (id == 0)
                {
                    return new JsonResult(ResponseModel.Error("موردی یافت نشد"));
                }
                return new JsonResult(await this._combinerepository.LogicalDelete(id));
            }
            catch (Exception ex)
            {
                return new JsonResult(ResponseModel.ServerInternalError(data: ex));
            }
        }
        [Route("LoadProductCombine")]
        [HttpPost]
        public async Task<ActionResult> LoadProductCombine(OutModels.Models.ProductCombine product)
        {
            try
            {
                var AllCombines = ((List<ProductCombineModel>)(await _combinerepository.GetAll()).Data).Where(x => !x.X_IsDeleted && x.X_ProductId == product.X_ProductId).OrderByDescending(x => x.Created_At).ToList();
                foreach (var item in AllCombines)
                {
                    item.WarrantyModel = await _warrantyrepo.GetById(item.X_WarrantyId);
                    item.ColorDetail = await _colorrepo.GetById(item.X_ColorId);
                }
                return new JsonResult(ResponseModel.Success(data: AllCombines));
            }
            catch (Exception ex)
            {
                return new JsonResult(ResponseModel.ServerInternalError(data: ex));
            }
        }
        [Route("AddProductManual")]
        [HttpPost]
        public async Task<ActionResult> AddProductManual(OutModels.Models.ProductManual product)
        {
            try
            {
                ProductManualModel b = (ProductManualModel)_mapper.Map<OutModels.Models.ProductManual, ProductManualModel>(product);
                return new JsonResult(await this._manualrepo.Insert(b));
            }
            catch (Exception ex)
            {
                return new JsonResult(ResponseModel.ServerInternalError(data: ex));
            }
        }
        [Route("GetProductManual/{pid}")]
        [HttpPost]
        public async Task<ActionResult> GetProductManual(int pid)
        {
            try
            {
                return new JsonResult(ResponseModel.Success(data: await this._manualrepo.GetByProductId(pid)));
            }
            catch (Exception ex)
            {
                return new JsonResult(ResponseModel.ServerInternalError(data: ex));
            }
        }

    }
}