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

namespace Barayand.Controllers.Cpanel.Requests
{
    [Route("api/cpanel/[controller]")]
    [ApiController]
    public class TicketController : ControllerBase
    {
        private readonly ILogger<TicketController> _logger;
        private readonly IPublicMethodRepsoitory<TicketModel> _ticketrepo;
        private readonly IUserRepository _userrepo;
        public TicketController(ILogger<TicketController> logger, IPublicMethodRepsoitory<TicketModel> ticketrepo,IUserRepository userrepo)
        {
            _logger = logger;
            _ticketrepo = ticketrepo;
            _userrepo = userrepo;
        }
        [Route("AddTicket")]
        [HttpPost]
        public async Task<IActionResult> AddTicket(TicketModel ticket)
        {
            try
            {
                return new JsonResult(await _ticketrepo.Insert(ticket));
            }
            catch(Exception ex)
            {
                _logger.LogError("",ex);
                return new JsonResult(ResponseModel.ServerInternalError(data:ex));
            }
        }
        [Route("LoadTickets/{eid}")]
        [HttpPost]
        public async Task<IActionResult> LoadTickets(decimal eid)
        {
            try
            {
                var allTickets = ((List<TicketModel>)(await _ticketrepo.GetAll()).Data).Where(x => x.T_Cid == eid).ToList();
                List<object> tickets = new List<object>();
                foreach(var t in allTickets)
                {
                    string user = "Admin";
                    bool right = true;
                    if(t.T_Userid != 0)
                    {
                        var um = await _userrepo.GetById(t.T_Userid);
                        if(um != null)
                        {
                            user = um.surename;
                            
                        }
                        right = false;
                    }
                    tickets.Add(new {
                        right=right,
                        user = user,
                        body = t.T_Body,
                        date = ((DateTime)t.Created_At).ToString("yyyy-MM-dd"),
                        id = t.T_Id
                    });
                }
                return new JsonResult(ResponseModel.Success(data:tickets));
            }
            catch (Exception ex)
            {
                _logger.LogError("", ex);
                return new JsonResult(ResponseModel.ServerInternalError(data: ex));
            }
        }
    }
}
