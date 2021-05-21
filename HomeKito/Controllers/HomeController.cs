using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using HomeKito.Models;
using Barayand.DAL.Interfaces;
using Barayand.Models.Entity;
using Microsoft.AspNetCore.Localization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Mvc.Localization;
using Microsoft.Extensions.Options;
using AutoMapper.Configuration;
using Barayand.Services.Interfaces;
using Newtonsoft.Json;
using Barayand.OutModels.Miscellaneous;
using Barayand.Common.Services;
using Wangkanai.Detection.Services;
using Wangkanai.Detection.Models;
using Barayand.OutModels.Response;
using System.Linq;
namespace HomeKito.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        private readonly IPublicMethodRepsoitory<DynamicPagesContent> _dynamicrepeo;
        private readonly IPublicMethodRepsoitory<ServiceModel> _Service;
        private readonly IDetectionService _detectionService;
        private readonly IPromotionRepository _promotionrepo;
        private readonly IPublicMethodRepsoitory<FaqCategoryModel> _faqcatrepo;
        private readonly IPublicMethodRepsoitory<FaqModel> _faqrepo;

        public HomeController(ILogger<HomeController> logger, IPublicMethodRepsoitory<DynamicPagesContent> dynamicrepeo, IDetectionService detectionService, IPromotionRepository promotionrepo, IPublicMethodRepsoitory<ServiceModel> service, IPublicMethodRepsoitory<FaqCategoryModel> faqcatrepo, IPublicMethodRepsoitory<FaqModel> faqrepo)
        {
            _logger = logger;
            _dynamicrepeo = dynamicrepeo;
            _detectionService = detectionService;
            _promotionrepo = promotionrepo;
            _Service = service;
            _faqcatrepo = faqcatrepo;
            _faqrepo = faqrepo;
        }


        public async Task<IActionResult> Index()
        {

            return View();
        }
        [Route("defult")]
        public async Task<IActionResult> defult()
        {
            ViewBag.staticBox = await _promotionrepo.GetByType(1);
            ViewBag.moveableBox = await _promotionrepo.GetByType(2);
            ViewBag.specialOffer = await _promotionrepo.GetByType(6);
            ViewBag.IsMobile = _detectionService.Device.Type == Device.Mobile;
            
            var IndexSeo = ((List<DynamicPagesContent>)(await _dynamicrepeo.GetAll()).Data).FirstOrDefault(x => x.PageName == "IndexSeo");
            ViewBag.IndexSeo = JsonConvert.DeserializeObject<SeoIndex>(IndexSeo.PageOtherSetting);
            return View();
        }
        [Route("contactus")]
        [Route("Pages/cu/{title?}")]
        public async Task<IActionResult> ContactUs(string title = "")
        {
            try
            {
                var page = ((List<DynamicPagesContent>)(await _dynamicrepeo.GetAll()).Data).FirstOrDefault(x=>x.PageName == "ContactUs");
                var IndexSeo = ((List<DynamicPagesContent>)(await _dynamicrepeo.GetAll()).Data).FirstOrDefault(x=>x.PageName == "IndexSeo");
                if(page == null)
                {
                    return RedirectToAction("Index","Home");
                }
                ViewBag.PageSeo = page.PageSeo;
                ViewBag.Data = JsonConvert.DeserializeObject<ContactUsData>(page.PageOtherSetting);
                ViewBag.IndexSeo = JsonConvert.DeserializeObject<SeoIndex>(IndexSeo.PageOtherSetting);
                var seo = UtilesService.ParseSeoData(page.PageSeo);
                ViewBag.PGNAME = seo.title;
                return View(page);
            }
            catch (Exception ex)
            {
                _logger.LogError("Error in main controller action index", ex);
                return null;
            }
        }
        [Route("aboutus")]
        [Route("Pages/au/{title?}")]
        public async Task<IActionResult> AboutUs(string title = "")
        {
            try
            {
                var page = ((List<DynamicPagesContent>)(await _dynamicrepeo.GetAll()).Data).FirstOrDefault(x => x.PageName == "AboutUs");
                if (page == null)
                {
                    return RedirectToAction("Index", "Home");
                }
                ViewBag.PageSeo = page.PageSeo;
                var seo = Barayand.Common.Services.UtilesService.ParseSeoData(page.PageSeo);
                ViewBag.PGNAME = seo.title;
                return View(page);
            }
            catch (Exception ex)
            {
                _logger.LogError("Error in main controller action index", ex);
                return null;
            }
        }
        [Route("Pages/{url?}")]
        public async Task<IActionResult> Pages(string url = "")
        {
            try
            {
                DynamicPagesContent page = null;
                foreach (var p in ((List<DynamicPagesContent>)(await _dynamicrepeo.GetAll()).Data))
                {
                    if(p.PageSeo != null )
                    {
                        var seo = Barayand.Common.Services.UtilesService.ParseSeoData(p.PageSeo);
                        if(seo != null && seo.url != null && seo.url == url)
                        {
                            page = p;
                        }
                    }
                }
                if (page == null)
                {
                    return RedirectToAction("Index", "Home");
                }
                ViewBag.PageSeo = page.PageSeo;
                var s = Barayand.Common.Services.UtilesService.ParseSeoData(page.PageSeo);
                ViewBag.PGNAME = s.title;
                return View(page);
            }
            catch (Exception ex)
            {
                _logger.LogError("Error in main controller action index", ex);
                return null;
            }
        }

        [Route("Service/{id}/{title?}")]
        public async Task<IActionResult> Service(int id,string title)
        {
            try
            {
                if (id != null)
                {
                    var query =await _Service.GetById(id);
                    return View(query);
                }
                return null;
            }
            catch (Exception ex)
            {
                return null;
            }
        }
        [Route("Profits/{id}/{title?}")]
        public async Task<IActionResult> Profits(int id,string title)
        {
            try
            {
                if (id != null)
                {
                    var query =await _Service.GetById(id);
                    return View(query);
                }
                return null;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        [Route("Faq")]
        public async Task<IActionResult> Faq()
        {
            var query = ((List<FaqCategoryModel>)(await _faqcatrepo.GetAll()).Data).Where(x => x.F_Status && x.F_IsDeleted != true).ToList();
           
            return View(query);
        }
        [Route("FaqPage")]
        public async Task<IActionResult> FaqPage(int id, int page = 1)
        {
            var query = ((List<FaqModel>)(await _faqrepo.GetAll()).Data).ToList() ;
            if (id != 0)
            {
                query = query.Where(x => x.FA_CatId == id).ToList();
            }
            ViewBag.catid = id;
            #region Paging
            Paging paging = new Paging();
            paging.TotalCount = query.Count();
            paging.PageSize = 5;
            paging.CurrentPage = page;
            paging.TotalPages = (int)Math.Ceiling(paging.TotalCount / (double)paging.PageSize);
            ViewBag.paging = paging;
            #endregion
            var items = query.Skip((paging.CurrentPage - 1) * paging.PageSize).Take(paging.PageSize).ToList();
            return View(items);
        }
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
