using AutoMapper;
using Barayand.Common.Services;
using Barayand.DAL.Interfaces;
using Barayand.Models.Entity;
using Barayand.OutModels.Miscellaneous;
using Barayand.OutModels.Response;
using Barayand.Services.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore.Internal;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Primitives;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
namespace Barayand.Services.Services
{
    public class ReportService : IReportService
    {
        Barayand.DAL.Context.BarayandContext _context;
        public ReportService( Barayand.DAL.Context.BarayandContext context)
        {
            _context = context;
        }

        public async Task<object> LastOrders()
        {
            try
            {
                List<object> Items = new List<object>();
                var LastOrders = _context.Invoice.OrderByDescending(x=>x.ID).Take(10).ToList();
                Thread.CurrentThread.CurrentCulture = new System.Globalization.CultureInfo("fa-IR");
                foreach(var invoice in LastOrders)
                {
                    var user = _context.Users.FirstOrDefault(x=>x.U_Id == invoice.I_UserId);
                    string userInfo = user != null ? user.U_Name + " " + user.U_Family : "ناشناس";
                    object item = new {
                        orderNumber = invoice.I_Id,
                        orderDate = (((DateTime)invoice.Created_At).ToString("yyyy/MM/dd ساعت HH:mm:ss")),
                        orderCustomer = userInfo,
                        orderSum = invoice.I_TotalAmount.ToString("#,#"),
                        orderState = invoice.I_Status
                    };
                    Items.Add(item);
                    
                }
                return ResponseModel.Success(data:Items);
            }
            catch(Exception ex)
            {
                return ResponseModel.ServerInternalError(data:ex);
            }
        }

        public Task<object> LastSixMonthIncome()
        {
            throw new NotImplementedException();
        }

        public async Task<object> SaleByCategories()
        {
            try
            {
                var LastOrders = _context.Order.ToList();
                var AllCategories = _context.ProductCategory.Where(x=>x.PC_ParentId == 0 && x.PC_IsDeleted != true && x.PC_Status).ToList();
                int CountOrders = LastOrders.Sum(x=>x.O_Quantity);
                Thread.CurrentThread.CurrentCulture = new System.Globalization.CultureInfo("fa-IR");
                List<object> Labels = new List<object>();
                List<object> Series = new List<object>();
                foreach(var category in AllCategories)
                {
                    var products = _context.Product.Where(x=>x.P_MainCatId == category.PC_Id).ToList();
                    
                    int count = 0;
                    foreach (var product in products)
                    {
                        var order = LastOrders.Where(x=>x.O_ProductId == product.P_Id).ToList();
                        int quantity = order.Sum(x=>x.O_Quantity);

                        count += quantity;
                    }
                    Labels.Add(category.PC_Title);
                    Series.Add(count);
                }
                return ResponseModel.Success(data:new {labels=Labels,series = Series });
            }
            catch(Exception ex)
            {
                return ResponseModel.ServerInternalError(data: ex);
            }
        }

        public Task<object> SaleTargets()
        {
            throw new NotImplementedException();
        }

        public async Task<object> TodayIncome()
        {
            try
            {
                DateTime today = DateTime.Now;
                var allInvoices = _context.Invoice.Where(x=>((DateTime)x.Created_At).Day == today.Day).ToList();
                decimal Total = allInvoices.Sum(x=>x.I_TotalAmount);
                decimal yesterdayIncome = await this.TodayIncome(today.AddDays(-1));
                if(yesterdayIncome == 0)
                {
                    yesterdayIncome = 0.1M;
                }
                var perc = this.PercentChangeBetweenTwoNumber(yesterdayIncome, Total);
                if (perc > 1000)
                    perc = 1000;
                return ResponseModel.Success(data:new {change = perc,income = (Total != 0) ? Total.ToString("#,#") : "0", value=new decimal[] { yesterdayIncome, Total } });
            }
            catch(Exception ex)
            {
                return ResponseModel.ServerInternalError(data:ex);
            }
        }

        public async Task<decimal> TodayIncome(DateTime date)
        {
            try
            {
                DateTime today = date;
                var allInvoices = _context.Invoice.Where(x => ((DateTime)x.Created_At).Day == today.Day).ToList();
                decimal Total = allInvoices.Sum(x => x.I_TotalAmount);
                return Total;
            }
            catch
            {
                return 0;
            }
        }

        public Task<object> TodayNewCustomers()
        {
            throw new NotImplementedException();
        }

        public Task<object> TodayNewVisitors()
        {
            throw new NotImplementedException();
        }

        public async Task<decimal> TodaySale(DateTime date)
        {
            DateTime today = date;
            var allInvoices = _context.Invoice.Where(x => ((DateTime)x.Created_At).Day == today.Day).ToList();
            decimal Total = allInvoices.Count();
            return Total;
        }
        public async Task<object> TodaySale()
        {
            try
            {
                DateTime today = DateTime.Now;
                var allInvoices = _context.Invoice.Where(x => ((DateTime)x.Created_At).Day == today.Day).ToList();
                decimal Total = allInvoices.Count();
                decimal yesterdayIncome = await this.TodaySale(today.AddDays(-1));
                
                var perc = this.PercentChangeBetweenTwoNumber(yesterdayIncome, Total);
                if (perc > 1000)
                    perc = 1000;
                return ResponseModel.Success(data: new { change = perc, income = (Total != 0) ? Total.ToString("#,#") : "0", value = new decimal[] { yesterdayIncome, Total } });
            }
            catch (Exception ex)
            {
                return ResponseModel.ServerInternalError(data: ex);
            }
        }

        public Task<object> TotalOverview()
        {
            throw new NotImplementedException();
        }
        private decimal PercentChangeBetweenTwoNumber(decimal a, decimal b)
        {
            try
            {
                decimal result = 0;
                result = ((b - a) / Math.Abs(a)) * 100;
                return result;
            }
            catch (Exception ex)
            {
                return 0;
            }
        }
    }
}
