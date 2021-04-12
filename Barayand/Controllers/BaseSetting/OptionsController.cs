using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Barayand.DAL.Interfaces;
using Barayand.Models.Entity;
using Barayand.OutModels.Response;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace Barayand.Controllers.BaseSetting
{
    [Route("api/cpanel/[controller]")]
    [ApiController]
    public class OptionsController : ControllerBase
    {
        private readonly IPublicMethodRepsoitory<OptionsModel> _optionrepo;
        private readonly ILogger<OptionsController> _logger;
        public OptionsController(ILogger<OptionsController> logger, IPublicMethodRepsoitory<OptionsModel> optionrepo)
        {
            _logger = logger;
            _optionrepo = optionrepo;
        }
        [Route("AddOption")]
        public async Task<ActionResult> UpdateOption(OptionsModel om)
        {
            try
            {
                var option = ((List<OptionsModel>)(await _optionrepo.GetAll()).Data).FirstOrDefault(x=>x.O_Key == om.O_Key);
                if(option == null)
                {
                    return new JsonResult(await _optionrepo.Insert(om));
                }
                else
                {
                    option.O_Value = om.O_Value;
                    return new JsonResult(await _optionrepo.Update(option));
                }
            }
            catch(Exception ex)
            {
                _logger.LogError("Error in options controller",ex);
                return new JsonResult(ResponseModel.Error(data:ex));
            }
        }
        [Route("LoadOptions")]
        public async Task<ActionResult> LoadOptions()
        {
            try
            {
                 return new JsonResult(await _optionrepo.GetAll());
            }
            catch (Exception ex)
            {
                _logger.LogError("Error in options controller", ex);
                return new JsonResult(ResponseModel.Error(data: ex));
            }
        }
    }
}
