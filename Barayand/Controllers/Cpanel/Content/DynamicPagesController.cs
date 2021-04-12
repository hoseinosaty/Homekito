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

namespace Barayand.Controllers.Cpanel.Content
{
    [Route("api/cpanel/[controller]")]
    [ApiController]
    public class DynamicPagesController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly IPublicMethodRepsoitory<DynamicPagesContent> _repository;
        private readonly IPublicMethodRepsoitory<SocialMediaTitlesModel> _socialrepository;
        private readonly IPublicMethodRepsoitory<DepartmentModel> _departmentrepo;
        private readonly IPublicMethodRepsoitory<IndexBoxProductRelModel> _indexsectionrepo;
        private readonly IPublicMethodRepsoitory<IndexBoxInfoModel> _indexboxinforepo;
        private readonly IPublicMethodRepsoitory<ProductModel> _productrepo;
        private readonly IPublicMethodRepsoitory<ServiceModel> _servicerepo;


        public DynamicPagesController(IMapper mapper, IPublicMethodRepsoitory<DynamicPagesContent> repository, IPublicMethodRepsoitory<SocialMediaTitlesModel> socialrepository, IPublicMethodRepsoitory<DepartmentModel> departmentrepo, IPublicMethodRepsoitory<IndexBoxProductRelModel> indexboxrepo, IPublicMethodRepsoitory<IndexBoxInfoModel> indexboxinforepo, IPublicMethodRepsoitory<ProductModel> productrepo, IPublicMethodRepsoitory<ServiceModel> servicerepo)
        {
            this._repository = repository;
            this._socialrepository = socialrepository;
            this._mapper = mapper;
            this._departmentrepo = departmentrepo;
            this._indexsectionrepo = indexboxrepo;
            this._indexboxinforepo = indexboxinforepo;
            this._productrepo = productrepo;
            this._servicerepo = servicerepo;
        }

        [Route("AddPageContent")]
        public async Task<ActionResult> AddPageContent(OutModels.Models.DynamicPages page)
        {
            try
            {
                DynamicPagesContent cm = (DynamicPagesContent)_mapper.Map<OutModels.Models.DynamicPages, DynamicPagesContent>(page);
                return new JsonResult(await this._repository.Insert(cm));
            }
            catch (Exception ex)
            {
                return null;
            }
        }
        [HttpPost]
        [Route("GetPageContent")]
        public async Task<ActionResult> GetPageContent(OutModels.Models.DynamicPages page)
        {
            try
            {
                var dynamicPages = (List<DynamicPagesContent>)(await _repository.GetAll()).Data;
                var data = dynamicPages.FirstOrDefault(x => x.PageName == page.PageName && page.Lang == x.Lang);
                return new JsonResult(ResponseModel.Success(data:data));
            }
            catch (Exception ex)
            {
                return null;
            }
        }
        [HttpPost]
        [Route("GetSocialTitles")]
        public async Task<ActionResult> GetSocialMediaTitles()
        {
            try
            {
                var social = (List<SocialMediaTitlesModel>)(await _socialrepository.GetAll()).Data;
                List<SocialMedia> result = new List<SocialMedia>();
                foreach(var item in social)
                {
                    result.Add(new SocialMedia(){ 
                        Title = item.SM_Title,
                        Url = item.SM_Url,
                        Icon = item.SM_Icon,
                        Id = item.SM_Id
                    });
                }
                return new JsonResult(ResponseModel.Success(data: result));
            }
            catch (Exception ex)
            {
                return null;
            }
        }


        /////Departments
        [HttpPost]
        [Route("AddDepartment")]
        public async Task<ActionResult> AddDepartment(DepartmentModel dp)
        {
            try
            {
                return new JsonResult(await _departmentrepo.Insert(dp));
            }
            catch (Exception ex)
            {
                return null;
            }
        }
        [HttpPost]
        [Route("UpdateDepartment")]
        public async Task<ActionResult> UpdateDepartment(DepartmentModel dp)
        {
            try
            {
                return new JsonResult(await _departmentrepo.Update(dp));
            }
            catch (Exception ex)
            {
                return null;
            }
        }
        [HttpPost]
        [Route("ActiveDepartment")]
        public async Task<ActionResult> AciveDepartment(DepartmentModel dp)
        {
            try
            {
                return new JsonResult(await _departmentrepo.LogicalAvailable(dp.D_Id,true));
            }
            catch (Exception ex)
            {
                return null;
            }
        }
        [HttpPost]
        [Route("DeativeDepartment")]
        public async Task<ActionResult> DeaciveDepartment(DepartmentModel dp)
        {
            try
            {
                return new JsonResult(await _departmentrepo.LogicalAvailable(dp.D_Id, false));
            }
            catch (Exception ex)
            {
                return null;
            }
        }
        [HttpPost]
        [Route("LoadDepartments/{lang}")]
        public async Task<ActionResult> LoadDepartments(string lang = "")
        {
            try
            {
                var deps = ((List<DepartmentModel>)(await _departmentrepo.GetAll()).Data).Where(x=>x.Lang == lang).ToList();
                return new JsonResult(ResponseModel.Success(data:deps));
            }
            catch (Exception ex)
            {
                return null;
            }
        }
        ///
        //Index page sections setting
        [HttpPost]
        [Route("SetIndexBox")]
        public async Task<ActionResult> SetIndexSections(IndexSectionsModel ism)
        {
            try
            {
                IndexBoxInfoModel BoxInfo = ((List<IndexBoxInfoModel>)(await _indexboxinforepo.GetAll()).Data).FirstOrDefault(x => x.I_SecId == ism.Section && x.Lang == ism.Lang);

                if(BoxInfo == null)
                {
                    await _indexboxinforepo.Insert(new IndexBoxInfoModel() {
                        I_SecId = ism.Section,
                        I_Title = ism.Title,
                        I_Icon = ism.Icon,
                        I_Sort = ism.Sort,
                        Lang = ism.Lang
                    });
                    await _indexboxinforepo.CommitAllChanges();
                }
                else
                {
                    await _indexboxinforepo.Update(new IndexBoxInfoModel()
                    {
                        I_Id =BoxInfo.I_Id,
                        I_SecId = ism.Section,
                        I_Title = ism.Title,
                        I_Icon = ism.Icon,
                        I_Sort = ism.Sort,
                        Lang = ism.Lang
                    });
                    await _indexboxinforepo.CommitAllChanges();
                }
                List<IndexBoxProductRelModel> OldRelations = ((List<IndexBoxProductRelModel>)(await _indexsectionrepo.GetAll()).Data).Where(x=>x.I_SecId == ism.Section && x.Lang == ism.Lang).ToList();
                foreach(var item in OldRelations)
                {
                    await _indexsectionrepo.Delete(item);
                }
                await _indexsectionrepo.CommitAllChanges();
                foreach(int p in ism.Products)
                {
                    await _indexsectionrepo.Insert(new IndexBoxProductRelModel() { 
                        Lang = ism.Lang,
                        I_Pid = p,
                        I_SecId = ism.Section
                    });
                }
                await _indexsectionrepo.CommitAllChanges();
                return new JsonResult(ResponseModel.Success("اطلاعات با موفقیت ذخیره گردید"));
            }
            catch (Exception ex)
            {
                return null;
            }
        }
        [HttpPost]
        [Route("GetIndexBox")]
        public async Task<ActionResult> GetIndexSections(IndexSectionsModel ism)
        {
            try
            {
                var box = ((List<IndexBoxInfoModel>)(await _indexboxinforepo.GetAll()).Data).Where(x=>x.Lang == ism.Lang && x.I_SecId == ism.Section).ToList();
                if(box.Count() < 1)
                {
                    return new JsonResult(ResponseModel.Error(data:new {Products=new List<ProductModel>(),Info="" }));
                }
                var PIds = ((List<IndexBoxProductRelModel>)(await _indexsectionrepo.GetAll()).Data).Where(x => x.Lang == ism.Lang && x.I_SecId == ism.Section).ToList();
                List<ProductModel> products = new List<ProductModel>();
                foreach(var p in PIds)
                {
                    products.Add(await _productrepo.GetById(p.I_Pid));
                }
                return new JsonResult(ResponseModel.Success(data:new { Products = products, Info  = box.FirstOrDefault()}));
            }
            catch(Exception ex)
            {
                return new JsonResult(ResponseModel.ServerInternalError(data:ex));
            }
        }
        //
        // services and profits
        [HttpPost]
        [Route("AddService")]
        public async Task<ActionResult> AddService(ServiceModel service)
        {
            try
            {
                return new JsonResult(await _servicerepo.Insert(service));
            }
            catch (Exception ex)
            {
                return null;
            }
        }
        [HttpPost]
        [Route("UpdateService")]
        public async Task<ActionResult> UpdateService(ServiceModel dp)
        {
            try
            {
                return new JsonResult(await _servicerepo.Update(dp));
            }
            catch (Exception ex)
            {
                return null;
            }
        }
        [HttpPost]
        [Route("ActiveService")]
        public async Task<ActionResult> ActiveService(ServiceModel service)
        {
            try
            {
                return new JsonResult(await _servicerepo.LogicalAvailable(service.S_Id,true));
            }
            catch (Exception ex)
            {
                return null;
            }
        }
        [HttpPost]
        [Route("DisableService")]
        public async Task<ActionResult> DisableService(ServiceModel service)
        {
            try
            {
                return new JsonResult(await _servicerepo.LogicalAvailable(service.S_Id,false));
            }
            catch (Exception ex)
            {
                return null;
            }
        }
        [HttpPost]
        [Route("DeleteService")]
        public async Task<ActionResult> DeleteService(ServiceModel service)
        {
            try
            {
                return new JsonResult(await _servicerepo.Delete(service.S_Id));
            }
            catch (Exception ex)
            {
                return null;
            }
        }
        [HttpPost]
        [Route("LoadServices/{type}")]
        public async Task<ActionResult> LoadServices(int type = 1)
        {
            try
            {
                var deps = ((List<ServiceModel>)(await _servicerepo.GetAll()).Data).Where(x => x.S_Type == type).ToList();
                return new JsonResult(ResponseModel.Success(data: deps));
            }
            catch (Exception ex)
            {
                return null;
            }
        }
        //
    }
}