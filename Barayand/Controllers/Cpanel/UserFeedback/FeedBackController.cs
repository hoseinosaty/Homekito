using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Barayand.DAL.Interfaces;
using Barayand.DAL.Repositories;
using Barayand.Models.Entity;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Barayand.OutModels.Response;
using Barayand.OutModels.Models;
using Barayand.OutModels.Miscellaneous;

namespace Barayand.Controllers.Cpanel.UserFeedback
{
    [Route("api/cpanel/[controller]")]
    [ApiController]
    public class FeedBackController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly IPublicMethodRepsoitory<RateModel> _raterepository;
        private readonly IPublicMethodRepsoitory<CommentModel> _commentrepository;
        public FeedBackController(IMapper mapper, IPublicMethodRepsoitory<RateModel> raterepository, IPublicMethodRepsoitory<CommentModel> commentrepsoitory)
        {
            this._raterepository = raterepository;
            this._commentrepository = commentrepsoitory;
            this._mapper = mapper;
        }
        [Route("AddRate")]
        [HttpPost]
        public async Task<ActionResult> AddRate(RateModel rate)
        {
            try
            {
                rate.R_Ip = Request.HttpContext.Connection.RemoteIpAddress.ToString();
                return new JsonResult(await this._raterepository.Insert(rate));
            }
            catch (Exception ex)
            {
                return null;
            }
        }
        [Route("AddComment")]
        [HttpPost]
        public async Task<ActionResult> AddComment(CommentModel comment)
        {
            try
            {
                return new JsonResult(await this._commentrepository.Insert(comment));
            }
            catch (Exception ex)
            {
                return null;
            }
        }
    }
}