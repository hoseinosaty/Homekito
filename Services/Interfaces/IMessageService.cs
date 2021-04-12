using Barayand.OutModels.Response;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Barayand.Services.Interfaces
{
    public interface IMessageService
    {
        public string Username { get; set; }
        public string Password { get; set; }
        public string From { get; set; }
        public string To { get; set; }
        public string Url { get; set; }

        Task<ResponseStructure> Send();
    }
}
