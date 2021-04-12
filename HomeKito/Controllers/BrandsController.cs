using Barayand.DAL.Interfaces;
using Barayand.Models.Entity;
using HomeKito.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HomeKito.Controllers
{

    public class BrandsController : Controller
    {
        private readonly IPublicMethodRepsoitory<BrandModel> _brandrepo;
        private readonly ILogger<BrandsController> _logger;
        private readonly IPublicMethodRepsoitory<ProductModel> _productrepo;
        private readonly IPCRepository _categories;
        public BrandsController(ILogger<BrandsController> logger, IPCRepository categories, IPublicMethodRepsoitory<BrandModel> brandrepo, IPublicMethodRepsoitory<ProductModel> productrepo)
        {
            _logger = logger;
            _brandrepo = brandrepo;
            _productrepo = productrepo;
            _categories = categories;
        }
        [Route("Brands")]
        public async Task<IActionResult> Index(bool isajax, int page = 1)
        {
            try
            {
                var brand = ((List<BrandModel>)(await _brandrepo.GetAll()).Data).Where(x => x.B_Status && !x.B_IsDeleted).OrderBy(x => x.B_SortField).ToList();
                if (brand != null)
                {
                    #region Paging
                    Paging paging = new Paging();
                    paging.TotalCount = brand.Count();
                    paging.PageSize = 16;
                    paging.CurrentPage = page;
                    paging.TotalPages = (int)Math.Ceiling(paging.TotalCount / (double)paging.PageSize);
                    ViewBag.paging = paging;
                    #endregion
                    var items = brand.Skip((paging.CurrentPage - 1) * paging.PageSize).Take(paging.PageSize).ToList();
                    if (isajax)
                    {
                        return View("_brandlist", items);
                    }
                    return View(items);
                }
                return RedirectToAction("Index", "Home");
            }
            catch (Exception ex)
            {
                _logger.LogError("Brands", ex);
                return RedirectToAction("Index", "Home");
            }
        }

        [Route("Brand/{id}/{title?}/{catid?}/{cattitle?}")]
        public async Task<IActionResult> Brand(
            int id,
            bool isAjax,
            string TitleSerch,
            bool isAvilable = false,
            bool fast = false,
            int? order = null,
            decimal minPrice = 0,
            decimal maxprice = 0,
            int page = 1,
            int? catid = null
            )
        {
            try
            {
                if (id != 0)
                {
                    int? catlvlcheck = null;

                    var AllProduct = ((List<ProductModel>)(await _productrepo.GetAll()).Data).Where(x => x.P_Status && x.P_BrandId == id && !x.P_IsDeleted).ToList();
                    ViewBag.brand = await _brandrepo.GetById(id);
                    ViewBag.catid = catid;
      
                    ViewBag.openattr = null;
                    if (catid != null)
                    {
                        var catbrand = await _categories.GetById(catid);

                        catlvlcheck = (catbrand.PC_ParentId == 0) ?  1 : 2;

                        if (catbrand.PC_ParentId == 0)
                        {
                            ViewBag.openattr = catid;
                        }
                        else
                        {
                            ViewBag.openattr = catbrand.PC_ParentId;
                        }
                        
                    }

                    #region Category
                    var Catlvl2 = AllProduct.GroupBy(x => x.P_EndLevelCatId)?.Select(x => x.FirstOrDefault().P_EndLevelCatId).ToList();
                    var Catlvl1 = AllProduct.GroupBy(x => x.P_MainCatId)?.Select(x => x.FirstOrDefault().P_MainCatId).ToList();
                    ViewBag.Catlvl2 = ((List<ProductCategoryModel>)(await _categories.GetAll()).Data).Where(x => Catlvl2.Contains(x.PC_Id)).ToList();
                    ViewBag.Catlvl1 = ((List<ProductCategoryModel>)(await _categories.GetAll()).Data).Where(x => Catlvl1.Contains(x.PC_Id)).ToList();
                    #endregion

                    #region GetPrice
                    if (AllProduct.Where(x => x.IsAvailable).Count() > 0)
                    {
                     
                        ViewBag.Minprice = AllProduct.Where(x => x.IsAvailable).Min(x => x.DefaultProductCombine.CalculatedPrice());
                        ViewBag.MaxPrice = AllProduct.Where(x => x.IsAvailable).Max(x => x.DefaultProductCombine.CalculatedPrice());
                    
                    }
                    #endregion

                    if (catlvlcheck != null)
                    {
                        if (catlvlcheck == 1)
                        {
                            AllProduct = AllProduct.Where(x => x.P_MainCatId == catid).ToList();
        
                        }
                        else
                        {
                            AllProduct = AllProduct.Where(x => x.P_EndLevelCatId == catid).ToList();
                 
                        }
                       

                    }

                    if (!string.IsNullOrEmpty(TitleSerch))
                    {
                        AllProduct = AllProduct.Where(x => x.P_Title.Contains(TitleSerch, StringComparison.InvariantCultureIgnoreCase)).ToList();
                    }
                    if (!string.IsNullOrEmpty(order.ToString()))
                    {
                        switch (order)
                        {
                            case 1: //See
                                AllProduct = AllProduct.OrderByDescending(x => x.VisitCount).ToList();
                                break;
                            case 2://sell
                                AllProduct = AllProduct.OrderByDescending(x => x.P_SaleCount).ToList();
                                break;
                            case 3://fav
                                AllProduct = AllProduct.OrderByDescending(x => x.ManualRate).ToList();

                                break;
                            case 4://new
                                AllProduct = AllProduct.OrderByDescending(x => x.VisitCount).ToList();
                                break;
                            case 5://cheap
                                AllProduct = AllProduct.Where(x => x.Warranties.Count > 0).OrderBy(x => (x.DefaultProductCombine.CalculatedPrice())).ToList();
                                break;
                            case 6://expenc
                                AllProduct = AllProduct.Where(x => x.Warranties.Count > 0).OrderByDescending(x => x.DefaultProductCombine.CalculatedPrice()).ToList();

                                break;
                            case 7://fast
                                AllProduct = AllProduct.Where(x => x.P_ImmediateSend).ToList();
                                break;
                            case 8://bestoffer
                                AllProduct = AllProduct.Where(x => x.P_BestOffer).ToList();
                                break;
                            default:
                                break;
                        }
                    }
                    if (fast)
                    {
                        AllProduct = AllProduct.Where(x => x.P_ImmediateSend).ToList();
                    }
                    if (isAvilable)
                    {
                        AllProduct = AllProduct.Where(x => x.IsAvailable).ToList();
                    }
                    if (minPrice > 0 && maxprice > 0)
                    {
                        AllProduct = AllProduct.Where(x => x.Warranties.Count > 0 && x.DefaultProductCombine.CalculatedPrice() >= minPrice && x.DefaultProductCombine.CalculatedPrice() <= maxprice).ToList();

                    }
                    if (catid != null)
                    {
                        AllProduct = AllProduct.Where(x => x.P_EndLevelCatId == catid || x.P_MainCatId == catid).ToList();
                    }
                    #region Paging
                    Paging paging = new Paging();
                    paging.TotalCount = AllProduct.Count();
                    paging.PageSize = 20;
                    paging.CurrentPage = page;
                    paging.TotalPages = (int)Math.Ceiling(paging.TotalCount / (double)paging.PageSize);
                    ViewBag.paging = paging;
                    #endregion
                    var items = AllProduct.Skip((paging.CurrentPage - 1) * paging.PageSize).Take(paging.PageSize).ToList();
                    if (isAjax)
                    {
                        return View("_ProductList", items);
                    }
                    return View(items);
                }
                return RedirectToAction("Index", "Home");
            }
            catch (Exception ex)
            {

                _logger.LogError("Brands", ex);
                return RedirectToAction("Index", "Home");
            }
        }
    }
}