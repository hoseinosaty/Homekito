using AutoMapper;
using Barayand.Models.Entity;
using Barayand.OutModels.Miscellaneous;
using Barayand.OutModels.Response;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Barayand.Services.Interfaces
{
    public interface IReportService
    {
        Task<object> TodayIncome();
        Task<decimal> TodayIncome(DateTime date);
        Task<object> TodaySale();
        Task<object> TodayNewCustomers();
        Task<object> TodayNewVisitors();
        Task<object> TotalOverview();
        Task<object> LastSixMonthIncome();
        Task<object> SaleTargets();
        Task<object> LastOrders();
        Task<object> SaleByCategories();
        
    }
}
