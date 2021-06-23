using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Barayand.DAL.Interfaces;
using Barayand.Models.Entity;
using Barayand.OutModels.Miscellaneous;
using Barayand.OutModels.Models;
using Barayand.OutModels.Response;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace HomeKito.Controllers
{
    public class PrintController : Controller
    {
        [Route("Print/{invoiceid}")]
        public IActionResult Index(string invoiceid)
        {
            if (invoiceid == null || invoiceid == "")
            {
                return RedirectToAction("Index", "Home");
            }
            ViewBag.inv = invoiceid;
            return View();
        }
    }
}
