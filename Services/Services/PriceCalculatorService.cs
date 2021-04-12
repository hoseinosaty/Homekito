using Barayand.DAL.Interfaces;
using Barayand.Models.Entity;
using Barayand.OutModels.Miscellaneous;
using Barayand.OutModels.Response;
using Barayand.Services.Interfaces;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using System.Threading.Tasks;

namespace Barayand.Services.Services
{
    public class PriceCalculatorService : IPriceCalculatorService
    {
        private readonly IPublicMethodRepsoitory<ProductModel> _productrepo;
        private readonly IPublicMethodRepsoitory<FormulaModel> _formularepo;
        private readonly IPublicMethodRepsoitory<CopponModel> _couponrepo;
        private readonly ILogger<PriceCalculatorService> _logger;

        public PriceCalculatorService(IPublicMethodRepsoitory<ProductModel> productrepo, IPublicMethodRepsoitory<FormulaModel> formularepo, IPublicMethodRepsoitory<CopponModel> couponrepo, ILogger<PriceCalculatorService> logger)
        {
            this._productrepo = productrepo;
            this._formularepo = formularepo;
            this._couponrepo = couponrepo;
            this._logger = logger;
        }

        public async Task<PriceModel> CalculateBookPrice(int pid, string lang)
        {
            try
            {
                ProductModel product = await _productrepo.GetById(pid);
                if (product == null)
                {
                    throw new Exception("محصول مورد نظر یافت نشد");
                }
                PriceModel RESPONSE = new PriceModel();
                decimal PDFPRICE = 0;
                decimal HARDCOPYPRICE = 0;

                decimal PDFPRICEWODISCOUNT = 0;
                decimal HCOPYPRICEWODISCOUNT = 0;
                //Calculate pdf price
                DateTime regdate = (DateTime)product.Created_At;
                if (regdate.AddDays(product.P_DiscountPeriodTime) <= DateTime.Now)//if period time started
                {
                    if (product.P_PeriodDiscountPriceType == 1)//pay by formula
                    {
                        FormulaModel formul = await _formularepo.GetById(product.P_PriodDiscountFormulaId);
                        if (formul == null)
                        {
                            throw new Exception("فرمول مورد نظر یافت نشد");
                        }
                        DataTable dt = new DataTable();
                        string ProductFormula = formul.F_Formula
                            .Replace("WEIGHT", product.P_Weight.ToString().Replace("/", "."))
                            .Replace("DOLLAR", product.P_ExternalPrice.ToString().Replace("/", "."))
                            .Replace("PAGES", product.P_PageCount.ToString().Replace("/", "."));
                        PDFPRICEWODISCOUNT = decimal.Parse(dt.Compute(ProductFormula, "").ToString());
                        PDFPRICE = product.FinalPrice(price: PDFPRICEWODISCOUNT, onlyPercentag: true);

                    }
                    else//pay by tomans
                    {
                        PDFPRICEWODISCOUNT = product.P_PeriodDiscountPrice;
                        PDFPRICE = product.FinalPrice(price: PDFPRICEWODISCOUNT, onlyPercentag: true);
                    }

                    if (product.P_PrintAbleVersion)
                    {
                        if (product.P_PrintAbleVerPriceType == 1)
                        {
                            FormulaModel formul = await _formularepo.GetById(product.P_PeriodPrintableFomrulaId);
                            if (formul == null)
                            {
                                throw new Exception("فرمول مورد نظر یافت نشد");
                            }
                            DataTable dt = new DataTable();
                            string ProductFormula = formul.F_Formula
                                .Replace("WEIGHT", product.P_Weight.ToString().Replace("/", "."))
                                .Replace("DOLLAR", product.P_ExternalPrice.ToString().Replace("/", "."))
                                .Replace("PAGES", product.P_PageCount.ToString().Replace("/", "."));
                            HCOPYPRICEWODISCOUNT = decimal.Parse(dt.Compute(ProductFormula, "").ToString());
                            HARDCOPYPRICE = product.FinalPrice(price: HCOPYPRICEWODISCOUNT, onlyPercentag: true);
                        }
                        else
                        {
                            HCOPYPRICEWODISCOUNT = product.P_PeriodPrintablePrice;
                            HARDCOPYPRICE = product.FinalPrice(price: HCOPYPRICEWODISCOUNT, onlyPercentag: true);
                        }
                    }
                }
                else//period time not started and calculate productprice from main price or main formula id
                {
                    if (product.P_PriceType == 1)//pay by formula
                    {
                        FormulaModel formul = await _formularepo.GetById(product.P_PriceFormulaId);
                        if (formul == null)
                        {
                            throw new Exception("فرمول مورد نظر یافت نشد");
                        }
                        DataTable dt = new DataTable();
                        string ProductFormula = formul.F_Formula
                            .Replace("WEIGHT", product.P_Weight.ToString().Replace("/", "."))
                            .Replace("DOLLAR", product.P_ExternalPrice.ToString().Replace("/", "."))
                            .Replace("PAGES", product.P_PageCount.ToString().Replace("/", "."));
                        PDFPRICEWODISCOUNT = decimal.Parse(dt.Compute(ProductFormula, "").ToString());
                        PDFPRICE = product.FinalPrice(price: PDFPRICEWODISCOUNT, onlyPercentag: true);
                    }
                    else//pay by tomans
                    {
                        PDFPRICEWODISCOUNT = product.P_Price;
                        PDFPRICE = product.FinalPrice();
                    }
                    if (product.P_PrintAbleVersion)
                    {
                        if (product.P_PrintAbleVerPriceType == 1)
                        {
                            FormulaModel formul = await _formularepo.GetById(product.P_PrintAbleVerFormulaId);
                            if (formul == null)
                            {
                                throw new Exception("فرمول مورد نظر یافت نشد");
                            }
                            DataTable dt = new DataTable();
                            string ProductFormula = formul.F_Formula
                                .Replace("WEIGHT", product.P_Weight.ToString().Replace("/", "."))
                                .Replace("DOLLAR", product.P_ExternalPrice.ToString().Replace("/", "."))
                                .Replace("PAGES", product.P_PageCount.ToString().Replace("/", "."));
                            HCOPYPRICEWODISCOUNT = decimal.Parse(dt.Compute(ProductFormula, "").ToString());
                            HARDCOPYPRICE = product.FinalPrice(price: HCOPYPRICEWODISCOUNT, onlyPercentag: true);
                        }
                        else
                        {
                            HARDCOPYPRICE = product.P_PrintAbleVerPrice;
                            HCOPYPRICEWODISCOUNT = product.P_PrintAbleVerPrice;
                        }
                    }
                }
                ///
                RESPONSE.PdfPrice = PDFPRICE;
                if (lang == "fa")
                {
                    RESPONSE.PdfPriceFormated = PDFPRICE.ToString("#,#") + " تومان";
                    RESPONSE.PdfPriceWithOutDiscountStr = PDFPRICEWODISCOUNT.ToString("#,#") + " تومان";
                    RESPONSE.PdfPriceWithOutDiscount = PDFPRICEWODISCOUNT;
                    RESPONSE.HcopyPriceWithOutDiscountStr = HCOPYPRICEWODISCOUNT.ToString("#,#") + " تومان";
                    RESPONSE.HcopyPriceWithOutDiscount = HCOPYPRICEWODISCOUNT;
                    RESPONSE.HcopyPriceFromated = HARDCOPYPRICE.ToString("#,#") + " تومان";
                }
                else
                {
                    if (product.P_BinPrice == 0)
                    {
                        RESPONSE.PdfPriceFormated = (int)(PDFPRICE / 1000) + " Point";
                    }
                    else
                    {
                        RESPONSE.PdfPriceFormated = (int)product.P_BinPrice + " Point";
                    }
                    RESPONSE.HcopyPriceFromated = (int)(HARDCOPYPRICE / 1000) + " Point";
                }
                RESPONSE.HcopyPrice = HARDCOPYPRICE;

                if (product.P_Discount > 0)
                {
                    RESPONSE.Discount = product.P_Discount;
                    RESPONSE.Discounted = true;
                    RESPONSE.DiscountType = product.P_DiscountType;
                }
                return RESPONSE;
            }
            catch (Exception ex)
            {
                _logger.LogError("Error in calculating product service", ex);
                return new PriceModel();
            }
        }
    }
}