using Barayand.DAL.Interfaces;
using Barayand.Models.Entity;
using Barayand.Models.RuntimeModels;
using Barayand.OutModels.Miscellaneous;
using Barayand.OutModels.Response;
using Barayand.Services.Interfaces;
using HomeKito.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Primitives;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HomeKito.Controllers
{
    public class ProductController : Controller
    {
        private readonly ILogger<ProductController> _logger;
        private readonly IPublicMethodRepsoitory<ProductModel> _productrepo;
        private readonly IPublicMethodRepsoitory<BrandModel> _brandrepo;
        private readonly IPCRepository _categories;
        private readonly IViewRenderer _viewRenderer;
        private readonly IPublicMethodRepsoitory<AttributeModel> _attrrepo;
        private readonly IPublicMethodRepsoitory<CatAttrRelationModel> _catattrrepo;
        private readonly IPublicMethodRepsoitory<AttrAnswerModel> _attransrepo;
        private readonly IPublicMethodRepsoitory<ManufacturContryModel> _Cuntry;
        private readonly IPublicMethodRepsoitory<ProductCombineModel> _Combine;
        private readonly IFavoriteRepository _favoritrepostory;
        private readonly IPromotionBoxProdRepository _PromotionBoxProdRepo;
        private readonly IPCalcRepository _PCalcRepository;
        private readonly IProductAttrAnswerRepository _prodansRepo;

        public ProductController(ILogger<ProductController> logger, IPublicMethodRepsoitory<ManufacturContryModel> cuntry, IPublicMethodRepsoitory<AttributeModel> attrrepo, IPublicMethodRepsoitory<CatAttrRelationModel> catattrrepo, IPublicMethodRepsoitory<AttrAnswerModel> attransrepo, IPublicMethodRepsoitory<ProductModel> productrepo, IPCRepository categories, IViewRenderer view, IPublicMethodRepsoitory<BrandModel> brandrepo, IPublicMethodRepsoitory<ProductCombineModel> combine, IFavoriteRepository favoritrepostory, IPromotionBoxProdRepository promotionBoxProdRepo, IPCalcRepository pCalcRepository, IProductAttrAnswerRepository prodansRepo)
        {
            _logger = logger;
            _productrepo = productrepo;
            _categories = categories;
            _viewRenderer = view;
            _brandrepo = brandrepo;
            _attrrepo = attrrepo;
            _catattrrepo = catattrrepo;
            _attransrepo = attransrepo;
            _Cuntry = cuntry;
            _Combine = combine;
            _favoritrepostory = favoritrepostory;
            _PromotionBoxProdRepo = promotionBoxProdRepo;
            _PCalcRepository = pCalcRepository;
            _prodansRepo = prodansRepo;
        }
        [Route("/Products/{cat1}/{title?}/{cat2?}/{title2?}")]
        public async Task<IActionResult> Index(
            string TitleSerch,
            int? order = null,
            bool isAvilable = false,
            bool fast = false,
            bool isajax = false,
            int[] Brand = null,
            int[] attribute = null,
            int cat1 = 0,
            int cat2 = 0,
            int page = 1,
            decimal minPrice = 0,
            decimal maxprice = 0
            )
        {
            try
            {
                var refUrl = Request.Headers["Referer"].ToString(); 
                var ProdCatList = (List<ProductCategoryModel>)(await _categories.GetAll()).Data;
                if(refUrl.Contains("offer") || refUrl.Contains("Offer"))
                {
                    ViewBag.FilterOffered = true;
                }
                else
                {
                    ViewBag.FilterOffered = false;
                }
                ViewBag.ProductCategory = ProdCatList.Where(x => x.PC_ParentId == cat1 && x.PC_Status && !x.PC_IsDeleted).ToList();
                var Catlvl1 = await _categories.GetById(cat1);
                if (Catlvl1 != null)
                {
                    ViewBag.CatTitle = Catlvl1.PC_Title;
                    ViewBag.description = Catlvl1.PC_Description;
                    ViewBag.CatSeo = Catlvl1.PC_Seo;
                    ViewBag.CatL1 = Catlvl1;
                }
                else
                {
                    ViewBag.CatL1 = null;
                }
                if (cat2 > 0)
                {
                    var Catlvl2 = await _categories.GetById(cat2);
                    if (Catlvl2 != null)
                    {
                        ViewBag.description = Catlvl2.PC_Description;
                        ViewBag.CatSeo = Catlvl2.PC_Seo;
                        ViewBag.CatL2 = Catlvl2;
                    }
                    else
                    {
                        ViewBag.CatL2 = null;
                    }
                }
                else
                {
                    ViewBag.CatL2 = null;
                }
                var seo = Barayand.Common.Services.UtilesService.ParseSeoData(Catlvl1.PC_Seo);
                ViewBag.CatId = cat1;
                ViewBag.CatId2 = cat2;
                
                ViewBag.pagetype = 1;
                ViewBag.CatUrl1 = seo.url.ToString();
                var AllProduct = ((List<ProductModel>)(await _productrepo.GetAll()).Data).Where(x => x.P_Status && x.P_MainCatId == cat1).OrderBy(x => x.P_Sort).OrderBy(x=> x.P_LabelIds.Count() > 0)/*.OrderBy(x=>x.P_Labels.FirstOrDefault(l=>l.L_Title.Equals("جدید")))*/.ToList();
                if (cat2 > 0)
                {
                    AllProduct = AllProduct.Where(x => x.P_EndLevelCatId == cat2).ToList();
                }
                ViewBag.Minprice = 0;
                ViewBag.MaxPrice = 10;
                #region GetPrice
                if (AllProduct.Where(x => x.IsAvailable).Count() > 0)
                {
                    ViewBag.Minprice = AllProduct.Where(x => x.DefaultProductCombine != null).Min(x => x.DefaultProductCombine.PriceModel.Price);
                    ViewBag.MaxPrice = AllProduct.Where(x => x.DefaultProductCombine != null).Max(x => x.DefaultProductCombine.PriceModel.Price);
                }
                #endregion
                #region Attrbute
                if (!isajax)
                {
                    List<AttributeAnswerList> Attributes = new List<AttributeAnswerList>();
                    List<CatAttrRelationModel> catAttrs = ((List<CatAttrRelationModel>)(await _catattrrepo.GetAll()).Data).Where(x => x.X_IsDeleted != true).ToList();
                    foreach (var catttr in catAttrs)
                    {
                        if (catttr.X_CatId == cat1 || catttr.X_CatId == cat2)
                        {
                            var attr = await _attrrepo.GetById(catttr.X_AttrId);
                            if (attr != null && attr.A_Type == 1 && attr.A_UseInSearch && attr.A_Status && attr.A_IsDeleted == false)
                            {
                                List<AttrAnswerModel> answers = ((List<AttrAnswerModel>)(await _attransrepo.GetAll()).Data).Where(x => x.X_CatAttrId == catttr.X_Id && !x.X_IsDeleted && x.X_Status).ToList();
                                Attributes.Add(new AttributeAnswerList()
                                {
                                    Attribute = attr,
                                    Answers = answers
                                });
                            }
                        }
                    }
                    ViewBag.Attributes = Attributes;
                }
                #endregion
                #region Brand
                var BrandGroups = AllProduct.GroupBy(x => x.P_BrandId)?.Select(x => x.FirstOrDefault().P_BrandId).ToList();
                ViewBag.Brands = ((List<BrandModel>)(await _brandrepo.GetAll()).Data).Where(x => BrandGroups.Contains(x.B_Id)).ToList();
                #endregion
                #region Filter
                if (cat2 != 0)
                {
                    AllProduct = AllProduct.Where(x => x.P_EndLevelCatId == cat2).ToList();
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
                            AllProduct = AllProduct.Where(x => x.DefaultProductCombine != null && x.DefaultProductCombine.PriceModel != null).OrderBy(x => (x.DefaultProductCombine.PriceModel.Price)).ToList();
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
                if (attribute != null && attribute.Count() > 0)
                {
                    List<int> AllFilteredProdsByAnsId = new List<int>();
                    foreach (int aid in attribute)
                    {
                        var r = await _prodansRepo.GetProductsByAnswer(aid);
                        AllFilteredProdsByAnsId.AddRange(r);
                    }
                    AllProduct = AllProduct.Where(x => AllFilteredProdsByAnsId.Contains(x.P_Id)).ToList();
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
                
                if((int)Math.Ceiling(paging.TotalCount / (double)paging.PageSize) < page)
                {
                    page = 1;
                }
                paging.CurrentPage = page;
                paging.TotalPages = (int)Math.Ceiling(paging.TotalCount / (double)paging.PageSize);
                ViewBag.paging = paging;
                #endregion
                if(ViewBag.Minprice == ViewBag.MaxPrice)
                {
                    ViewBag.MaxPrice = ViewBag.MaxPrice + 100; 
                }
                var items = AllProduct.Skip((paging.CurrentPage - 1) * paging.PageSize).Take(paging.PageSize).ToList();
                if (isajax)
                {
                    return View("_ProductList", items);
                }
                return View(items);
            }
            catch (Exception ex)
            {
                _logger.LogError("", ex);
                return Content(ex.Message);
            }
        }
        [Route("/Product/{id}/{title?}/{title2?}")]
        public async Task<IActionResult> Detail(int id, int page = 1)
        {
            if (id != 0)
            {
                ViewBag.LikedIt = false;

                var product = await _productrepo.GetById(id);
                int auth = Barayand.Common.Services.TokenService.AuthorizeUser(Request);
                if (auth > 0)
                {
                    ViewBag.LikedIt = await _favoritrepostory.ChekExistsInList(product.P_Id, auth, 1);
                }
                ViewBag.loggedin = auth > 0;

                ViewBag.brand = await _brandrepo.GetById(product.P_BrandId);
                #region Paging
                Paging paging = new Paging();
                paging.TotalCount = product.Comments.Count();
                paging.PageSize = 30;
                paging.CurrentPage = page;
                paging.TotalPages = (int)Math.Ceiling(paging.TotalCount / (double)paging.PageSize);
                ViewBag.paging = paging;
                #endregion
                var items = product.Comments.Skip((paging.CurrentPage - 1) * paging.PageSize).Take(paging.PageSize).ToList();
                product.Comments = items;
                return View(product);
            }
            return RedirectToAction("Index", "Home");
        }
        [HttpPost]
        [Route("GetProductComments")]
        public async Task<IActionResult> GetComments(int id, int page = 1)
        {
            var product = await _productrepo.GetById(id);
            #region Paging
            Paging paging = new Paging();
            paging.TotalCount = product.Comments.Count();
            paging.PageSize = 10;
            paging.CurrentPage = page;
            paging.TotalPages = (int)Math.Ceiling(paging.TotalCount / (double)paging.PageSize);
            ViewBag.paging = paging;
            var items = product.Comments.Skip((paging.CurrentPage - 1) * paging.PageSize).Take(paging.PageSize).ToList();
            return View("_CommentList", items);
            #endregion
        }

        [Route("/Products/Country/{id}/{title?}/{catid?}/{cattitle?}")]
        public async Task<IActionResult> Country(
            int id,
            string TitleSerch,
            bool isAjax,
            int? order = null,
            decimal minPrice = 0,
            bool isAvilable = false,
            bool fast = false,
            decimal maxprice = 0,
            int page = 1,
            int? catid = null
            )
        {
            try
            {
                int? catlvlcheck = null;
                var AllProduct = ((List<ProductModel>)(await _productrepo.GetAll()).Data).Where(x => x.P_Status && x.P_MCuntryId == id).ToList();

                ViewBag.country = await _Cuntry.GetById(id);
                ViewBag.catid = catid;
                ViewBag.pagetype = 3;
                ViewBag.Minprice = 0;
                ViewBag.MaxPrice = 0;
                ViewBag.openattr = null;
                if (catid != null)
                {
                    var catbrand = await _categories.GetById(catid);

                    catlvlcheck = (catbrand.PC_ParentId == 0) ? 1 : 2;

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
                    ViewBag.Minprice = AllProduct.Where(x => x.DefaultProductCombine != null).Min(x => x.DefaultProductCombine.PriceModel.Price);
                    ViewBag.MaxPrice = AllProduct.Where(x => x.DefaultProductCombine != null).Max(x => x.DefaultProductCombine.PriceModel.Price);
                }
                #endregion
                #region Filter
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
                            AllProduct = AllProduct.Where(x =>x.DefaultProductCombine != null && x.DefaultProductCombine.PriceModel != null).OrderBy(x => (x.DefaultProductCombine.PriceModel.Price)).ToList();
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
                    AllProduct = AllProduct.Where(x =>  x.DefaultProductCombine.PriceModel.Price >= minPrice && x.DefaultProductCombine.PriceModel.Price <= maxprice).ToList();

                }
                if (catid != null)
                {
                    AllProduct = AllProduct.Where(x => x.P_EndLevelCatId == catid || x.P_MainCatId == catid).ToList();
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
                if (isAjax)
                {
                    return View("_ProductList", items);
                }
                return View(items);
            }
            catch (Exception ex)
            {

                _logger.LogError("", ex);
                return RedirectToAction("Index", "Home");
            }
        }

        [Route("/Products/GetPriceAjax")]
        public async Task<JsonResult> GetPriceAjax(int id, int garanty, int color)
        {
            try
            {

                var combine = ((List<ProductCombineModel>)(await _Combine.GetAll()).Data).FirstOrDefault(x => x.X_ColorId == color && x.X_WarrantyId == garanty && x.X_ProductId == id && x.X_IsDeleted != true && x.X_Status);
                var product = await _productrepo.GetById(combine.X_ProductId);
                ProductCombinePriceModel priceModel = await _PCalcRepository.CalculateProductCombinePrice(combine.X_Id,product.P_EndLevelCatId);
                return new JsonResult(ResponseModel.Success(data: new
                {
                    id= combine.X_Id,
                    priceModel = priceModel
                }));

            }
            catch (Exception ex)
            {

                return new JsonResult(ResponseModel.ServerInternalError(data: ex));
            }
        }

        [Route("/Products/More/{id}/{type}/{title?}")]
        public async Task<IActionResult> More(
            int id,
            string TitleSerch,
            int type,//1=mortabet/2=set/3=hamrah
            bool isAjax,
            bool fast = false,
            int[] Brand = null,
            int page = 1,
            decimal minPrice = 0,
            decimal maxprice = 0,
            int? order = null,
            int? catid = null,
            bool isAvilable = false
            )
        {
            List<ProductModel> items = new List<ProductModel>();
            var product = await _productrepo.GetById(id);
            ViewBag.type = type;
            ViewBag.id = id;
            ViewBag.pagetype = 2;
            ViewBag.Minprice = 0;
            ViewBag.MaxPrice = 0;
            if (type == 1)
            {
                items = product.RelatedProducts;
            }
            else if (type == 2)
            {
                items = product.SetProducts;
            }
            else
            {
                items = product.CompletelyProducts;
            }
            #region GetPrice
            if (items.Where(x => x.IsAvailable).Count() > 0)
            {
                ViewBag.Minprice = items.Where(x => x.IsAvailable).Min(x => x.DefaultProductCombine.CalculatedPrice());
                ViewBag.MaxPrice = items.Where(x => x.IsAvailable).Max(x => x.DefaultProductCombine.CalculatedPrice());
            }
            #endregion
            #region Brand
            var BrandGroups = items.GroupBy(x => x.P_BrandId)?.Select(x => x.FirstOrDefault().P_BrandId).ToList();
            ViewBag.Brands = ((List<BrandModel>)(await _brandrepo.GetAll()).Data).Where(x => BrandGroups.Contains(x.B_Id)).ToList();
            #endregion
            #region Filter
            if (!string.IsNullOrEmpty(TitleSerch))
            {
                items = items.Where(x => x.P_Title.Contains(TitleSerch, StringComparison.InvariantCultureIgnoreCase)).ToList();
            }
            if (!string.IsNullOrEmpty(order.ToString()))
            {
                switch (order)
                {
                    case 1: //See
                        items = items.OrderByDescending(x => x.VisitCount).ToList();
                        break;
                    case 2://sell
                        items = items.OrderByDescending(x => x.P_SaleCount).ToList();
                        break;
                    case 3://fav
                        items = items.OrderByDescending(x => x.ManualRate).ToList();

                        break;
                    case 4://new
                        items = items.OrderByDescending(x => x.VisitCount).ToList();
                        break;
                    case 5://cheap
                        items = items.Where(x => x.DefaultProductCombine != null && x.DefaultProductCombine.PriceModel.Price != null).OrderBy(x => (x.DefaultProductCombine.PriceModel.Price)).ToList();
                        break;
                    case 6://expenc
                        items = items.Where(x => x.DefaultProductCombine != null && x.DefaultProductCombine.PriceModel.Price != null).OrderByDescending(x => x.DefaultProductCombine.PriceModel.Price).ToList();

                        break;
                    case 7://fast
                        items = items.Where(x => x.P_ImmediateSend).ToList();
                        break;
                    case 8://bestoffer
                        items = items.Where(x => x.P_BestOffer).ToList();
                        break;
                    default:
                        break;
                }
            }
            if (fast)
            {
                items = items.Where(x => x.P_ImmediateSend).ToList();
            }
            if (isAvilable)
            {
                items = items.Where(x => x.IsAvailable).ToList();
            }
            if (minPrice > 0 && maxprice > 0)
            {
                items = items.Where(x =>  x.DefaultProductCombine.PriceModel.Price >= minPrice && x.DefaultProductCombine.PriceModel.Price <= maxprice).ToList();

            }
            if (Brand != null && Brand.Count() > 0)
            {
                items = items.Where(x => Brand.Contains(x.P_BrandId)).ToList();
            }
            //if (catid != null)
            //{
            //    AllProduct = AllProduct.Where(x => x.P_EndLevelCatId == catid || x.P_MainCatId == catid).ToList();
            //}
            #endregion
            #region Paging
            Paging paging = new Paging();
            paging.TotalCount = items.Count();
            paging.PageSize = 30;
            paging.CurrentPage = page;
            paging.TotalPages = (int)Math.Ceiling(paging.TotalCount / (double)paging.PageSize);
            ViewBag.paging = paging;
            #endregion
            var item = items.Skip((paging.CurrentPage - 1) * paging.PageSize).Take(paging.PageSize).ToList();
            if (isAjax)
            {
                return View("_ProductList", item);
            }
            return View(item);
        }

        [Route("/Productbox/{id}/{title?}")]
        public async Task<IActionResult> ProductPromotion(
            int id,
            bool isAjax,
            string TitleSerch,
            int[] Brand = null,
            decimal minPrice = 0,
            decimal maxprice = 0,
            int? order = null,
            int page=1,
            bool fast = false,
            bool isAvilable = false
            )
        {
            try
            {
                List<ProductModel> Products = new List<ProductModel>();
                ViewBag.id = id;
                ViewBag.pagetype = 4;
                ViewBag.Minprice = 0;
                ViewBag.MaxPrice = 0;
                var query = await _PromotionBoxProdRepo.GetAllRelation(new Miscellaneous() { Id = id });
                if (query.Status)
                {
                    List<PromotionBoxProductsModel> ProdIds = (List<PromotionBoxProductsModel>)query.Data;
                    foreach (var item in ProdIds)
                    {
                        var p = await _productrepo.GetById(item.X_ProdId);
                        if (p != null)
                        {
                            Products.Add(p);
                        }
                    }
                    #region Brand
                    var BrandGroups = Products.GroupBy(x => x.P_BrandId)?.Select(x => x.FirstOrDefault().P_BrandId).ToList();
                    ViewBag.Brands = ((List<BrandModel>)(await _brandrepo.GetAll()).Data).Where(x => BrandGroups.Contains(x.B_Id)).ToList();
                    #endregion
                    #region GetPrice
                    if (Products.Where(x => x.IsAvailable).Count() > 0)
                    {
                        ViewBag.Minprice = Products.Where(x => x.IsAvailable).Min(x => x.DefaultProductCombine.CalculatedPrice());
                        ViewBag.MaxPrice = Products.Where(x => x.IsAvailable).Max(x => x.DefaultProductCombine.CalculatedPrice());
                    }
                    #endregion
                    #region Filter
                    if (!string.IsNullOrEmpty(TitleSerch))
                    {
                        Products = Products.Where(x => x.P_Title.Contains(TitleSerch, StringComparison.InvariantCultureIgnoreCase)).ToList();
                    }
                    if (!string.IsNullOrEmpty(order.ToString()))
                    {
                        switch (order)
                        {
                            case 1: //See
                                Products = Products.OrderByDescending(x => x.VisitCount).ToList();
                                break;
                            case 2://sell
                                Products = Products.OrderByDescending(x => x.P_SaleCount).ToList();
                                break;
                            case 3://fav
                                Products = Products.OrderByDescending(x => x.ManualRate).ToList();

                                break;
                            case 4://new
                                Products = Products.OrderByDescending(x => x.VisitCount).ToList();
                                break;
                            case 5://cheap
                                Products = Products.Where(x => x.DefaultProductCombine.PriceModel.Price != null && x.DefaultProductCombine.PriceModel.Price != null).OrderBy(x => (x.DefaultProductCombine.PriceModel.Price)).ToList();
                                break;
                            case 6://expenc
                                Products = Products.Where(x =>  x.DefaultProductCombine.PriceModel.Price != null && x.DefaultProductCombine.PriceModel.Price != null).OrderByDescending(x => x.DefaultProductCombine.PriceModel.Price).ToList();

                                break;
                            case 7://fast
                                Products = Products.Where(x => x.P_ImmediateSend).ToList();
                                break;
                            case 8://bestoffer
                                Products = Products.Where(x => x.P_BestOffer).ToList();
                                break;
                            default:
                                break;
                        }
                    }
                    if (fast)
                    {
                        Products = Products.Where(x => x.P_ImmediateSend).ToList();
                    }
                    if (isAvilable)
                    {
                        Products = Products.Where(x => x.IsAvailable).ToList();
                    }
                    if (minPrice > 0 && maxprice > 0)
                    {
                        Products = Products.Where(x =>  x.DefaultProductCombine.PriceModel.Price >= minPrice && x.DefaultProductCombine.PriceModel.Price <= maxprice).ToList();

                    }
                    if (Brand != null && Brand.Count() > 0)
                    {
                        Products = Products.Where(x => Brand.Contains(x.P_BrandId)).ToList();
                    }
                    #endregion


                    #region Paging
                    Paging paging = new Paging();
                    paging.TotalCount = Products.Count();
                    paging.PageSize = 30;
                    paging.CurrentPage = page;
                    paging.TotalPages = (int)Math.Ceiling(paging.TotalCount / (double)paging.PageSize);
                    ViewBag.paging = paging;
                    #endregion
                    if (isAjax)
                    {
                        return View("_ProductList", Products);
                    }
                    return View(Products);        
                }
                return RedirectToAction("Index", "Home");
            }
            catch (Exception ex)
            {

                throw;
            }

          
        }
        [Route("/Offer/{catid?}")]
        public async Task<IActionResult> Offer(
            string type,
            int catid,
            bool isAjax,
            string TitleSerch,
            int[] Brand = null,
            decimal minPrice = 0,
            decimal maxprice = 0,
            int? order = null,
            int page = 1,
            bool fast = false,
            bool isAvilable = false
            )
        {
            try
            {
                ViewBag.pagetype = 5;
                var AllProduct = ((List<ProductModel>)(await _productrepo.GetAll()).Data).Where(x => x.P_Status).ToList();
                AllProduct = AllProduct.Where(x => x.DefaultProductCombine != null && x.DefaultProductCombine.PriceModel != null && x.DefaultProductCombine.PriceModel.Avl > 0 && x.DefaultProductCombine.PriceModel.HasDiscount).ToList();
                var AllCategories = ((List<ProductCategoryModel>)(await _categories.GetAll()).Data).ToList();
                if (catid != null && catid != 0)
                {
                    var catSelected = AllCategories.FirstOrDefault(x=>x.PC_Id == catid);
                    if(catSelected != null)
                    {
                        if(catSelected.PC_ParentId == null || catSelected.PC_ParentId == 0)
                        {
                            AllProduct = AllProduct.Where(x=>x.P_MainCatId == catid).ToList();
                        }
                        else
                        {
                            AllProduct = AllProduct.Where(x=>x.P_EndLevelCatId == catid).ToList();
                        }
                    }
                }
                #region GetPrice
                if (AllProduct.Where(x => x.IsAvailable).Count() > 0)
                {
                    ViewBag.Minprice = AllProduct.Where(x => x.IsAvailable).Min(x => x.DefaultProductCombine.CalculatedPrice());
                    ViewBag.MaxPrice = AllProduct.Where(x => x.IsAvailable).Max(x => x.DefaultProductCombine.CalculatedPrice());
                }
                #endregion
                #region Brand
                var BrandGroups = AllProduct.GroupBy(x => x.P_BrandId)?.Select(x => x.FirstOrDefault().P_BrandId).ToList();
                ViewBag.Brands = ((List<BrandModel>)(await _brandrepo.GetAll()).Data).Where(x => BrandGroups.Contains(x.B_Id)).ToList();
                #endregion
                var Catlvl2 = AllProduct.GroupBy(x => x.P_EndLevelCatId)?.Select(x => x.FirstOrDefault().P_EndLevelCatId).ToList();
                var Catlvl1 = AllProduct.GroupBy(x => x.P_MainCatId)?.Select(x => x.FirstOrDefault().P_MainCatId).ToList();
                ViewBag.Catlvl2 = AllCategories.Where(x => Catlvl2.Contains(x.PC_Id)).ToList();
                ViewBag.Catlvl1 = AllCategories.Where(x => Catlvl1.Contains(x.PC_Id)).ToList();
                #region Filter
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
                            AllProduct = AllProduct.Where(x =>  x.DefaultProductCombine != null && x.DefaultProductCombine.PriceModel.Price != 0).OrderBy(x => (x.DefaultProductCombine.PriceModel.Price)).ToList();
                            break;
                        case 6://expenc
                            AllProduct = AllProduct.Where(x =>  x.DefaultProductCombine != null && x.DefaultProductCombine.PriceModel.Price != 0).OrderByDescending(x => x.DefaultProductCombine.PriceModel.Price).ToList();

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
                    AllProduct = AllProduct.Where(x =>  x.DefaultProductCombine.PriceModel.Price >= minPrice && x.DefaultProductCombine.PriceModel.Price <= maxprice).ToList();

                }
                if (Brand != null && Brand.Count() > 0)
                {
                    AllProduct = AllProduct.Where(x => Brand.Contains(x.P_BrandId)).ToList();
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
                if (isAjax)
                {
                    return View("_ProductList", items);
                }
                return View(items);
            }
            catch (Exception)
            {
                throw;
            }

        }

    }
}