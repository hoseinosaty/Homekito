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
using Barayand.Common.Constants;
using Barayand.OutModels.Miscellaneous;

namespace Barayand.Controllers.Cpanel.Product
{
    
    [Route("api/cpanel/product/[controller]")]
    [ApiController]
    
    public class CategoryController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly IPCRepository _repository;
        private readonly IPublicMethodRepsoitory<AttributeModel> _attrrepository;
        private readonly IPublicMethodRepsoitory<CatAttrRelationModel> _catattrrepository;
        private readonly IPublicMethodRepsoitory<AttrAnswerModel> _attranswerrepository;
        public CategoryController(IMapper mapper,IPCRepository repository, IPublicMethodRepsoitory<AttributeModel> attrrepository, IPublicMethodRepsoitory<CatAttrRelationModel> catattrrepository, IPublicMethodRepsoitory<AttrAnswerModel> attranswerrepository)
        {
            this._repository = repository;
            this._attrrepository = attrrepository;
            this._catattrrepository = catattrrepository;
            this._attranswerrepository = attranswerrepository;
            this._mapper = mapper;
        }
        [Route("AddCategory")]
        [HttpPost]
        public async Task<ActionResult> AddCategory(OutModels.Models.ProductCat cat)
        {
            try
            {
                ProductCategoryModel pcm =(ProductCategoryModel)_mapper.Map<OutModels.Models.ProductCat, ProductCategoryModel>(cat);
                return new JsonResult(await this._repository.Insert(pcm));
            }
            catch(Exception ex)
            {
                return null;
            }
        }
        [Route("UpdateCategory")]
        [HttpPost]
        public async Task<ActionResult> UpdateCategory(OutModels.Models.ProductCat cat)
        {
            try
            {
                ProductCategoryModel pcm = (ProductCategoryModel)_mapper.Map<OutModels.Models.ProductCat, ProductCategoryModel>(cat);
                if(pcm.PC_Id == pcm.PC_ParentId)
                {
                    return new JsonResult(ResponseModel.Error("دسته بندی جاری نمیتوانند زیر مجموعه خودش قرار بگیرد"));
                }
                return new JsonResult(await this._repository.Update(pcm));
            }
            catch (Exception ex)
            {
                return null;
            }
        }
        [Route("ActiveCategory")]
        [HttpPost]
        public async Task<ActionResult> ActiveCategory(OutModels.Models.ProductCat cat)
        {
            try
            {
                int id = cat.PC_Id;
                if (id == 0)
                {
                    return new JsonResult(ResponseModel.Error("دسته بندی مورد نظر یافت نشد"));
                }
                return new JsonResult(await this._repository.LogicalAvailable(id,true));
            }
            catch (Exception ex)
            {
                return null;
            }
        }
        [Route("DisableCategory")]
        [HttpPost]
        public async Task<ActionResult> DisableCategory(OutModels.Models.ProductCat cat)
        {
            try
            {
                int id = cat.PC_Id;
                if (id == 0)
                {
                    return new JsonResult(ResponseModel.Error("دسته بندی مورد نظر یافت نشد"));
                }
                return new JsonResult(await this._repository.LogicalAvailable(id, false));
            }
            catch (Exception ex)
            {
                return null;
            }
        }
        [Route("DeleteCategory")]
        [HttpPost]
        public async Task<ActionResult> DeleteCategory(OutModels.Models.ProductCat cat)
        {
            try
            {
                int id = cat.PC_Id;
                if (id == 0)
                {
                    return new JsonResult(ResponseModel.Error("دسته بندی مورد نظر یافت نشد"));
                }
                return new JsonResult(await this._repository.LogicalDelete(id));
            }
            catch (Exception ex)
            {
                return null;
            }
        }
        [Route("LoadCategory/{type?}/{lang}")]
        [HttpPost]
        public async Task<ActionResult> GetAllCategories(string lang,int type=1)//default load physical
        {
            try
            {
                type = type == 0 ? 1 : type;
                List<ProductCategoryModel> data = (List<ProductCategoryModel>)(await this._repository.GetAll()).Data;
                List<ProductCat> result = _mapper.Map<List<ProductCategoryModel>,List<ProductCat>>(data.Where(x=>x.PC_Type == type && x.Lang == lang).OrderBy(x=>x.PC_OrderField).ToList());
                return new JsonResult(ResponseModel.Success("CATEGORY_LIST_RETURNED",result));
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        ///////////////////////OTHER PAGE REQUESTS HANDLER
        ///
        [Route("GetCategoryChildsById")]
        [HttpPost]
        public async Task<ActionResult> GetCategoryForProductCreationPage(Miscellaneous misc)
        {
            try
            {
                if(misc.Id == 0)
                {
                    return new JsonResult( ResponseModel.Error(msg:"دسته بندی یافت نشد"));
                }
                var getChilds = (List<ProductCategoryModel>)(await this._repository.GetAllByParentIdOneLevel(misc.Id)).Data;
                List<ProductCat> result = _mapper.Map<List<ProductCategoryModel>, List<ProductCat>>(getChilds.Where(x=> !x.PC_IsDeleted && x.PC_Status).ToList());
                return new JsonResult(ResponseModel.Success(data: new {childs = result }));
            }
            catch (Exception ex)
            {
                return null;
            }
        }
        [Route("GetCategoryAttributes")]
        [HttpPost]
        public async Task<ActionResult> GetProductionCategoryAttributes(Miscellaneous misc)
        {
            try
            {
                if (misc.Id == 0)
                {
                    return new JsonResult(ResponseModel.Error(msg: "دسته بندی یافت نشد"));
                }
                var getChilds = (List<ProductCategoryModel>)(await this._repository.GetAllByParentIdOneLevel(misc.Id)).Data;
                var AllAttributes = ((List<AttributeModel>)(await this._attrrepository.GetAll()).Data).Where(x=>!x.A_IsDeleted && x.A_Status).ToList();
                var AllCatAttributes = (List<CatAttrRelationModel>)(await this._catattrrepository.GetAll()).Data;
                var AllAnswers = ((List<AttrAnswerModel>)(await this._attranswerrepository.GetAll()).Data).Where(x=>!x.X_IsDeleted && x.X_Status).ToList();
                List<SetAttributeComboModel> Attributes = new List<SetAttributeComboModel>();
                var catattr = AllCatAttributes.Where(x => x.X_CatId == misc.Id);
                foreach (var item in catattr)
                {
                    string ATitle = "";
                    int AId = 0;
                    int AType = 0;
                    List<object> AnswersList = new List<object>();

                    var attribute = AllAttributes.FirstOrDefault(x => x.A_Id == item.X_AttrId);
                    if (attribute != null)
                    {
                        ATitle = attribute.A_Title;
                        AId = attribute.A_Id;
                        AType = attribute.A_Type;
                        if (AType == 1)
                        {
                            var answers = AllAnswers.Where(x => x.X_CatAttrId == item.X_Id);
                            foreach (var ansItem in answers)
                            {
                                AnswersList.Add(new { Id = ansItem.X_Id, Title = ansItem.X_Answer });
                            }
                        }
                        Attributes.Add(new SetAttributeComboModel()
                        {
                            Answer = AnswersList,
                            Id = AId,
                            Title = ATitle,
                            Type = AType
                        });
                    }
                    else
                    {
                        continue;
                    }

                }
                return new JsonResult(ResponseModel.Success(data: Attributes));
            }
            catch (Exception ex)
            {
                return new JsonResult(ResponseModel.ServerInternalError(data:ex));
            }
        }

        [Route("GetCategoryLevelOne/{type?}")]
        [HttpPost]
        public async Task<ActionResult> GetCategoryLevelOne(int type = 1)//Default load physical
        {
            try
            {
                type = type == 0 ? 1 : type;
                int MAX = 0;
                if(type == 1)
                {
                    MAX = Constants.MAX_CAT_LEVEL;
                }
                else if(type == 2)
                {
                    MAX = Constants.MAX_DIGITAL_CAT_LEVEL;
                }
                else if (type == 3)
                {
                    MAX = Constants.MAX_TRAINING_CAT_LEVEL;
                }
                var getChilds = (List<ProductCategoryModel>)(await this._repository.GetAll()).Data;
                

                List<ProductCat> result = _mapper.Map<List<ProductCategoryModel>, List<ProductCat>>(getChilds.Where(x=>x.PC_ParentId == 0 && x.PC_Type == type).ToList());
                return new JsonResult(ResponseModel.Success(data: new { Cats = result, Max = MAX }));
                
            }
            catch (Exception ex)
            {
                return null;
            }
        }
    }
}