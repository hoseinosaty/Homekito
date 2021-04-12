using Barayand.DAL.Context;
using Barayand.DAL.Interfaces;
using Barayand.Models.Entity;
using Barayand.OutModels.Miscellaneous;
using Barayand.OutModels.Models;
using Barayand.OutModels.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Barayand.DAL.Repositories
{
    public class CatAttrRelationRepository : GenericRepository<CatAttrRelationModel>, IPublicMethodRepsoitory<CatAttrRelationModel>
    {
        private readonly BarayandContext _context;

        public CatAttrRelationRepository(BarayandContext context):base(context)
        {
            this._context = context;
        }
        public async Task<ResponseStructure> LogicalAvailable(object id, bool newState)
        {
            try
            {
                var item = await this.GetById(id);
                item.X_Status = newState;
                return await this.Update(item);
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public async Task<ResponseStructure> LogicalDelete(object id)
        {
            try
            {
                var item = await this.GetById(id);
                item.X_IsDeleted = true;
                return await this.Update(item);
            }
            catch (Exception ex)
            {
                return null;
            }
        }


        public async Task<ResponseStructure> GetAttrsByCat(int catid)
        {
            try
            {
                List<AttributeModel> data =  this._context.Attribute.ToList();
               

                var catsAttrAll = this._context.CategoryAttribute.ToList();

                List<CatAttrRelationModel> catsAttr = catsAttrAll.Where(x => x.X_CatId == catid && x.X_IsDeleted == false).ToList();

                List<object> Data = new List<object>();

                foreach (var item in catsAttr)
                {
                    Data.Add(new
                    {
                        A_Title = data.FirstOrDefault(x => x.A_Id == item.X_AttrId).A_Title,
                        A_Id = data.FirstOrDefault(x => x.A_Id == item.X_AttrId).A_Id,
                        A_Status = item.X_Status,
                        relationId = item.X_Id,
                    });
                }
                return ResponseModel.Success("ATTRIBUTE_LIST_RETURNED", Data);
            }
            catch (Exception ex)
            {
                return null;
            }
        }
        public async Task<ResponseStructure> Insert(CatAttrRelationModel entity)
        {
            try
            {
                var checkExists = this._context.CategoryAttribute.Count(x=>x.X_AttrId == entity.X_AttrId && x.X_CatId == entity.X_CatId && x.X_IsDeleted == false);
                if(checkExists > 0)
                {
                    return ResponseModel.Error("فیلد اختصاصی مورد نظر برای دسته بندی انتخاب شده قبلا تعریف گردیده است");
                }
                await this._context.CategoryAttribute.AddAsync(entity: entity);
                await this.CommitAllChanges();
                var catattrid = this._context.CategoryAttribute.FirstOrDefault(x => x.X_AttrId == entity.X_AttrId && x.X_CatId == entity.X_CatId);
                if(catattrid == null)
                {
                    return ResponseModel.ServerInternalError();
                }
                if (entity.X_Type == 1)
                {
                    string[] RawAnswers = entity.X_Answers.Split(':');
                    string[] answers = RawAnswers[0].Split(',');
                    string[] sorts = RawAnswers[1].Split(',');
                    List<AttrAnswerModel> attrAnswerModels = new List<AttrAnswerModel>();
                    for(int i = 0;i<answers.Length;i++)
                    {
                        attrAnswerModels.Add(new AttrAnswerModel() {
                        X_Answer = answers[i],
                        X_CatAttrId = catattrid.X_Id,
                        X_Sort = int.Parse(sorts[i]),
                        X_Status = true
                        });
                    }
                    await this._context.AttributeAnswer.AddRangeAsync(attrAnswerModels);
                    await this.CommitAllChanges();
                }
                return ResponseModel.Success("operation successfully completed");
            }
            catch (Exception ex)
            {
                return ResponseModel.ServerInternalError(data:ex);
            }
        }

    }
}
