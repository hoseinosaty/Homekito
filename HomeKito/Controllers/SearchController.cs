using Barayand.DAL.Interfaces;
using Barayand.Models.Entity;
using Microsoft.AspNetCore.Mvc;
using HomeKito.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HomeKito.Controllers
{
    public class SearchController : Controller
    {
        private readonly IPublicMethodRepsoitory<ProductModel> _productrepo;
        private readonly IPublicMethodRepsoitory<BrandModel> _brandrepo;
        private readonly IPCRepository _categories;
        public SearchController(IPublicMethodRepsoitory<ProductModel> productrepo, IPublicMethodRepsoitory<BrandModel> brandrepo, IPCRepository categories)
        {
            _productrepo = productrepo;
            _brandrepo = brandrepo;
            _categories = categories;
        }
        [Route("GetSearch/{id}")]
        public async Task<IActionResult> GetSearch(string id)
        {
            string[] QueryStrings = id.Split(" ");
            var AllProduct = ((List<ProductModel>)(await _productrepo.GetAll()).Data).Where(x => x.P_Status && QueryStrings.Any(y => x.P_Title.Contains(y,StringComparison.InvariantCultureIgnoreCase) || x.P_Description.Contains(y,StringComparison.InvariantCultureIgnoreCase) || (x.P_BrandTitle != null && x.P_BrandTitle.Contains(y, StringComparison.InvariantCultureIgnoreCase)))).Take(8).ToList();

            ViewBag.search = id;
            if (AllProduct.Count() < 1)
            {
                return Content("موردی یافت نشد");
            }
            return View(AllProduct);
        }
        [Route("Search/{Catid?}/{cattitle?}/{q}")]
        [Route("Search/{q}")]
        public async Task<IActionResult> Result(
            string q,
            string TitleSerch,
            int? order = null,
            bool isAvilable = false,
            bool fast = false,
            bool isajax = false,
            int[] Brand = null,
            int Catid = 0,
            int page = 1,
            decimal minPrice = 0,
            decimal maxprice = 0

         )
        {
            var AllProduct = ((List<ProductModel>)(await _productrepo.GetAll()).Data).ToList();
            string[] QueryStrings = q.Split(" ");
            if(QueryStrings.Count() > 0)
            {
                 AllProduct = AllProduct.Where(x => x.P_Status && QueryStrings.Any(y => x.P_Title.Contains(y, StringComparison.InvariantCultureIgnoreCase) || x.P_Description.Contains(y, StringComparison.InvariantCultureIgnoreCase) || (x.P_BrandTitle != null && x.P_BrandTitle.Contains(y, StringComparison.InvariantCultureIgnoreCase)))).Take(8).ToList();
            }
           

            if (Catid > 0)
            {
                AllProduct = AllProduct.Where(x => x.P_MainCatId == Catid || x.P_EndLevelCatId == Catid).ToList();
            }
            ViewBag.Minprice = 0;
            ViewBag.MaxPrice = 0;
            ViewBag.id = q;
            ViewBag.catid = Catid;
            #region Category
            var Catlvl2 = AllProduct.GroupBy(x => x.P_EndLevelCatId)?.Select(x => x.FirstOrDefault().P_EndLevelCatId).ToList();
            var Catlvl1 = AllProduct.GroupBy(x => x.P_MainCatId)?.Select(x => x.FirstOrDefault().P_MainCatId).ToList();
            ViewBag.Catlvl2 = ((List<ProductCategoryModel>)(await _categories.GetAll()).Data).Where(x => Catlvl2.Contains(x.PC_Id)).ToList();
            ViewBag.Catlvl1 = ((List<ProductCategoryModel>)(await _categories.GetAll()).Data).Where(x => Catlvl1.Contains(x.PC_Id)).ToList();
            #endregion
            #region GetPrice
            if (AllProduct.Where(x => x.IsAvailable).Count() > 0)
            {
                ViewBag.Minprice = AllProduct.Where(x => x.DefaultProductCombine!=null).Min(x => x.DefaultProductCombine.PriceModel.Price);
                ViewBag.MaxPrice = AllProduct.Where(x => x.DefaultProductCombine!=null).Max(x => x.DefaultProductCombine.PriceModel.Price);
            }
            #endregion

            #region Brand
            var BrandGroups = AllProduct.GroupBy(x => x.P_BrandId)?.Select(x => x.FirstOrDefault().P_BrandId).ToList();
            ViewBag.Brands = ((List<BrandModel>)(await _brandrepo.GetAll()).Data).Where(x => BrandGroups.Contains(x.B_Id)).ToList();
            #endregion
            #region Filter
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
                        AllProduct = AllProduct.Where(x => x.DefaultProductCombine!=null && x.DefaultProductCombine.PriceModel != null).OrderBy(x => (x.DefaultProductCombine.PriceModel.Price)).ToList();
                        break;
                    case 6://expenc
                        AllProduct = AllProduct.Where(x => x.DefaultProductCombine != null && x.DefaultProductCombine.PriceModel != null).OrderByDescending(x => x.DefaultProductCombine.PriceModel.Price).ToList();

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
            if (!string.IsNullOrEmpty(TitleSerch))
            {
                AllProduct = AllProduct.Where(x => x.P_Title.Contains(TitleSerch, StringComparison.InvariantCultureIgnoreCase)).ToList();
            }
            if (Brand != null && Brand.Count() > 0)
            {
                AllProduct = AllProduct.Where(x => Brand.Contains(x.P_BrandId)).ToList();
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
                AllProduct = AllProduct.Where(x => x.DefaultProductCombine.PriceModel.Price >= minPrice && x.DefaultProductCombine.PriceModel.Price <= maxprice).ToList();

            }
            #endregion
            #region Paging
            Paging paging = new Paging();
            paging.TotalCount = AllProduct.Count();
            paging.PageSize = 30;
            paging.CurrentPage = page;
            paging.TotalPages = (int)Math.Ceiling(paging.TotalCount / (double)paging.PageSize);
            ViewBag.paging = paging;
            #endregion
            var items = AllProduct.Skip((paging.CurrentPage - 1) * paging.PageSize).Take(paging.PageSize).ToList();
            if (isajax)
            {
                return View("_ProductList", items);
            }
            return View(items);
        }
    }
}
