using Barayand.OutModels.Response;
using Barayand.Services.Interfaces;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Barayand.Services.Services
{
    public class SmsService : ISmsService
    {
        public string Url { get; set; }
        public string APIKey { get ; set ; }
        public string Username { get ; set ; }
        public string Password { get ; set ; }
        public string From { get ; set ; }
        public string To { get ; set ; }
        public string PatternId { get; set; }

        public IConfiguration _configuration;
        public ILogger<SmsService> _logger;
        public SmsService(IConfiguration configuration, ILogger<SmsService> logger)
        {
            _configuration = configuration;
            _logger = logger;
            this.APIKey = _configuration["SmsSetting:APIKEY"];
            this.Username = _configuration["SmsSetting:USERNAME"];
            this.Password = _configuration["SmsSetting:PASSWORD"];
            this.From = _configuration["SmsSetting:FROMNUMBER"];
            this.Url = _configuration["SmsSetting:URL"];
            this.Url += string.Format("?apikey={0}&fnum={1}",this.APIKey,this.From);
        }
        public async Task<ResponseStructure> OfflineRequest(string phone, string code, string state)
        {
            try
            {
                this.PatternId = _configuration["SmsSetting:PATTERNS:OFFLINEREQUEST"];
                this.To = phone;
                this.Url += string.Format("&p1=code&p2=state&v1={0}&v2={1}",code,state);
                return await Send();
            }
            catch(Exception ex)
            {
                _logger.LogError("Error in Offlinerequest Method SMSCLASS",ex);
                return ResponseModel.ServerInternalError(data:ex);
            }
        }

        public async Task<ResponseStructure> OrderAlert(string phone, string invoiceid, string amount)
        {
            try
            {
                this.PatternId = _configuration["SmsSetting:PATTERNS:ORDERALERT"];
                this.To = phone;
                Thread.CurrentThread.CurrentCulture = new System.Globalization.CultureInfo("fa-IR");
                this.Url += string.Format("&p1=invoiceid&p2=amount&v1={0}&v2={1}", invoiceid, amount);
                return await Send();
            }
            catch (Exception ex)
            {
                _logger.LogError("Error in OrderAlert Method SMSCLASS", ex);
                return ResponseModel.ServerInternalError(data: ex);
            }
        }

        public async Task<ResponseStructure> SignUp(string phone, string userName = null)
        {
            try
            {
                this.PatternId = _configuration["SmsSetting:PATTERNS:SIGNUP"];
                this.To = phone;
                this.Url += string.Format("&p1=phone&v1={0}", phone);
                return await Send();
            }
            catch (Exception ex)
            {
                _logger.LogError("Error in SignUp Method SMSCLASS", ex);
                return ResponseModel.ServerInternalError(data: ex);
            }
        }
        public async Task<ResponseStructure> Otp(string phone, string code)
        {
            try
            {
                this.PatternId = _configuration["SmsSetting:PATTERNS:OTP"];
                this.To = phone;
                this.Url += string.Format("&p1=code&v1={0}", code);
                return await Send();
            }
            catch (Exception ex)
            {
                _logger.LogError("Error in SignUp Method SMSCLASS", ex);
                return ResponseModel.ServerInternalError(data: ex);
            }
        }

        public async Task<ResponseStructure> WalletAllert(string phone, int type, string amount)
        {
            try
            {
                this.PatternId = (type == 1) ? _configuration["SmsSetting:PATTERNS:WALLETDEPOSIT"]: _configuration["SmsSetting:PATTERNS:WALLETWITHDRAW"];
                this.To = phone;
                Thread.CurrentThread.CurrentCulture = new System.Globalization.CultureInfo("fa-IR");
                this.Url += string.Format("&p1=amount&p2=date&v1={0}&v2={1}", amount, DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss"));
                return await Send();
            }
            catch (Exception ex)
            {
                _logger.LogError("Error in WalletAlert Method SMSCLASS", ex);
                return ResponseModel.ServerInternalError(data: ex);
            }
        }
        public async Task<ResponseStructure> Send()
        {
            try
            {
                this.Url += string.Format("&pid={0}&tnum={1}", this.PatternId, this.To);
                var client = new HttpClient();
                HttpResponseMessage response = await client.GetAsync(this.Url);
                HttpContent responseContent = response.Content;
                using (var reader = new StreamReader(await responseContent.ReadAsStreamAsync()))
                {
                    var resp = await reader.ReadToEndAsync();
                    if(resp == "0")
                    {
                        return ResponseModel.Error("Error in sending sms.",resp);
                    }
                }
                return ResponseModel.Success();
            }
            catch (Exception ex)
            {
                _logger.LogError("Error in Send Method SMSCLASS", ex);
                return ResponseModel.ServerInternalError(data: ex);
            }
        }

        public async Task<ResponseStructure> ChangeOrderState(string phone, string invoiceid, string newstate)
        {
            try
            {
                this.PatternId = _configuration["SmsSetting:PATTERNS:CHANGEORDERSTATE"];
                this.To = phone;
                Thread.CurrentThread.CurrentCulture = new System.Globalization.CultureInfo("fa-IR");
                this.Url += string.Format("&p1=invoiceid&p2=state&v1={0}&v2={1}", invoiceid, newstate);
                return await Send();
            }
            catch (Exception ex)
            {
                _logger.LogError("Error in OrderAlert Method SMSCLASS", ex);
                return ResponseModel.ServerInternalError(data: ex);
            }
        }
    }
}
