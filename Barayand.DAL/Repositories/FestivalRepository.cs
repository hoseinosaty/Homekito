using Barayand.DAL.Context;
using Barayand.DAL.Interfaces;
using Barayand.Models.Entity;
using Barayand.OutModels.Miscellaneous;
using Barayand.OutModels.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Barayand.DAL.Repositories
{
    public class FestivalRepository : GenericRepository<FestivalOfferModel>,IPublicMethodRepsoitory<FestivalOfferModel>, IFestivalRepository
    {
        private readonly BarayandContext _context;
        public FestivalRepository(BarayandContext context) : base(context)
        {
            this._context = context;
        }

        public async Task<ResponseStructure> InsertCollation(FestivalCreationModel data)
        {
            try
            {
                var AllFestivals = ((List<FestivalOfferModel>)(await GetAll()).Data);
                if (data.Type == 1)
                {
                    if(AllFestivals.Count(x=>x.F_Type == 1) > 0)
                    {
                        return ResponseModel.Error("برای تمام محصولا فقط میتوان یکبار درصد تخفیف کلی اعمال نمود");
                    }
                    return (await Insert(new FestivalOfferModel()
                    {
                        F_Discount = data.Discount,
                        F_Title = data.Title,
                        F_Type = data.Type
                    }));
                    
                }
                else
                {
                    if(data.Categories.Count() < 1)
                    {
                        return ResponseModel.Error("دسته بندی یافت نشد");
                    }
                    
                    List<FestivalOfferModel> PrepareData = new List<FestivalOfferModel>();
                    foreach(var item in data.Categories)
                    {
                        if(AllFestivals.Count(x=>x.F_EndLevelCategoryId == item) < 1)
                        {
                            await Insert(new FestivalOfferModel()
                            {
                                F_Discount = data.Discount,
                                F_EndLevelCategoryId = item,
                                F_Title = data.Title,
                                F_Type = data.Type
                            });
                        }
                    }
                    return ResponseModel.Success("اطلاعات با موفقیت ذخیره گردید");
                }
            }
            catch(Exception ex)
            {
                return ResponseModel.ServerInternalError(data:ex);
            }
        }

        public async Task<ResponseStructure> LogicalAvailable(object id, bool newState)
        {
            try
            {
                var festivel = await GetById(id);
                if (festivel == null)
                {
                    return ResponseModel.Error("تخفیف مورد نظر یافت نشد");
                }
                festivel.F_Status = newState;
                festivel.Updated_At = DateTime.Now;
                return await Update(festivel);
            }
            catch (Exception ex)
            {
                return ResponseModel.ServerInternalError(data: ex);
            }
        }

        public async Task<ResponseStructure> LogicalDelete(object id)
        {
            try
            {
                var festivel = await GetById(id);
                if(festivel == null)
                {
                    return ResponseModel.Error("تخفیف مورد نظر یافت نشد");
                }
                festivel.F_IsDeleted = true;
                festivel.Updated_At = DateTime.Now;
                return await Update(festivel);
            }
            catch(Exception ex)
            {
                return ResponseModel.ServerInternalError(data:ex);
            }
        }
    }
}
