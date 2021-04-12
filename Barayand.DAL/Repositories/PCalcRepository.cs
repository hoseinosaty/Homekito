using Barayand.DAL.Context;
using Barayand.DAL.Interfaces;
using Barayand.Models.Entity;
using Barayand.Models.RuntimeModels;
using Barayand.OutModels.Miscellaneous;
using Barayand.OutModels.Response;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Barayand.DAL.Repositories
{
    public class PCalcRepository : IPCalcRepository
    {
        private readonly IPromotionBoxProdRepository _boxProdRepository;
        private readonly IPublicMethodRepsoitory<CopponModel> _couponrepo;
        private readonly IFestivalRepository _festrepo;
        private readonly ILogger<PCalcRepository> _logger;
        private readonly BarayandContext _context;
        IPublicMethodRepsoitory<OptionsModel> _optionrepo;
        private bool RemoveDiscounts = false;

        public PCalcRepository(IPublicMethodRepsoitory<CopponModel> couponrepo, ILogger<PCalcRepository> logger, IPromotionBoxProdRepository boxProdRepository, IFestivalRepository festivalRepository, BarayandContext context, IPublicMethodRepsoitory<OptionsModel> optionrepo)
        {
            this._couponrepo = couponrepo;
            this._logger = logger;
            this._boxProdRepository = boxProdRepository;
            this._festrepo = festivalRepository;
            _context = context;
            _optionrepo = optionrepo;
            CheckOptions();
        }
        public async Task CheckOptions()
        {
            var opt = ((List<OptionsModel>)(await _optionrepo.GetAll()).Data).FirstOrDefault(x => x.O_Key == "DELETEDISCOUNTS");
            bool isActive = bool.Parse(opt.O_Value);
            this.RemoveDiscounts = isActive;
        }
        public async Task<ProductCombinePriceModel> CalculateProductCombinePrice(int cid, int EndLevelCatId = 0)
        {
            try
            {
                Thread.CurrentThread.CurrentCulture = new System.Globalization.CultureInfo("en-GB");
                ProductCombinePriceModel PriceModel = new ProductCombinePriceModel();
                var existsCombine = this._context.ProductCombine.FirstOrDefault(x => x.X_Id == cid);
                if (existsCombine != null)
                {
                    if (RemoveDiscounts)
                    {
                        PriceModel = existsCombine.PriceModel;
                    }
                    else
                    {
                        List<FestivalOfferModel> AllFestivalRepo = ((List<FestivalOfferModel>)(await _festrepo.GetAll()).Data);
                        var existsInBox = await _boxProdRepository.CheckProductCombineExistsInBox(existsCombine.X_ProductId, existsCombine.X_WarrantyId, existsCombine.X_ColorId);

                        if (existsInBox != null)
                        {
                            var defaultCombine = existsCombine;
                            PriceModel.Avl = existsCombine.X_AvailableCount;
                            PriceModel.Price = defaultCombine.X_Price;
                            if (existsInBox.X_SectionId == 34 && (DateTime.Now >= existsInBox.X_StartDate && DateTime.Now <= existsInBox.X_EndDate))//is special sale and timer started
                            {
                                if (existsInBox.X_DiscountedPrice > 0)//has discount
                                {
                                    PriceModel.HasDiscount = true;
                                    if (!existsInBox.X_DiscountType) // please calculate price by percentage
                                    {
                                        PriceModel.Discount = existsInBox.X_DiscountedPrice;
                                        PriceModel.DiscountedPrice = (defaultCombine.X_Price - ((defaultCombine.X_Price * existsInBox.X_DiscountedPrice) / 100));
                                    }
                                    else//calculate percentage by price
                                    {
                                        var disc = ((existsInBox.X_DiscountedPrice * 100) / defaultCombine.X_Price);
                                        PriceModel.Discount = (100 - disc);
                                        PriceModel.DiscountedPrice = existsInBox.X_DiscountedPrice;
                                    }
                                }
                                PriceModel.Timer = existsInBox.X_EndDate.ToString("yyyy-MM-dd HH:mm:ss");
                            }

                            if (existsInBox.X_SectionId != 34)
                            {
                                if (existsInBox.X_DiscountedPrice > 0)//has discount
                                {
                                    PriceModel.HasDiscount = true;
                                    if (!existsInBox.X_DiscountType) // please calculate price by percentage
                                    {
                                        PriceModel.Discount =
                                        existsInBox.X_DiscountedPrice;
                                        PriceModel.DiscountedPrice =
                                        (defaultCombine.X_Price -
                                        ((defaultCombine.X_Price *
                                        existsInBox.X_DiscountedPrice) / 100));
                                    }
                                    else//calculate percentage by price
                                    {
                                        var disc = ((existsInBox.X_DiscountedPrice *
                                            100) / defaultCombine.X_Price);
                                        PriceModel.Discount = (100 - disc);

                                        PriceModel.DiscountedPrice =
                                        existsInBox.X_DiscountedPrice;
                                    }
                                }
                            }
                        }
                        else if (AllFestivalRepo.Count(x => x.F_Type == 1 && x.F_Status) > 0)
                        {
                            var fest = AllFestivalRepo.FirstOrDefault(x => x.F_Type == 1);
                            var defaultCombine = existsCombine;
                            var cmb = await SetDiscountByCombine(defaultCombine, fest.F_Discount);
                            PriceModel = cmb.PriceModel;
                        }
                        else if (EndLevelCatId != 0 && AllFestivalRepo.Count(x => x.F_Type == 2 && x.F_Status && x.F_EndLevelCategoryId == EndLevelCatId) > 0)
                        {
                            var fest = AllFestivalRepo.FirstOrDefault(x => x.F_Type == 2 && x.F_Status && x.F_EndLevelCategoryId == EndLevelCatId);
                            var cmb = await SetDiscountByCombine(existsCombine, fest.F_Discount);
                            PriceModel = cmb.PriceModel;
                        }
                        else
                        {
                            if (existsCombine.X_Discount > 0)//has discount
                            {
                                PriceModel.HasDiscount = true;
                                PriceModel.Price = existsCombine.X_Price;
                                if (existsCombine.X_DiscountType == 1) // please calculate price by percentage
                                {
                                    PriceModel.Discount = existsCombine.X_Discount;
                                    PriceModel.DiscountedPrice = (existsCombine.X_Price - ((existsCombine.X_Price * existsCombine.X_Discount) / 100));
                                }
                                else//calculate percentage by price
                                {
                                    var disc = ((existsCombine.X_Discount * 100) / existsCombine.X_Price);
                                    PriceModel.Discount = (100 - disc);

                                    PriceModel.DiscountedPrice = existsCombine.X_Discount;
                                }
                            }
                            else
                            {
                                PriceModel.HasDiscount = false;
                                PriceModel.Price = existsCombine.X_Price;
                                PriceModel.Discount = 0;
                                PriceModel.DiscountedPrice = 0;
                                PriceModel.Avl = existsCombine.X_AvailableCount;
                            }
                        }
                    }

                }
                return PriceModel;
            }
            catch (Exception ex)
            {
                return new ProductCombinePriceModel();
            }
        }

        public async Task<ProductCombineModel> CalculateProductPrice(int pid, int EndLevelCatId = 0)
        {
            Thread.CurrentThread.CurrentCulture = new System.Globalization.CultureInfo("en");

            ProductCombineModel ProductCombine = new ProductCombineModel();
            try
            {
                if (this.RemoveDiscounts)
                {
                    return await CalculateDefaultCombine(pid);
                }
                ProductCombinePriceModel PriceModel = new ProductCombinePriceModel();
                List<FestivalOfferModel> AllFestivalRepo = ((List<FestivalOfferModel>)(await _festrepo.GetAll()).Data);
                var existsInBox = await _boxProdRepository.CheckProductEixstsInBoxs(pid);

                //product exists in boxs
                if (existsInBox != null)
                {
                    var defaultCombine = _context.ProductCombine.FirstOrDefault(x => x.X_ColorId == existsInBox.X_ColorId && x.X_WarrantyId == existsInBox.X_WarrantyId && x.X_ProductId == existsInBox.X_ProdId && x.X_Status && !x.X_IsDeleted);
                    if (defaultCombine == null)
                    {
                        return await CalculateDefaultCombine(pid);
                    }

                    ProductCombine = defaultCombine;
                    PriceModel.Price = defaultCombine.X_Price;
                    PriceModel.Avl = defaultCombine.X_AvailableCount;
                    if (existsInBox.X_SectionId == 34 && (DateTime.Now >= existsInBox.X_StartDate && DateTime.Now <= existsInBox.X_EndDate))//is special sale and timer started
                    {
                        if (existsInBox.X_DiscountedPrice > 0)//has discount
                        {
                            PriceModel.HasDiscount = true;
                            if (!existsInBox.X_DiscountType) // please calculate price by percentage
                            {
                                PriceModel.Discount = existsInBox.X_DiscountedPrice;
                                PriceModel.DiscountedPrice = (defaultCombine.X_Price - ((defaultCombine.X_Price * existsInBox.X_DiscountedPrice) / 100));
                            }
                            else//calculate percentage by price
                            {
                                var disc = ((existsInBox.X_DiscountedPrice * 100) / defaultCombine.X_Price);
                                PriceModel.Discount = (100 - disc);
                                PriceModel.DiscountedPrice = existsInBox.X_DiscountedPrice;
                            }
                        }
                        PriceModel.Timer = existsInBox.X_EndDate.ToString("yyyy-MM-dd HH:mm:ss");
                    }
                    else
                    {
                        return await CalculateDefaultCombine(pid);
                    }
                    if (existsInBox.X_SectionId != 34)
                    {
                        if (existsInBox.X_DiscountedPrice > 0)//has discount
                        {
                            PriceModel.Avl = defaultCombine.X_AvailableCount;
                            PriceModel.HasDiscount = true;
                            if (!existsInBox.X_DiscountType) // please calculate price by percentage
                            {
                                PriceModel.Discount = existsInBox.X_DiscountedPrice;
                                PriceModel.DiscountedPrice = (defaultCombine.X_Price - ((defaultCombine.X_Price * existsInBox.X_DiscountedPrice) / 100));
                            }
                            else//calculate percentage by price
                            {
                                PriceModel.Discount = ((existsInBox.X_DiscountedPrice * 100) / defaultCombine.X_Price);
                                PriceModel.DiscountedPrice = existsInBox.X_DiscountedPrice;
                            }
                        }
                    }
                }
                else if (AllFestivalRepo.Count(x => x.F_Type == 1 && x.F_Status) > 0)
                {
                    var fest = AllFestivalRepo.FirstOrDefault(x => x.F_Type == 1);
                    var defaultCombine = await CalculateDefaultCombine(pid);
                    return await CalculateDefaultCombine(pid, fest.F_Discount);
                }
                else if (EndLevelCatId != 0 && AllFestivalRepo.Count(x => x.F_Type == 2 && x.F_Status && x.F_EndLevelCategoryId == EndLevelCatId) > 0)
                {
                    var fest = AllFestivalRepo.FirstOrDefault(x => x.F_Type == 2 && x.F_Status && x.F_EndLevelCategoryId == EndLevelCatId);
                    var defaultCombine = await CalculateDefaultCombine(pid);
                    return await CalculateDefaultCombine(pid, fest.F_Discount);
                }
                else
                {
                    return await CalculateDefaultCombine(pid);
                }

                ProductCombine.PriceModel = PriceModel;
                return ProductCombine;
            }
            catch (Exception ex)
            {
                return ProductCombine;
            }
        }

        public async Task<bool> checkProductCombineExistsDiscount(int cid, int EndLevelCatId = 0)
        {
            try
            {
                if (RemoveDiscounts)
                {
                    return false;
                }
                ProductCombinePriceModel PriceModel = new ProductCombinePriceModel();
                var existsCombine = _context.ProductCombine.FirstOrDefault(x => x.X_Id == cid);
                if (existsCombine != null)
                {
                    List<FestivalOfferModel> AllFestivalRepo = ((List<FestivalOfferModel>)(await _festrepo.GetAll()).Data);
                    var existsInBox = await _boxProdRepository.CheckProductCombineExistsInBox(existsCombine.X_ProductId, existsCombine.X_WarrantyId, existsCombine.X_ColorId);
                    if (existsInBox != null)
                    {
                        var defaultCombine = existsCombine;

                        PriceModel.Price = defaultCombine.X_Price;
                        if (existsInBox.X_SectionId == 34 && (DateTime.Now >= existsInBox.X_StartDate && DateTime.Now <= existsInBox.X_EndDate))//is special sale and timer started
                        {
                            if (existsInBox.X_DiscountedPrice > 0)//has discount
                            {
                                return true;
                            }
                        }

                        if (existsInBox.X_SectionId != 34)
                        {
                            if (existsInBox.X_DiscountedPrice > 0)//has discount
                            {
                                return true;
                            }
                        }
                    }
                    else if (AllFestivalRepo.Count(x => x.F_Type == 1 && x.F_Status) > 0)
                    {
                        return true;
                    }
                    else if (EndLevelCatId != 0 && AllFestivalRepo.Count(x => x.F_Type == 2 && x.F_Status && x.F_EndLevelCategoryId == EndLevelCatId) > 0)
                    {
                        return true;
                    }

                }
                return false;
            }
            catch (Exception ex)
            {
                return true;
            }
        }

        private async Task<ProductCombineModel> CalculateDefaultCombine(int pid, decimal discount = 0)
        {
            Thread.CurrentThread.CurrentCulture = new System.Globalization.CultureInfo("en-GB");
            ProductCombineModel ProductCombine = new ProductCombineModel();
            try
            {
                ProductCombinePriceModel PriceModel = new ProductCombinePriceModel();
                //product not exists in boxs
                var getAllCombines = _context.ProductCombine.Where(x => x.X_ProductId == pid && x.X_Status && !x.X_IsDeleted);
                if (getAllCombines.Count() > 0)//combines set for current product
                {
                    var defaultCombine = getAllCombines.FirstOrDefault(x => x.X_Default);

                    if (defaultCombine == null)
                    {
                        defaultCombine = getAllCombines.FirstOrDefault(x => x.X_Price > 0 && x.X_AvailableCount > 0);
                    }
                    if (defaultCombine != null)//has default combine
                    {
                        PriceModel.Avl = defaultCombine.X_AvailableCount;
                        if (defaultCombine.X_AvailableCount < 1)
                        {
                            var hasavl = getAllCombines.FirstOrDefault(x => x.X_AvailableCount > 0);
                            if (hasavl != null)
                            {
                                defaultCombine = hasavl;
                            }
                        }
                        ProductCombine = defaultCombine;
                        PriceModel.Price = defaultCombine.X_Price;
                        PriceModel.Avl = defaultCombine.X_AvailableCount;
                        if (discount != 0)
                        {
                            defaultCombine.X_Discount = discount;
                            defaultCombine.X_DiscountType = 1;
                        }
                        if (defaultCombine.X_Discount > 0 && !RemoveDiscounts)//has discount
                        {
                            PriceModel.HasDiscount = true;
                            if (defaultCombine.X_DiscountType == 1) // please calculate price by percentage
                            {
                                PriceModel.Discount = defaultCombine.X_Discount;
                                PriceModel.DiscountedPrice = (defaultCombine.X_Price - ((defaultCombine.X_Price * defaultCombine.X_Discount) / 100));
                            }
                            else//calculate percentage by price
                            {
                                var disc = ((defaultCombine.X_Discount * 100) / defaultCombine.X_Price);
                                PriceModel.Discount = (100 - disc);
                                PriceModel.DiscountedPrice = defaultCombine.X_Discount;
                            }
                        }
                    }
                }
                ProductCombine.PriceModel = PriceModel;
                return ProductCombine;
            }
            catch (Exception ex)
            {
                return ProductCombine;
            }
        }
        private async Task<ProductCombineModel> SetDiscountByCombine(ProductCombineModel defaultCombine, decimal discount = 0)
        {
            ProductCombineModel ProductCombine = defaultCombine;
            try
            {
                ProductCombinePriceModel PriceModel = new ProductCombinePriceModel();
                //product not exists in boxs
                PriceModel.Price = defaultCombine.X_Price;
                if (discount != 0)
                {
                    defaultCombine.X_Discount = discount;
                    defaultCombine.X_DiscountType = 1;
                }
                if (defaultCombine.X_Discount > 0 && !RemoveDiscounts)//has discount
                {
                    PriceModel.HasDiscount = true;
                    if (defaultCombine.X_DiscountType == 1) // please calculate price by percentage
                    {
                        PriceModel.Discount = defaultCombine.X_Discount;
                        PriceModel.DiscountedPrice = (defaultCombine.X_Price - ((defaultCombine.X_Price * defaultCombine.X_Discount) / 100));
                    }
                    else//calculate percentage by price
                    {
                        var disc = ((defaultCombine.X_Discount * 100) / defaultCombine.X_Price);
                        PriceModel.Discount = (100 - disc);
                        PriceModel.DiscountedPrice = defaultCombine.X_Discount;
                    }
                }


                ProductCombine.PriceModel = PriceModel;
                return ProductCombine;
            }
            catch (Exception ex)
            {
                return ProductCombine;
            }
        }
    }
}
