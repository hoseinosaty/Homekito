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
using Newtonsoft.Json;

namespace Barayand.DAL.Repositories
{
    public class TrainingRepository : GenericRepository<TrainingModel>, IPublicMethodRepsoitory<TrainingModel>
    {
        private readonly BarayandContext _context;
        private readonly IPCRepository _pCRepository;
        public TrainingRepository(BarayandContext context, IPCRepository pCRepository) : base(context)
        {
            this._context = context;
            this._pCRepository = pCRepository;
        }
        public async Task<ResponseStructure> LogicalAvailable(object id, bool newState)
        {
            try
            {
                var item = await this.GetById(id);
                item.T_Status = newState;
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
                item.T_IsDeleted = true;
                return await this.Update(item);
            }
            catch (Exception ex)
            {
                return null;
            }
        }
        public async Task<ResponseStructure> Insert(TrainingModel entity)
        {
            try
            {
                entity.T_Code = "VP-" + entity.T_MainCatId + "" + entity.T_EndLevelCatId + DateTime.Now.ToString("yyyyMMddHHmmss");
                this._context.Trainings.Add(entity);
                await this.CommitAllChanges();
                //Product Atrribute And Answer Registration

                int tid = entity.T_Id;//product id
                if (!string.IsNullOrEmpty(entity.T_DedicatedField))
                {
                    List<IncomeProdFieldSetModel> DedicatedFields = JsonConvert.DeserializeObject<List<IncomeProdFieldSetModel>>(entity.T_DedicatedField);
                    if (DedicatedFields != null)
                    {
                        List<ProductAttributeModel> Attributes = new List<ProductAttributeModel>();
                        foreach (var item in DedicatedFields)
                        {
                            int AnswerId = 0;
                            bool isInteger = int.TryParse(item.value + "", out AnswerId);
                            Attributes.Add(new ProductAttributeModel()
                            {
                                X_AId = item.id,
                                X_AnswerId = (isInteger) ? AnswerId : 0,
                                X_AnswerTitle = (!isInteger) ? item.value : null,
                                X_PId = tid,
                                X_EntityType = 2
                            });
                        }
                        this._context.ProductAttributeAnswer.AddRange(Attributes);
                        await this.CommitAllChanges();
                    }
                }

                if (!string.IsNullOrEmpty(entity.T_Seasons))
                {
                    List<TrainingSeasonsModel> Seasons = new List<TrainingSeasonsModel>();
                    List<SeasonStructure> UserEnterd = JsonConvert.DeserializeObject<List<SeasonStructure>>(entity.T_Seasons);
                    foreach(var s in UserEnterd)
                    {
                        Seasons.Add(new TrainingSeasonsModel() { 
                            S_Cost = decimal.Parse(s.cost),
                            S_Sort = int.Parse(s.sort),
                            S_TId = tid,
                            S_Time = s.time,
                            S_Title = s.title,
                            S_VideoUrl = s.url
                        });
                    }
                    this._context.TrainingSeasons.AddRange(Seasons);
                    await this.CommitAllChanges();
                }

                return ResponseModel.Success("operation successfully completed");
            }
            catch (Exception ex)
            {
                return ResponseModel.ServerInternalError(data: ex);
            }
        }
        public async Task<ResponseStructure> Update(TrainingModel entity)
        {
            try
            {
                var item = await this.GetById(entity.T_Id);
                entity.Created_At = item.Created_At;
                entity.Updated_At = DateTime.Now;
                this._context.Entry(item).State = Microsoft.EntityFrameworkCore.EntityState.Detached;
                this._context.Trainings.Update(entity);
                await this.CommitAllChanges();
                //Product Atrribute And Answer Registration
                if (this._context.ProductAttributeAnswer.Count(x => x.X_PId == entity.T_Id && x.X_EntityType == 2) > 0)
                {
                    this._context.ProductAttributeAnswer.RemoveRange(this._context.ProductAttributeAnswer.Where(x => x.X_PId == entity.T_Id && x.X_EntityType == 2).ToList());
                    await this.CommitAllChanges();
                }
               
                int pid = entity.T_Id;//product id
                if (!string.IsNullOrEmpty(entity.T_DedicatedField))
                {
                    List<IncomeProdFieldSetModel> DedicatedFields = JsonConvert.DeserializeObject<List<IncomeProdFieldSetModel>>(entity.T_DedicatedField);
                    if (DedicatedFields != null)
                    {
                        List<ProductAttributeModel> Attributes = new List<ProductAttributeModel>();
                        foreach (var ditem in DedicatedFields)
                        {
                            int AnswerId = 0;
                            bool isInteger = int.TryParse(ditem.value + "", out AnswerId);
                            Attributes.Add(new ProductAttributeModel()
                            {
                                X_AId = ditem.id,
                                X_AnswerId = (isInteger) ? AnswerId : 0,
                                X_AnswerTitle = (!isInteger) ? ditem.value : null,
                                X_PId = pid,
                                X_EntityType = 2
                            });
                        }
                        await this._context.ProductAttributeAnswer.AddRangeAsync(Attributes);
                        await this.CommitAllChanges();
                    }
                }
                if (this._context.TrainingSeasons.Count(x => x.S_TId == entity.T_Id ) > 0)
                {
                    this._context.TrainingSeasons.RemoveRange(this._context.TrainingSeasons.Where(x => x.S_TId == entity.T_Id ).ToList());
                    await this.CommitAllChanges();
                }
                if (!string.IsNullOrEmpty(entity.T_Seasons))
                {
                    List<TrainingSeasonsModel> Seasons = new List<TrainingSeasonsModel>();
                    List<SeasonStructure> UserEnterd = JsonConvert.DeserializeObject<List<SeasonStructure>>(entity.T_Seasons);
                    foreach (var s in UserEnterd)
                    {
                        Seasons.Add(new TrainingSeasonsModel()
                        {
                            S_Cost = decimal.Parse(s.cost),
                            S_Sort = int.Parse(s.sort),
                            S_TId = pid,
                            S_Time = s.time,
                            S_Title = s.title,
                            S_VideoUrl = s.url
                        });
                    }
                    this._context.TrainingSeasons.AddRange(Seasons);
                    await this.CommitAllChanges();
                }

                return ResponseModel.Success("رکورد مورد نظر با موفقیت بروزرسانی گردید");
            }
            catch (Exception ex)
            {
                return ResponseModel.ServerInternalError(data: ex);
            }
        }
        public async Task<ResponseStructure> GetAll()
        {
            try
            {
                List<TrainingModel> AllProducts = this._context.Trainings.Where(x => x.T_IsDeleted == false).ToList();
                List<ProductCategoryModel> AllCategories = this._context.ProductCategory.ToList();
                List<TrainingSeasonsModel> AllSeasions = this._context.TrainingSeasons.ToList();
                foreach (var item in AllProducts)
                {
                    string cattitle = "";
                    var cat = AllCategories.FirstOrDefault(x => x.PC_Id == item.T_EndLevelCatId);
                    if (cat != null)
                    {
                        cattitle = cat.PC_Title;
                    }
                  
                    AllProducts.FirstOrDefault(x => x.T_Id == item.T_Id).T_CatTitle = cattitle;
                    
                    AllProducts.FirstOrDefault(x => x.T_Id == item.T_Id).T_ParentCategories = await this._pCRepository.GetCategoryParents(item.T_EndLevelCatId);
                    List<SeasonStructure> TrainSeasons = new List<SeasonStructure>();
                    List<TrainingSeasonsModel> TrainSeasonsModel = new List<TrainingSeasonsModel>();
                    foreach(var season in AllSeasions.Where(x=>x.S_TId == item.T_Id).ToList())
                    {
                        TrainSeasons.Add(new SeasonStructure() {
                            cost = season.S_Cost.ToString(),
                            sort = season.S_Sort.ToString(),
                            time = season.S_Time,
                            title = season.S_Title,
                            url = season.S_VideoUrl
                        });
                        TrainSeasonsModel.Add(season);
                    }
                    AllProducts.FirstOrDefault(x => x.T_Id == item.T_Id).T_Seasons = JsonConvert.SerializeObject(TrainSeasons);
                    AllProducts.FirstOrDefault(x => x.T_Id == item.T_Id).T_SeasonsModel = TrainSeasonsModel;
                }
                return ResponseModel.Success(data: AllProducts);
            }
            catch (Exception ex)
            {
                return ResponseModel.Error(msg: ex.Message, data: ex);
            }
        }
    }
}
