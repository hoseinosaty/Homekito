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
using System.Threading.Tasks;
namespace Barayand.Services.Services
{
    public class LogService : ILogService
    {
        private readonly IPublicMethodRepsoitory<LogModel> _logRepo;
        public LogService(IPublicMethodRepsoitory<LogModel> logRepo)
        {
            _logRepo = logRepo;
        }
        public async Task LogException(Exception ex)
        {
            try
            {
                LogModel log = new LogModel();
                log.L_Title = ex.Message;
                log.L_Body = JsonConvert.SerializeObject(ex);
                log.L_FileName = ex.Source;
                await _logRepo.Insert(log);
                await _logRepo.CommitAllChanges();
            }
            catch(Exception e)
            {
                await LogException(e);
            }
        }

        public async Task LogResult(object res)
        {
            try
            {
                LogModel log = new LogModel();
                log.L_Title = "Result Log";
                log.L_Body = JsonConvert.SerializeObject(res);
                await _logRepo.Insert(log);
                await _logRepo.CommitAllChanges();
            }
            catch (Exception e)
            {
                await LogException(e);
            }
        }
    }
}
