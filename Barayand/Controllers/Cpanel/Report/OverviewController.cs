using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Barayand.OutModels.Response;
using Barayand.Services.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Barayand.Controllers.Cpanel.Report
{
    [Route("api/cpanel/report/[controller]")]
    [ApiController]
    public class OverviewController : ControllerBase
    {
        private readonly IReportService _reportservice;
        public OverviewController(IReportService reportService)
        {
            _reportservice = reportService;
        }
        [Route("LastOrders")]
        public async Task<IActionResult> LoadLastOrders()
        {
            try
            {
                return new JsonResult(await _reportservice.LastOrders());
            }
            catch (Exception ex)
            {
                return new JsonResult(ResponseModel.ServerInternalError(data: ex));
            }
        }
        [Route("SaleByCategory")]
        public async Task<IActionResult> SaleByCategory()
        {
            try
            {
                return new JsonResult(await _reportservice.SaleByCategories());
            }
            catch (Exception ex)
            {
                return new JsonResult(ResponseModel.ServerInternalError(data: ex));
            }
        }
        [Route("TodayIncome")]
        public async Task<IActionResult> TodayIncome()
        {
            try
            {
                return new JsonResult(await _reportservice.TodayIncome());
            }
            catch (Exception ex)
            {
                return new JsonResult(ResponseModel.ServerInternalError(data: ex));
            }
        }
        [Route("LastSixMonthIncome")]
        public async Task<IActionResult> LastSixMonthIncome()
        {
            try
            {
                return new JsonResult(await _reportservice.LastSixMonthIncome());
            }
            catch (Exception ex)
            {
                return new JsonResult(ResponseModel.ServerInternalError(data: ex));
            }
        }
        [Route("TodayCustomers")]
        public async Task<IActionResult> TodayCustomers()
        {
            try
            {
                return new JsonResult(await _reportservice.TodayNewCustomers());
            }
            catch (Exception ex)
            {
                return new JsonResult(ResponseModel.ServerInternalError(data: ex));
            }
        }
        [Route("TodaySale")]
        public async Task<IActionResult> TodaySale()
        {
            try
            {
                return new JsonResult(await _reportservice.TodaySale());
            }
            catch (Exception ex)
            {
                return new JsonResult(ResponseModel.ServerInternalError(data: ex));
            }
        }
    }
}
