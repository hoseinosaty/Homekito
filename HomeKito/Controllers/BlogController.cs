using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Barayand.Common.Constants;
using Barayand.DAL.Context;
using Barayand.DAL.Interfaces;
using Barayand.Models.Entity;
using Barayand.OutModels.Miscellaneous;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using HomeKito.Models;

namespace HomeKito.Controllers
{
    public class BlogController : Controller
    {
        private readonly IPublicMethodRepsoitory<NoticesCategoryModel> _categoryrepo;
        private readonly IPublicMethodRepsoitory<DynamicPagesContent> _DynamicPageRepository;
        private readonly IPublicMethodRepsoitory<NoticesModel> _noticesrepo;
        private readonly IRateRepository _rateRepository;
        private readonly ICommentRepository _commentRepository;
        private readonly IPCRepository _categories;

        public BlogController(IPublicMethodRepsoitory<NoticesCategoryModel> categoryrepo, IPublicMethodRepsoitory<NoticesModel> noticesrepo, IRateRepository rateRepository, ICommentRepository commentRepository, IPublicMethodRepsoitory<DynamicPagesContent> DynamicPageRepository, IPCRepository categories)
        {
            _categoryrepo = categoryrepo;
            _noticesrepo = noticesrepo;
            _rateRepository = rateRepository;
            _DynamicPageRepository = DynamicPageRepository;
            _commentRepository = commentRepository;
            _categories = categories;
        }
       
        public async Task<IActionResult> Index(int cat=0,string title = null)
        {
            ViewBag.Category = ((List<NoticesCategoryModel>)(await _categoryrepo.GetAll()).Data).Where(x => x.NC_IsDeleted == false&&x.NC_Status&& x.NC_Type == 1).OrderBy(x => x.NC_SortField).ToList();
                
            var ProdCatList=(List<ProductCategoryModel>)(await _categories.GetAll()).Data;
            ProdCatList.Where(x => x.PC_ParentId == 0&&x.PC_Status&&x.PC_IsDeleted!=true).ToList();
            ViewBag.ProductCategory = ProdCatList;
            var AllNews = ((List<NoticesModel>)(await _noticesrepo.GetAll()).Data).Where(x => x.N_IsDeleted == false&&x.N_Status && x.N_Type == 1).OrderBy(x => x.N_Sort).ToList();
            return View(AllNews);
        }

        [Route("Blog/Cat/{cat?}/{title?}")]
        public async Task<IActionResult> Category(int cat = 0, string title = null)
        {
            ViewBag.Category = ((List<NoticesCategoryModel>)(await _categoryrepo.GetAll()).Data).Where(x => x.NC_IsDeleted == false && x.NC_Status && x.NC_Type == 1).OrderBy(x => x.NC_SortField).ToList();
            ViewBag.catid = cat;
             var ProdCatList = (List<ProductCategoryModel>)(await _categories.GetAll()).Data;
            ProdCatList.Where(x => x.PC_ParentId == 0&&x.PC_Status&&x.PC_IsDeleted!=true).ToList();
            ViewBag.ProductCategory = ProdCatList;
            var AllNews = ((List<NoticesModel>)(await _noticesrepo.GetAll()).Data).Where(x => x.N_IsDeleted == false &&x.N_Status&& x.N_Type == 1&&x.N_CId==cat).OrderBy(x => x.N_Sort).ToList();
            return View(AllNews);
        }

        [Route("Blog/Detail/{Id?}/{title?}")]
        public async Task<IActionResult> Detail(int Id = 0, string title = null)
        {
            ViewBag.Category = ((List<NoticesCategoryModel>)(await _categoryrepo.GetAll()).Data).Where(x => x.NC_IsDeleted == false && x.NC_Type == 1).OrderBy(x => x.NC_SortField).ToList();
            
            var news =await _noticesrepo.GetById(Id);
            ViewBag.AllNews = ((List<NoticesModel>)(await _noticesrepo.GetAll()).Data).Where(x => x.N_IsDeleted == false && x.N_Status && x.N_Type == 1).OrderBy(x => x.N_Sort).ToList();
            ViewBag.catroute =await _categoryrepo.GetById(news.N_CId);



            return View(news);

        }


    }
}
