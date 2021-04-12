using Barayand.DAL.Interfaces;
using Barayand.Models.Entity;
using Barayand.OutModels.Miscellaneous;
using Barayand.OutModels.Response;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Barayand.Services.Interfaces;
namespace HomeKito.Controllers
{
    public class CompareController : Controller
    {
        private readonly IPublicMethodRepsoitory<ProductModel> _productrepo;
        private readonly IPCRepository _categories;
        private readonly IViewRenderer _viewRenderer;
        private readonly IPublicMethodRepsoitory<BrandModel> _brandrepo;

        public CompareController(IPublicMethodRepsoitory<ProductModel> productrepo, IPCRepository categories, IPublicMethodRepsoitory<BrandModel> brandrepo, IViewRenderer viewRenderer)
        {
            _productrepo = productrepo;
            _categories = categories;
            _brandrepo = brandrepo;
            _viewRenderer = viewRenderer;
        }
        [Route("Compare")]
        public async Task<IActionResult> Index()
        {
            try
            {
                ViewBag.category = null;
                List<ProductModel> items = new List<ProductModel>();
                string cookie = "";
                HttpContext.Request.Cookies.TryGetValue("Compaire", out cookie);
                var ProdCatList = (List<ProductCategoryModel>)(await _categories.GetAll()).Data;
                ViewBag.ProductCategory = ProdCatList.Where(x => x.PC_ParentId == 0 && x.PC_Status).ToList();

                if (cookie != null)
                {
                    var decryptCookie = Barayand.Common.Services.CryptoJsService.DecryptStringAES(cookie);
                    CompareCookieModel ccm = JsonConvert.DeserializeObject<CompareCookieModel>(decryptCookie);


                    foreach (var item in ccm.Products)
                    {
                        var p = await _productrepo.GetById(item);
                        items.Add(p);
                    }
                }
                if (items.Count() > 0)
                {
                    var getAllProductByCatId = ((List<ProductModel>)(await _productrepo.GetAll()).Data).Where(x=>x.P_EndLevelCatId == items.FirstOrDefault().P_EndLevelCatId).ToList();
                    foreach(var item in items)
                    {
                        getAllProductByCatId.Remove(item);
                    }
                    ViewBag.getAllProductByCatId = getAllProductByCatId;
                    #region Brand
                    var BrandGroups = getAllProductByCatId.GroupBy(x => x.P_BrandId)?.Select(x => x.FirstOrDefault().P_BrandId).ToList();
                    ViewBag.Brands = ((List<BrandModel>)(await _brandrepo.GetAll()).Data).Where(x => BrandGroups.Contains(x.B_Id)).ToList();
                    #endregion
                }
                else
                {
                    ViewBag.Brands = ((List<BrandModel>)(await _brandrepo.GetAll()).Data).Where(x =>x.B_Status).ToList();
                }
                return View(items);
            }
            catch (Exception ex)
            {
                return new JsonResult(ResponseModel.ServerInternalError(data: ex));
            }



        }
        [Route("AddToCompare/{id}")]
        public async Task<IActionResult> AddToCompare(int id)
        {
            try
            {
                string cookie = "";
                var prod = await _productrepo.GetById(id);
                if (prod == null)
                {
                    return new JsonResult(ResponseModel.Error("محصول یافت نشد"));
                }
                if (HttpContext.Request.Cookies.TryGetValue("Compaire", out cookie))
                {
                    if (cookie != null)
                    {
                        var decryptCookie = Barayand.Common.Services.CryptoJsService.DecryptStringAES(cookie);
                        CompareCookieModel ccm = JsonConvert.DeserializeObject<CompareCookieModel>(decryptCookie);
                        if (ccm.Products.Count() > 0)
                        {
                            if (ccm.Products.Count(x => x == prod.P_Id) > 0)
                            {
                                return new JsonResult(ResponseModel.Error("محصول  مورد نظر قبلا در لیست مقایسه ثبت شده است"));
                            }
                            var p = await _productrepo.GetById(ccm.Products.FirstOrDefault());
                            if (p != null)
                            {
                                if (p.P_EndLevelCatId != prod.P_EndLevelCatId)
                                {
                                    return new JsonResult(ResponseModel.Error("فقط محصولاتی میتوانند در لیست دسته بندی قرار گیرند که در یک گروه قرار داشته باشند"));
                                }
                            }
                            ccm.Products.Add(prod.P_Id);
                        }
                        else
                        {
                            ccm.Products.Add(prod.P_Id);
                        }
                        var SerilizedCcm = JsonConvert.SerializeObject(ccm);
                        var encryptData = Barayand.Common.Services.CryptoJsService.EncryptStringToAES(SerilizedCcm);
                        Response.Cookies.Append("Compaire", encryptData);
                    }
                    return new JsonResult(ResponseModel.Success("محصول مورد نظر با موفقیت به لیست مقایسه اضافه گردید"));
                }
                else
                {
                    CompareCookieModel ccm = new CompareCookieModel();
                    ccm.Products.Add(prod.P_Id);
                    var SerilizedCcm = JsonConvert.SerializeObject(ccm);
                    var encryptData = Barayand.Common.Services.CryptoJsService.EncryptStringToAES(SerilizedCcm);
                    Response.Cookies.Append("Compaire", encryptData);
                    return new JsonResult(ResponseModel.Success("محصول مورد نظر با موفقیت به لیست مقایسه اضافه گردید"));
                }

            }
            catch (Exception ex)
            {
                return new JsonResult(ResponseModel.ServerInternalError(data: ex));
            }
        }
        [Route("GetCompareCount")]
        public async Task<IActionResult> GetCompareCount()
        {
            try
            {
                var result = 0;
                string cookie = "";
                if (HttpContext.Request.Cookies.TryGetValue("Compaire", out cookie))
                {
                    if (cookie != null)
                    {
                        var decryptCookie = Barayand.Common.Services.CryptoJsService.DecryptStringAES(cookie);
                        CompareCookieModel ccm = JsonConvert.DeserializeObject<CompareCookieModel>(decryptCookie);
                        result = ccm.Products.Count();
                    }

                }
                return new JsonResult(ResponseModel.Success(data: result));
            }
            catch(Exception ex)
            {
                return new JsonResult(ResponseModel.Success(data:0));
            }
        }

        [Route("DeleteCompare/{id}")]
        public async Task<IActionResult> DeleteCompare(int id)
        {
            try
            {
                string cookie = "";
                HttpContext.Request.Cookies.TryGetValue("Compaire", out cookie);
                var decryptCookie = Barayand.Common.Services.CryptoJsService.DecryptStringAES(cookie);
                CompareCookieModel ccm = JsonConvert.DeserializeObject<CompareCookieModel>(decryptCookie);
                ccm.Products.Remove(id);
                var SerilizedCcm = JsonConvert.SerializeObject(ccm);
                var encryptData = Barayand.Common.Services.CryptoJsService.EncryptStringToAES(SerilizedCcm);
                Response.Cookies.Append("Compaire", encryptData);
                return new JsonResult(ResponseModel.Success("محصول مورد نظر با موفقیت حذف شد"));
            }
            catch (Exception ex)
            {
                return new JsonResult(ResponseModel.ServerInternalError(data: ex));
            }
        }

        [Route("CompareSerch")]
        [HttpPost]
        public async Task<IActionResult> CompareSerch(string title,int cat2,int? brand)
        {
            try
            {
                if (cat2 > 0)
                {
                    var getAllProductByCatId = ((List<ProductModel>)(await _productrepo.GetAll()).Data).Where(x => x.P_EndLevelCatId ==cat2).ToList();

                    if (!string.IsNullOrEmpty(title))
                    {
                        getAllProductByCatId = getAllProductByCatId.Where(x => x.P_Title.Contains(title, StringComparison.InvariantCultureIgnoreCase)).ToList();
                    }
                    if (brand != null && brand > 0)
                    {
                        getAllProductByCatId = getAllProductByCatId.Where(x => x.P_BrandId==brand).ToList();
                    }

                    var model = await _viewRenderer.RenderAsync(this, "_productlist", getAllProductByCatId);



                    return new JsonResult(ResponseModel.Success(data:model));
                }
                return new JsonResult(ResponseModel.ServerInternalError(data: "error"));
            }


            catch (Exception ex)
            {
                return new JsonResult(ResponseModel.ServerInternalError(data: ex));
            }
        }
        }
    }
