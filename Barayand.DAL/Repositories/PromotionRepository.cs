using Barayand.DAL.Context;
using Barayand.DAL.Interfaces;
using Barayand.Models.Entity;
using Barayand.OutModels.Models;
using Barayand.OutModels.Response;
using System;
using System.Collections.Generic;

using System.Linq;
using System.Threading.Tasks;

namespace Barayand.DAL.Repositories
{
    public class PromotionRepository : GenericRepository<PromotionBoxModel>, IPromotionRepository
    {
        private readonly BarayandContext _context;
        private readonly IPublicMethodRepsoitory<ProductModel> _productrepo;
        private readonly IPCalcRepository _pcalcrepo;
        private readonly IPublicMethodRepsoitory<OptionsModel> _optionrepo;
        public PromotionRepository(BarayandContext context, IPublicMethodRepsoitory<ProductModel> productrepo, IPCalcRepository pcalcrepo, IPublicMethodRepsoitory<OptionsModel> optionrepo) : base(context)
        {
            _context = context;
            _productrepo = productrepo;
            _pcalcrepo = pcalcrepo;
            _optionrepo = optionrepo;
        }

        public async Task<List<PromotionBoxModel>> GetByType(int Type)
        {
            try
            {
                var opt = ((List<OptionsModel>)(await _optionrepo.GetAll()).Data).FirstOrDefault(x => x.O_Key == "DELETEDISCOUNTS");
                bool isActive = bool.Parse(opt.O_Value);
                
                var optSpecial = ((List<OptionsModel>)(await _optionrepo.GetAll()).Data).FirstOrDefault(x => x.O_Key == "SPECIALSALESTATE");
                bool isActiveSpecial = bool.Parse(optSpecial.O_Value);
                var AllPromotions = ((List<PromotionBoxModel>)(await GetAll()).Data).Where(x => x.B_Type == Type).ToList();
                if ((isActive  && Type == 6) )
                {
                    return null;
                }
                if ((!isActiveSpecial && Type == 6))
                {
                    return null;
                }
                foreach (var box in AllPromotions)
                {
                    foreach (var item in _context.PromotionBoxProducts.Where(x => x.X_SectionId == box.B_SectionId))
                    {
                        var p = _context.Product.FirstOrDefault(x=>x.P_Id == item.X_ProdId);
                        var combine = _context.ProductCombine.FirstOrDefault(x=>x.X_ColorId == item.X_ColorId && x.X_WarrantyId == item.X_WarrantyId && x.X_ProductId == item.X_ProdId);
                        if(combine != null)
                        {
                            combine.PriceModel = await _pcalcrepo.CalculateProductCombinePrice(combine.X_Id, p.P_EndLevelCatId);
                            p.DefaultProductCombine = combine;
                            if (p != null)
                            {
                                box.Products.Add(p);
                            }
                        }
                    }
                }
                return AllPromotions;
            }
            catch (Exception ex)
            {
                return new List<PromotionBoxModel>();
            }
        }
        public async Task<PromotionBoxModel> GetBySectionId(int Secid)
        {
            try
            {
                var AllPromotions = ((List<PromotionBoxModel>)(await GetAll()).Data).FirstOrDefault(x => x.B_SectionId == Secid);
                foreach (var item in _context.PromotionBoxProducts.Where(x => x.X_SectionId == Secid))
                {
                    var p = _context.Product.FirstOrDefault(x => x.P_Id == item.X_ProdId);
                    if (p != null)
                    {
                        AllPromotions.Products.Add(p);
                    }
                }
                return AllPromotions;
            }
            catch (Exception ex)
            {
                return null;
            }
        }
        public async Task<ResponseStructure> Insert(PromotionBoxModel entity)
        {
            try
            {
                var checkExists = await GetBySectionId(entity.B_SectionId);
                if(checkExists != null)
                {
                    checkExists.B_LoadType = entity.B_LoadType;
                    checkExists.B_Link = entity.B_Link;
                    checkExists.B_Image = entity.B_Image;
                    checkExists.B_Seo = entity.B_Seo;
                    checkExists.B_Title = entity.B_Title;
                    checkExists.B_Type = entity.B_Type;
                    checkExists.B_ColorCode = entity.B_ColorCode;
                    checkExists.B_EntityId = entity.B_EntityId;
                    checkExists.Updated_At = DateTime.Now;
                    return await this.Update(checkExists);
                }
                else
                {
                    this._context.PromotionBoxs.Add(entity);
                    await this._context.SaveChangesAsync();
                    return ResponseModel.Success("باکس مورد نظر با موفقیت بروزرسانی گردید");
                }
            }
            catch (Exception ex)
            {
                return ResponseModel.ServerInternalError(data: ex);
            }
        }
        public Task<ResponseStructure> LogicalAvailable(object id, bool newState)
        {
            throw new NotImplementedException();
        }

        public Task<ResponseStructure> LogicalDelete(object id)
        {
            throw new NotImplementedException();
        }
    }
}
